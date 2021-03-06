﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float lookSpeed;
        [SerializeField] public float jumpSpeed;

        [SerializeField] private ObjectInspector inspector;
        [SerializeField] private CharacterController characterController;

        [NonSerialized] public static bool BlockLook = true;
        [NonSerialized] public static bool BlockMove = true;

        public Canvas SettingsCanvas;
        public Slider SensibilitySlider;

        public float gravity;

        private Transform playerEyes;
        private Vector3 _look;

        private float _verticalSpeed;
        private bool _canJump;
        private float _verticalAxis;
        private float _horizontalAxis;
        private float _jumpAxis;

        private float _sensibility;

        private bool _noClip;

        public static PlayerController _instance;

        private void Awake()
        {
            _instance = this;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _sensibility = PlayerPrefs.GetFloat("SENSIBILITY", 1);
            SensibilitySlider.value = _sensibility;
            SensibilitySlider.onValueChanged.AddListener(SetSensibility);

            SettingsCanvas.gameObject.SetActive(false);
        }

        private void Start()
        {
            playerEyes = EyesCamera.Transform;
            gravity = Physics.gravity.y;
        }

        public static void SetSpeed(float s)
        {
            _instance.movementSpeed = s;
        }

        public static void SetJump(float j)
        {
            _instance.jumpSpeed = j;
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
            if (BlockMove) return;
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
                _verticalSpeed = characterController.velocity.y + gravity * dt;
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
            if (BlockLook) return;
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