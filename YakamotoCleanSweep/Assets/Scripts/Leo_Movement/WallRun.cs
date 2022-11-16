using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement movement;

    [Header("Movement")]
    [SerializeField] private Transform orientation;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Detection")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("WallRunning")]
    [SerializeField] private float initialWallRunForwardForce;
    [SerializeField] private float initialWallRunUpwardForce;
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunUpJumpForce;
    [SerializeField] private float wallRunSideJumpForce;
    [SerializeField] private float requiredForwardVelocity = 1;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }

    private bool wallLeft = false;
    private bool wallRight = false;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;

    private Rigidbody rb;

    private bool isWallRunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isWallRunning = false;
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if ((wallLeft || wallRight))
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    private void CheckWall() //determines if a wall is to the left or right of a player
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    private bool CanWallRun()
    {
        //makes sure player is off the ground, moving forward, and not crouching to initiate wall run
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight) && orientation.InverseTransformDirection(rb.velocity).z > requiredForwardVelocity && !movement.isCrouching && !movement.isGrounded;
    }

    private void StartWallRun()
    {
        rb.useGravity = false; //switches to lower gravity for wall running
        isWallRunning = true;

        rb.AddForce(movement.moveDirection.normalized * initialWallRunForwardForce);
        rb.AddForce(Vector3.up * initialWallRunUpwardForce);
        rb.AddForce(Vector3.down * wallRunGravity);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        if (wallLeft) //tilts camera approriately depending on which side the wall is on
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(jumpKey)) //sends players jumping away from and up from wall
        {
            Vector3 wallRunUpJumpDirection = transform.up;
            if (wallLeft)
            {
                Vector3 wallRunLeftJumpDirection = leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunUpJumpDirection * wallRunUpJumpForce * 100, ForceMode.Force);
                rb.AddForce(wallRunLeftJumpDirection * wallRunSideJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunRightJumpDirection = rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunUpJumpDirection * wallRunUpJumpForce * 100, ForceMode.Force);
                rb.AddForce(wallRunRightJumpDirection * wallRunSideJumpForce * 100, ForceMode.Force);
            }
        }

    }

    private void StopWallRun()
    {
        if (!movement.isSprinting)
        {
            rb.useGravity = true;
            isWallRunning = false;

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
            tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        }
    }
}
