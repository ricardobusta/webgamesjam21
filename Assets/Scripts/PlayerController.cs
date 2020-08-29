using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lookSpeed;

    //private Rigidbody _rigidbody;
    private CharacterController _characterController;
    private Vector3 _lastMousePos;

    private Vector3 _look;

    [SerializeField] private Transform _camera;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
        _lastMousePos = Input.mousePosition;
    }

    private void Move()
    {
        var t = transform;
        var lookAngle = _look.y * Mathf.Deg2Rad;
        var angleSin = Mathf.Sin(lookAngle);
        var angleCos = Mathf.Cos(lookAngle);

        var vertical = Input.GetAxis("Vertical") * movementSpeed;
        var horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        
        var forwardSpeed = angleSin * vertical + angleCos * horizontal;
        var strafeSpeed = angleCos * vertical - angleSin * horizontal;

        var movement = forwardSpeed + strafeSpeed;
        var verticalSpeed = _characterController.velocity.y;
        _characterController.SimpleMove(new Vector3(forwardSpeed,verticalSpeed,strafeSpeed));
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

        _camera.rotation = Quaternion.Euler(_look);
    }

    private void Update()
    {
        Look();
        Move();
    }
}