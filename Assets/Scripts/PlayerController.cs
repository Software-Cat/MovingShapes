using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected Camera playerCam;
    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] protected float turnSmoothness = 0.1f;

    protected CharacterController playerController;

    private Vector2 moveInput;
    private float turnSmoothVelocity;
    private Vector3 velocity;

    [Header("Jumping")]
    [SerializeField] protected float jumpHeight = 3f;

    private bool jumpInput = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerController.isGrounded && context.started)
        {
            jumpInput = true;
        }
    }

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Turning
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothness);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Moving
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            playerController.Move(moveSpeed * Time.deltaTime * moveDir.normalized);
        }

        // Gravity
        if (playerController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += Registry.GRAVITY * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        // Jumping
        if (jumpInput)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Registry.GRAVITY);
            jumpInput = false;
        }
    }
}
