using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private Transform orientation;
    private bool canStrafe;
 
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float crouchFOV = 50f;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float sprintFOV = 70f;
    [SerializeField] private float slideFOV = 75f;
    [SerializeField] private float fovTime = 1f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float sprintAcceleration = 10f;
    [SerializeField] private bool toggleSprint;

    [Header("Jumping")]
    [SerializeField] private float groundJumpForce = 5f;
    //[SerializeField] private AudioClip jumpSound;

    [Header("Crouching")]
    [SerializeField] private float crouchHeightScale = 0.5f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float crouchAcceleration = 4f;
    //[SerializeField] private float ceilingDistance = 0.1f;
    //[SerializeField] private Transform ceilingCheck;
    //private bool ceilingContest;

    [Header("Sliding")]
    [SerializeField] private float slideSpeed = 12f;
    [SerializeField] private float slideTime = 1f;
    [SerializeField] private float velocityToSlide = 11f;
    //[SerializeField] private AudioClip slideSound;
    private Vector3 slideDirection;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode toggleCrouchKey = KeyCode.C;
    [SerializeField] KeyCode holdCrouchKey = KeyCode.LeftControl;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private RaycastHit slopeHit;

    [Header("Physical Attributes")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform capsuleSize;
    [SerializeField] private CapsuleCollider playerCapsule;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;

    private float startPlayerHeight;
    private float currentPlayerHeight;

    public bool isCrouching { get; private set; }
    public bool isSprinting { get; private set; }
    private bool isSliding;
    AudioSource m_AudioSource;

    private delegate void CrouchDelegate();
    private CrouchDelegate crouchMethod;
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
        moveSpeed = walkSpeed;
        canStrafe = true;
        //m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //ceilingContest = Physics.CheckSphere(ceilingCheck.position, ceilingDistance);

        DelegateToggles();
        PlayerInput();
        ControlDrag();
        ControlSpeed();
        ControlPhysical();

        if (Input.GetKeyDown(jumpKey) && isGrounded && !isCrouching)
        {
            Jump(groundJumpForce);
        }

        crouchMethod();

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
    }

    private void DelegateToggles()
    {
        if (toggleSprint)
        {
            crouchMethod = HoldCrouch;
            sprintMethod = ToggleSprint;
        }
        else
        {
            crouchMethod = ToggleCrouch;
            sprintMethod = HoldSprint;
        }
    }

    public void ToggleCrouch()
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
        else if (Input.GetKeyDown(toggleCrouchKey) && isCrouching && isGrounded && !isSprinting)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y / crouchHeightScale, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
            isCrouching = false;
            isSliding = false;
            canStrafe = true;
        }
    }

    public void HoldCrouch()
    {
        if (Input.GetKey(holdCrouchKey) && isGrounded && !isCrouching)
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
        else if (Input.GetKeyUp(holdCrouchKey) && isCrouching)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y / crouchHeightScale, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
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
        }
        else if (isCrouching && isSliding)
        {
            moveSpeed = slideSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slideFOV, fovTime * Time.deltaTime);
        }
        
        else if (!isSprinting)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, crouchAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
        }
        
    }

    private void ToggleSprint()
    {
        if (Input.GetKeyDown(sprintKey) && isGrounded && !isCrouching && !isSprinting)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
            //moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = sprintFOV;
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, fovTime * Time.deltaTime);
        }
        else if (Input.GetKeyDown(sprintKey) && isGrounded && !isCrouching && isSprinting) 
        {
            isSprinting = false;
            moveSpeed = walkSpeed;
            //moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = normalFOV;
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
           //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
        }
    }

    private void HoldSprint()
    {

        if (Input.GetKey(sprintKey) && isGrounded && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, fovTime * Time.deltaTime);
            isSprinting = true;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
            isSprinting = false;
        }
    }

    private void ControlPhysical()
    {
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

    private bool OnSlope()
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


}
