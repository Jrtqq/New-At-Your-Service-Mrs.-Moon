using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    [Serializable]
    public class ViewController
    {
        private const string IsBatAnimatorBool = "IsBat";
        private const string IsWalkingAnimatorBool = "IsWalking";
        private const string DieAnimatorTrigger = "Die";
        private const string ResetAnimatorTrigger = "Reset";

        [Header("Техническое")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        public void Init()
        {
            if (_spriteRenderer == null)
                Debug.LogError("SpriteRenderer is null");
            if (_animator == null)
                Debug.LogError("Animator is null");
        }

        public void OnMove(float xAxisDirection)
        {
            if (xAxisDirection < 0)
                _spriteRenderer.flipX = true;
            else
                _spriteRenderer.flipX = false;
        }

        public void OnMoveStart()
        {
            _animator.SetBool(IsWalkingAnimatorBool, true);
        }

        public void OnMoveEnd()
        {
            _animator.SetBool(IsWalkingAnimatorBool, false);
        }

        public void OnTransform(bool isBat)
        {
            _animator.SetBool(IsBatAnimatorBool, isBat);
        }

        public void OnDash()
        {

        }

        public void OnDeath()
        {
            _animator.SetTrigger(DieAnimatorTrigger);
        }

        public void Reset()
        {
            _animator.SetTrigger(ResetAnimatorTrigger);

            _animator.SetBool(IsWalkingAnimatorBool, false);
            _animator.SetBool(IsBatAnimatorBool, false);
            _spriteRenderer.flipX = false;
        }
    }
}