using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    [Serializable]
    public class Mover
    {
        [Header("Характеристики")]
        [SerializeField] private float _vampireSpeed;
        [SerializeField] private float _batSpeed;
        [SerializeField] private float _accelerationFactor;
        [SerializeField] private float _decelerationFactor;
        [SerializeField] private float _dashImpulse;
        [SerializeField] private float _dashDeceleration;
        [Header("Техническое")]
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _currentSpeed;
        private Vector3 _currentVelocity = Vector3.zero;
        private Vector3 _currentDashImpulse = Vector3.zero;

        public void Init()
        {
            if (_rigidbody == null)
                Debug.LogError("Rigidbody is null");

            _currentSpeed = _vampireSpeed;
        }

        public void FixedUpdate(Vector3 direction)
        {
            _currentVelocity = Vector3.MoveTowards(
                Vector3.ClampMagnitude(_currentVelocity + _accelerationFactor * Time.fixedDeltaTime * direction, _currentSpeed),
                Vector3.zero,
                _decelerationFactor * Time.fixedDeltaTime);

            _currentDashImpulse = Vector3.MoveTowards(_currentDashImpulse,
                Vector3.zero,
                _dashDeceleration * Time.fixedDeltaTime);

            _rigidbody.velocity = _currentVelocity + _currentDashImpulse;
        }

        public void Transform(bool isBat)
        {
            if (isBat)
                _currentSpeed = _batSpeed;
            else
                _currentSpeed = _vampireSpeed;
        }

        public void StartDash(Vector3 direction)
        {
            _currentDashImpulse = _dashImpulse * direction;
        }
    }
}