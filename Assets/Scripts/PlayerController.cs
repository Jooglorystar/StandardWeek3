using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    private Vector2 moveInput;
    public float moveSpeed;
    public float jumpForce;

    [Header("Look")]
    private Vector2 mouseDelta;


    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {

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

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 direction = transform.forward * moveInput.y + transform.right * moveInput.x;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }
}