using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera PlayerCamera;

    [Header ("Rotation Settings")]
    public float rotationSenstivity = 10f;

    [Header("Movement Settings")]
    [Header("Speed Settings")]

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    [Header("Jump Settings")]
    public float jumpHeight = 1.9f;
    public float gravityScale = -20f;

    private float cameraVerticalAngle;
    private Vector3 moveInput = Vector3.zero;
    private Vector3 rotationImput = Vector3.zero;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look(){
        rotationImput.x = Input.GetAxis("Mouse X") * rotationSenstivity * Time.deltaTime;
        rotationImput.y = Input.GetAxis("Mouse Y") * rotationSenstivity * Time.deltaTime;

        cameraVerticalAngle += rotationImput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        transform.Rotate(Vector3.up * rotationImput.x);
        PlayerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }
}