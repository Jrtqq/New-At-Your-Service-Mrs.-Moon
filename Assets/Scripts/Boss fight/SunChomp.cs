using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunChomp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _chomp;
    [SerializeField] private Sprite _collapsedChomp;
    [SerializeField] private float _tickCooldown = 0.7f;

    private float t = 0;

    private void Update()
    {
        if (t > _tickCooldown)
        {
            _renderer.sprite = _renderer.sprite == _chomp ? _collapsedChomp : _chomp;

            t = 0;
        }

        t += Time.deltaTime;
    }
}
