﻿using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float lookSpeed;
        [SerializeField] private float jumpSpeed;

        [SerializeField] private ObjectInspector inspector;
        [SerializeField] private CharacterController characterController;

        private Transform playerEyes;
        private Vector3 _look;

        private float _verticalSpeed;
        private bool _canJump;
        private float _verticalAxis;
        private float _horizontalAxis;
        private float _jumpAxis;

        private bool _noClip;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            // Mouse horizontal control camera rotation in vertical axis and vice versa
            _look = new Vector3(
                Mathf.Clamp(_look.x - mouseY * lookSpeed, -90, 90),
                _look.y + mouseX * lookSpeed
            );

            playerEyes.rotation = Quaternion.Euler(_look);
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _noClip = !_noClip;
                gameObject.layer = _noClip ? LayerMask.NameToLayer("UI") : LayerMask.NameToLayer("Player");
            }

            PlayerInput();
            Look();
        }
    }
}