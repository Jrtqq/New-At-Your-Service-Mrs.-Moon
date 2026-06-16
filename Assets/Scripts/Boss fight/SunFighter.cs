using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunFighter : MonoBehaviour
{
    private static readonly Color MaxCounterColor = new(0.6792453f, 0.05446779f, 0.05446779f, 1);

    [SerializeField] private AudioSource _sunChompSound;
    [SerializeField] private TMP_Text _textCounter;
    [SerializeField] private GameObject _sunChomp;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private float _glowSpeed = 0.7f;

    private SpriteRenderer _renderer;
    private Player _player;

    private float _kills = 0;
    private float _needKills = 15;

    private float _sinValue;

    private bool _neededKillScored = false;

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

                _textCounter.text = $"{_kills}/{_needKills}";

                if (_kills >= _needKills)
                    OnNeededKills();
            }
            else
            {
                _player.Die();
            }
        }
        else if (collision.collider.CompareTag("Sun"))
        {
            if (_neededKillScored && _player.IsDead == false)
            {
                _player.IsDead = true;
                _winScreen.SetActive(true);
                _sunChompSound.Play();
            }
            else
            {
                _player.Die();
            }
        }
    }

    private void OnNeededKills()
    {
        if (_neededKillScored == false)
        {
            _sunChomp.SetActive(true);
            _textCounter.color = MaxCounterColor;

            _neededKillScored = true;
        }
    }
}
