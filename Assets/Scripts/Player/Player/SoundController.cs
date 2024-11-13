using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class SoundController
    {
        [Header("Техническое")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _dash;
        [SerializeField] private AudioClip _transform;
        [SerializeField] private AudioClip _death;

        public void Init()
        {
            if (_audioSource == null)
                Debug.LogError("SpriteRenderer is null");
        }

        public void OnMoveStart()
        {
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

        public void OnTransform()
        {
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