using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Mover), typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [Header("Характеристики")]
        [SerializeField] private float _dashCooldown;
        [SerializeField] private float _transformCooldown;
        [Header("Техническое")]
        [SerializeField] private Mover _mover;
        [SerializeField] private ViewController _view;
        [SerializeField] private SoundController _sound;

        private PlayerInput _controls;

        private bool _canDash = true;
        private bool _canTransform = true;

        private bool _isBat = false;

        private Vector2 Direction => _controls.Main.Move.ReadValue<Vector2>();

        private void Awake()
        {
            _controls = new();
            _view.Init();
            _mover.Init();
            _sound.Init();

            EnableInput();
        }

        private void FixedUpdate()
        {
            _mover.FixedUpdate(Direction);
        }

        private void OnEnable()
        {
            _controls.Main.Move.started += OnMoveStart;
            _controls.Main.Move.performed += OnMove;
            _controls.Main.Move.canceled += OnMoveEnd;

            _controls.Main.Dash.performed += OnDash;
            _controls.Main.Transform.performed += OnTransform;
        }

        private void OnDisable()
        {
            _controls.Main.Move.started -= OnMoveStart;
            _controls.Main.Move.performed -= OnMove;
            _controls.Main.Move.canceled -= OnMoveEnd;

            _controls.Main.Dash.performed -= OnDash;
            _controls.Main.Transform.performed -= OnTransform;
        }

        public void EnableInput() => _controls.Enable();
        public void Disable() => _controls.Disable();

        private void OnMoveStart(InputAction.CallbackContext context)
        {
            _view.OnMoveStart();
            _sound.OnMoveStart();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _view.OnMove(Direction.x);
        }

        private void OnMoveEnd(InputAction.CallbackContext context)
        {
            _view.OnMoveEnd();
            _sound.OnMoveEnd();
        }

        private void OnTransform(InputAction.CallbackContext context)
        {
            if (_canTransform)
            {
                _isBat = !_isBat;

                _mover.Transform(_isBat);
                _view.OnTransform(_isBat);
                _sound.OnTransform();

                StartCoroutine(WaitForTransformCooldown());
            }
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (_canDash && _isBat == false)
            {
                _mover.StartDash(Direction);
                _view.OnDash();
                _sound.OnDash();

                StartCoroutine(WaitForDashCooldown());
            }
        }

        private IEnumerator WaitForTransformCooldown()
        {
            _canTransform = false;
            yield return new WaitForSeconds(_transformCooldown);
            _canTransform = true;
        }

        private IEnumerator WaitForDashCooldown()
        {
            _canDash = false;
            yield return new WaitForSeconds(_dashCooldown);
            _canDash = true;
        }
    }
}