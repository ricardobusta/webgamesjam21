using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float jumpSpeed;
    
    [SerializeField] private Transform playerCamera;
    
    private CharacterController _characterController;
    private Vector3 _lastMousePos;

    private Vector3 _look;

    private float _verticalSpeed;
    private bool _canJump;
    private float _verticalAxis;
    private float _horizontalAxis;
    private float _jumpAxis;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
        _lastMousePos = Input.mousePosition;
    }

    private void MoveInput()
    {
        _verticalAxis = Input.GetAxis("Vertical") * movementSpeed;
        _horizontalAxis = Input.GetAxis("Horizontal") * movementSpeed;
        _jumpAxis = Input.GetAxis("Jump");
        
        if (_characterController.isGrounded && _jumpAxis <= 0)
        {
            _canJump = true;
        } 
    }
    
    private void Move(float dt)
    {
        if (_characterController.isGrounded)
        {
            if (_canJump && _jumpAxis > 0)
            {
                _verticalSpeed = jumpSpeed;
                _canJump = false;
            }
            else
            {
                _verticalSpeed = 0;
            }
        }
        else
        {
            _verticalSpeed = _characterController.velocity.y + Physics.gravity.y * dt;
        }
        
        var lookAngle = _look.y * Mathf.Deg2Rad;
        var angleSin = Mathf.Sin(lookAngle);
        var angleCos = Mathf.Cos(lookAngle);

        var forwardSpeed = angleSin * _verticalAxis + angleCos * _horizontalAxis;
        var strafeSpeed = angleCos * _verticalAxis - angleSin * _horizontalAxis;

        _characterController.Move(new Vector3(forwardSpeed, _verticalSpeed, strafeSpeed) * dt);
    }

    private void Look()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        // Mouse horizontal control camera rotation in vertical axis and vice versa
        _look = new Vector3(
            Mathf.Clamp(_look.x - mouseY * lookSpeed, -90, 90),
            _look.y + mouseX * lookSpeed
        );

        playerCamera.rotation = Quaternion.Euler(_look);
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
    }

    private void Update()
    {
        MoveInput();
        Look();
    }
}