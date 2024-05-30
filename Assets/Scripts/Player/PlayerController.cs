using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CameraController CharaCamera;

    [SerializeField] private float mouseSpeed;
    private float moveSpeed;

    private PlayerStat stat;
    
    private Vector3 velocity;
    private Vector2 keyboardInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        stat = GetComponent<PlayerStatHandler>().stat;
    }

    private void Start()
    {
        moveSpeed = stat.Speed;
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void LateUpdate()
    {
        OnLook();
    }

    private void OnMove()
    {
        velocity = transform.forward * keyboardInput.y + transform.right * keyboardInput.x;
        velocity *= moveSpeed;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void OnLook()
    {
        transform.eulerAngles += new Vector3(0, mouseInput.x, 0);
        CharaCamera.y_input = -mouseInput.y;
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + mouseInput.x, transform.rotation.z);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        keyboardInput = context.ReadValue<Vector2>().normalized;
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
        mouseInput *= mouseSpeed;
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            _rigidbody.AddForce(Vector2.up * stat.Jump, ForceMode.Impulse);
    }
}
