using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float lookSpeed;
        [SerializeField] private float jumpSpeed;

        [SerializeField] private ObjectInspector inspector;
        [SerializeField] private CharacterController characterController;

        [NonSerialized] public static bool BlockInput = true;

        public Canvas SettingsCanvas;
        public Slider SensibilitySlider;

        private Transform playerEyes;
        private Vector3 _look;

        private float _verticalSpeed;
        private bool _canJump;
        private float _verticalAxis;
        private float _horizontalAxis;
        private float _jumpAxis;

        private float _sensibility;

        private bool _noClip;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            SensibilitySlider.onValueChanged.AddListener(SetSensibility);
            SensibilitySlider.value = PlayerPrefs.GetFloat("SENSIBILITY", 1);
            
            SettingsCanvas.gameObject.SetActive(false);
        }

        private void Start()
        {
            playerEyes = EyesCamera.Transform;
        }

        private void PlayerInput()
        {
            _verticalAxis = Input.GetAxis("Vertical") * movementSpeed;
            _horizontalAxis = Input.GetAxis("Horizontal") * movementSpeed;
            _jumpAxis = Input.GetAxis("Jump");

            if (characterController.isGrounded && _jumpAxis <= 0) _canJump = true;

            if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Fire1")) inspector.PickObject();

            if (Input.GetButtonDown("Fire2")) inspector.InspectObject();
        }

        private void Move(float dt)
        {
            if (characterController.isGrounded)
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
                _verticalSpeed = characterController.velocity.y + Physics.gravity.y * dt;
            }

            var lookAngle = _look.y * Mathf.Deg2Rad;
            var angleSin = Mathf.Sin(lookAngle);
            var angleCos = Mathf.Cos(lookAngle);

            var forwardSpeed = angleSin * _verticalAxis + angleCos * _horizontalAxis;
            var strafeSpeed = angleCos * _verticalAxis - angleSin * _horizontalAxis;

            var movement = _noClip
                ? playerEyes.forward * _verticalAxis + playerEyes.right * _horizontalAxis
                : new Vector3(forwardSpeed, _verticalSpeed, strafeSpeed);

            characterController.Move(movement * dt);
        }

        private void Look()
        {
            var mouseX = Input.GetAxis("Mouse X") * _sensibility;
            var mouseY = Input.GetAxis("Mouse Y") * _sensibility;

            // Mouse horizontal control camera rotation in vertical axis and vice versa
            _look = new Vector3(
                Mathf.Clamp(_look.x - mouseY * lookSpeed, -90, 90),
                _look.y + mouseX * lookSpeed
            );

            playerEyes.rotation = Quaternion.Euler(_look);
        }

        private void SetSensibility(float v)
        {
            _sensibility = v;
            PlayerPrefs.SetFloat("SENSIBILITY", v);
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (BlockInput) return;
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _noClip = !_noClip;
                gameObject.layer = _noClip ? LayerMask.NameToLayer("UI") : LayerMask.NameToLayer("Player");
            }
#endif
            if (Input.GetKeyDown(KeyCode.F4))
            {
                var open = !SettingsCanvas.gameObject.activeSelf;
                SettingsCanvas.gameObject.SetActive(open);
                Cursor.visible = open;
                Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
            }

            if (SettingsCanvas.gameObject.activeSelf)
            {
                return;
            }
            
            PlayerInput();
            Look();
        }
    }
}