using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    [Serializable]
    public class ViewController
    {
        private const string IsBatAnimatorBool = "IsBat";
        private const string IsWalkingAnimatorBool = "IsWalking";
        private const string DieAnimatorTrigger = "Die";
        private const string ResetAnimatorTrigger = "Reset";

        [Header("Abilities")]
        [SerializeField] private UIAbilitiesConfig _uiImages;
        [SerializeField] private AbilitiesCooldownConfig _config;
        [SerializeField] private Image _dashCooldownImage;
        [SerializeField] private Image _transformCooldownImage;
        [SerializeField] private Image _characterIcon;
        [Header("CharacterAnimations")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private Coroutine _dashCoroutine;
        private Coroutine _transformCoroutine;

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

            _characterIcon.sprite = isBat ? _uiImages.BatIcon : _uiImages.VampireIcon;

            if (_transformCoroutine != null)
                _transformCooldownImage.StopCoroutine(_transformCoroutine);

            _transformCoroutine = _transformCooldownImage.StartCoroutine(DrawCooldown(false));
        }

        public void OnDash()
        {
            if (_dashCoroutine != null)
                _dashCooldownImage.StopCoroutine(_dashCoroutine);

            _dashCoroutine = _dashCooldownImage.StartCoroutine(DrawCooldown(true));
        }

        public void OnDeath()
        {
            _animator.SetTrigger(DieAnimatorTrigger);

            _characterIcon.sprite = _characterIcon.sprite == _uiImages.VampireIcon ?
                _uiImages.DeadVampireIcon :
                _uiImages.DeadBatIcon;
        }

        private IEnumerator DrawCooldown(bool dash)
        {
            Image target = _transformCooldownImage;
            float cooldown = _config.TransformCooldown;

            if (dash)
            {
                target = _dashCooldownImage;
                cooldown = _config.DashCooldown;
            }

            if (target == null)
                yield break;

            target.sprite = dash ? _uiImages.DashInactive : _uiImages.TransformInactive;
            Image fade = target.transform.GetChild(0).GetComponent<Image>();

            float t = 0;

            while (t < cooldown)
            {
                fade.fillAmount = Mathf.Lerp(1, 0, t / cooldown);

                t += Time.deltaTime;
                yield return null;
            }

            fade.fillAmount = 0;
            target.sprite = dash ? _uiImages.DashActive : _uiImages.TransformActive;
        }
    }
}