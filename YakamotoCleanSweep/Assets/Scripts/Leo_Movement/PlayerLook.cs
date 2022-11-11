using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WallRun wallRun;

    [SerializeField] private float sensX = 2f;
    [SerializeField] private float sensY = 2f;

    private Camera cam;
    [SerializeField] private Transform orientation;

    private float mouseX;
    private float mouseY;

    [SerializeField] private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    public bool allowLooking;

    private void Start()
    {

        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        allowLooking = true;
    }

    private void Update()
    {
        if (allowLooking)
        {
            PlayerInput();
        }

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void PlayerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    public void setSensitivity(float sensitivity) {
        Debug.Log(sensitivity);
        multiplier = sensitivity;
    }
}
