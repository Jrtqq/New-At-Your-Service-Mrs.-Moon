using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFighter : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private float _glowSpeed = 0.7f;

    private SpriteRenderer _renderer;
    private Player _player;

    private float _kills = 0;
    private float _needKills = 15;

    private float _sinValue;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _sinValue = 1 - Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad * _glowSpeed)) * (_kills / _needKills);

        _renderer.color = new Color(1, _sinValue, _sinValue, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out BossEnemy enemy))
        {
            if (_player.IsBat == false)
            {
                enemy.Die();
                _player.SlowDown();
                _kills = Mathf.Clamp(_kills + 1, 0, _needKills);
                _glowSpeed = Mathf.Clamp(_glowSpeed + 1, 0, _needKills);
            }
            else
            {
                _player.Die();
            }
        }
        else if (collision.collider.CompareTag("Sun"))
        {
            if (_kills >= _needKills)
            {
                _player.IsDead = true;
                _winScreen.SetActive(true);
            }
            else
            {
                _player.Die();
            }
        }
    }
}
