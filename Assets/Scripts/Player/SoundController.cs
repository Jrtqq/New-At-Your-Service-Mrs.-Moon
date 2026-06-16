using System;
using UnityEngine;

namespace PlayerScripts
{
    [Serializable]
    public class SoundController
    {
        [Header("Sounds")]
        [SerializeField] private AudioClip _dash;
        [SerializeField] private AudioClip _transform;
        [SerializeField] private AudioClip _death;
        [Header("Dependencies")]
        [SerializeField] private AudioSource _audioSource;

        public void Init()
        {
            if (_audioSource == null)
                Debug.LogError("SpriteRenderer is null");
        }

        public void OnMoveStart(bool isBat)
        {
            if (isBat)
                return;

            _audioSource.loop = true;

            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
        }

        public void OnMoveEnd()
        {
            _audioSource.loop = false;
        }

        public void OnTransform(bool isBat, bool isMoving)
        {
            if (isBat)
            {
                _audioSource.loop = false;
            }
            else
            {
                if (isMoving)
                {
                    _audioSource.loop = true;
                    _audioSource.Play();
                }
            }

            _audioSource.PlayOneShot(_transform);
        }

        public void OnDash()
        {
            _audioSource.PlayOneShot(_dash);
        }

        public void OnDeath()
        {
            _audioSource.PlayOneShot(_death);
        }
    }
}