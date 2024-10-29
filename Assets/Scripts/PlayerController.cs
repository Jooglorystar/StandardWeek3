using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    private Vector2 moveInput;
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    private float minXLook = -85f;
    private float maxXLook = 85f;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    public bool canLook = true;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canLook = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnMoveinput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void Move()
    {
        Vector3 direction = transform.forward * moveInput.y + transform.right * moveInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[]
        {
            new Ray(transform.position + transform.forward * 0.2f + transform.up * 0.1f, Vector3.down),
            new Ray(transform.position + -transform.forward * 0.2f + transform.up * 0.1f, Vector3.down),
            new Ray(transform.position + transform.right * 0.2f + transform.up * 0.1f, Vector3.down),
            new Ray(transform.position + -transform.right * 0.2f + transform.up * 0.1f , Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if(Physics.Raycast(rays[i],0.1f,groundLayerMask))
            {
                return true;
            }

        }
        return false;
    }
}