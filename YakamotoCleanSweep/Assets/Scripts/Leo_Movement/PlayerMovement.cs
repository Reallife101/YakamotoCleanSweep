using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected pauseLevel pause;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f; //for debug: shows the current movem speed of the player
    [SerializeField] private float movementMultiplier = 10f; //purely for rigidbody physics
    [SerializeField] private float airMultiplier = 0.4f; //rigidbody physics in the air
    [SerializeField] private Transform orientation; //keeps track of where the player is looking
    [SerializeField] private bool toggleSprint; //whether or not the sprint button is toggle or hold
    private bool canStrafe; //whether or not the player can move side-to-side
 
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPosition;
    private Vector3 startingCamPosition;
    //Fields of View for the camera
    [SerializeField] private float crouchFOV = 80f;
    [SerializeField] private float normalFOV = 90f;
    [SerializeField] private float sprintFOV = 100f;
    [SerializeField] private float slideFOV = 110f;
    //Smoothing Values to Prevent FOV Stuttering
    [SerializeField] private float actualCrouchFOV = 83f;
    [SerializeField] private float actualNormalFOV = 90f;
    [SerializeField] private float actualSprintFOV = 100f;
    [SerializeField] private float actualSlideFOV = 103f;
    [SerializeField] private float fovBuffer = 1f;
    //for lerp time to transition between FOVs
    [SerializeField] private float fovTime = 15f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float sprintAcceleration = 10f; //how long it takes to get up to sprint speed

    [Header("Jumping")]
    [SerializeField] private float groundJumpForce = 5f;
    //[SerializeField] private AudioClip jumpSound;

    [Header("Crouching")]
    [SerializeField] private float crouchHeightScale = 0.5f; //height of the player when crouching
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float crouchAcceleration = 4f;
    [SerializeField] private float ceilingDistance = 0.1f;
    [SerializeField] private Transform ceilingCheck;
    private bool ceilingContest;

    [Header("Sliding")]
    [SerializeField] private float slideSpeed = 12f;
    [SerializeField] private float slideTime = 1f; //max time slide can last
    [SerializeField] private float velocityToSlide = 11f; //required velocity of player to initiate a slide
    //[SerializeField] private AudioClip slideSound;
    private Vector3 slideDirection; //tracks the direction a player initiated a slide so they stay sliding in that direction

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode toggleCrouchKey = KeyCode.C;
    [SerializeField] KeyCode holdCrouchKey = KeyCode.LeftControl;

    [Header("Drag")]
    //rigidbody drag
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck; //game object on the bottom of the player
    [SerializeField] private float groundDistance = 0.4f; //distance the player has to be from the ground to determine ifGrounded
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask enemyMask;
    public bool isGrounded { get; private set; }
    private RaycastHit slopeHit;

    [Header("Physical Attributes")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform capsuleSize;
    [SerializeField] private CapsuleCollider playerCapsule;

    private float horizontalMovement;
    private float verticalMovement;

    public Vector3 moveDirection { get; private set; }
    private Vector3 slopeMoveDirection;

    private float startPlayerHeight;
    private float currentPlayerHeight;

    public bool isCrouching { get; private set; }
    public bool isSprinting { get; private set; }
    private bool isSliding;
    AudioSource m_AudioSource;

    //relies on toggleSprint
    private delegate void SprintDelegate();
    private SprintDelegate sprintMethod;

    private void Start()
    {
        rb.freezeRotation = true;
        startPlayerHeight = playerCapsule.height;
        currentPlayerHeight = startPlayerHeight;
        isCrouching = false;
        isSprinting = false;
        isSliding = false;
        cam.fieldOfView = normalFOV;
        startingCamPosition = cameraPosition.localPosition;
        moveSpeed = walkSpeed;
        canStrafe = true;
        //m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, enemyMask); //performs sphereCheck to see if the player is close enough to the ground to be considered grounded
        ceilingContest = Physics.CheckSphere(ceilingCheck.position, ceilingDistance);

        DelegateToggles();
        PlayerInput();
        ControlDrag();
        ControlSpeed();
        ControlPhysical();
        Crouching();

        if (Input.GetKeyDown(jumpKey) && isGrounded && !pause.isPaused())
        {
            Jump(groundJumpForce);
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        //Debug.Log(rb.velocity.magnitude);
    }

    public void PlayerInput()
    {
        if (canStrafe)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            verticalMovement = Input.GetAxisRaw("Vertical");
        }

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //m_AudioSource.clip = jumpSound;
        //m_AudioSource.PlayOneShot(m_AudioSource.clip);

        if (isCrouching)
        {
            isCrouching = false;
            isSliding = false;
            canStrafe = true;
            capsuleSize.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
        }
    }

    private void DelegateToggles()
    {
        if (toggleSprint)
        {
            sprintMethod = ToggleSprint;
        }
        else
        {
            sprintMethod = HoldSprint;
        }
    }

    public void Crouching()
    {
        if (Input.GetKeyDown(toggleCrouchKey) && isGrounded && !isCrouching)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y * crouchHeightScale, 1);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y * crouchHeightScale, 0);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            isCrouching = true;
            if (rb.velocity.magnitude > velocityToSlide) //TODO: add buffer variable for slide speed target
            {
                Slide();
                Invoke("StopSlide", slideTime);
            }
        }
        else if (Input.GetKeyDown(toggleCrouchKey) && isCrouching && isGrounded && !isSprinting && !ceilingContest)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y / crouchHeightScale, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
            isCrouching = false;
            isSliding = false;
            canStrafe = true;
        }

        else if (Input.GetKey(holdCrouchKey) && isGrounded && !isCrouching)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y * crouchHeightScale, 1);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y * crouchHeightScale, 0);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            isCrouching = true;
            if (rb.velocity.magnitude > velocityToSlide)
            {
                Slide();
                Invoke("StopSlide", slideTime);
            }
        }
        else if (Input.GetKeyUp(holdCrouchKey) && isCrouching && !ceilingContest)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y / crouchHeightScale, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
            isCrouching = false;
            isSliding = false;
            canStrafe = true;
        }
        else if (!isGrounded)
        {
            capsuleSize.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = startingCamPosition;
            isCrouching = false;
            isSliding = false;
            canStrafe = true;
        }
    }

    private void Slide()
    {
        slideDirection = rb.velocity.normalized;
        isSliding = true;
        isSprinting = false;
        canStrafe = false;
        //m_AudioSource.clip = slideSound;
        //m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    private void StopSlide()
    {
        isSliding = false;
        canStrafe = true;
    }

    public void ControlSpeed()
    {
        sprintMethod();

        if (isCrouching && !isSliding)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, crouchAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, crouchFOV, fovTime * Time.deltaTime);
            if (cam.fieldOfView < actualCrouchFOV + fovBuffer)
            {
                cam.fieldOfView = actualCrouchFOV;
            }
        }
        else if (isSliding)
        {
            moveSpeed = slideSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slideFOV, fovTime * Time.deltaTime);
            if (cam.fieldOfView > actualSlideFOV - fovBuffer)
            {
                cam.fieldOfView = actualSlideFOV;
            }
        }
        else if (isSprinting)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, fovTime * Time.deltaTime);
            if (cam.fieldOfView >= actualSprintFOV - fovBuffer && cam.fieldOfView < actualSprintFOV + fovBuffer)
            {
                cam.fieldOfView = sprintFOV;
            }
        }
        else if (!isSprinting)
        {
            isSprinting = false;
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, crouchAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
            if (cam.fieldOfView >= actualNormalFOV - fovBuffer && cam.fieldOfView < actualNormalFOV + fovBuffer)
            {
                cam.fieldOfView = normalFOV;
            }
        }
        
    }

    private void ToggleSprint()
    {
        if (Input.GetKeyDown(sprintKey) && isGrounded && !isCrouching && !isSprinting)
        {
            isSprinting = true;
        }
        else if ((Input.GetKeyDown(sprintKey) && isGrounded && !isCrouching && isSprinting) || verticalMovement == 0 || !isGrounded) 
        {
            isSprinting = false;
        }
    }

    private void HoldSprint()
    {

        if (Input.GetKey(sprintKey) && isGrounded && !isCrouching)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void ControlPhysical()
    {
        //changes the player capsule height depending on if crouched or not
        playerCapsule.height = currentPlayerHeight;
        groundCheck.localPosition = new Vector3(0, -capsuleSize.transform.localScale.y, 0);
    }

    public void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private bool OnSlope() //stops player from sliding down on slopes
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, currentPlayerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (isSliding)
        {
            rb.AddForce(slideDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void MultiplySpeed(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}
