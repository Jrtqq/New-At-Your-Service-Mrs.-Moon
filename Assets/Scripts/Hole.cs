using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

public class Hole : MonoBehaviour
{
    private bool _isActive = false;
    private Player _player;
    private Rigidbody2D _playerRigidbody;

    private void Update()
    {
        if (_isActive && _player.IsBat == false && _playerRigidbody.velocity.magnitude < _player.VampireSpeed)
        {
            _player.Die();
            _isActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = player;
            _isActive = true;

            if (_playerRigidbody == null)
                _playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = null;
            _isActive = false;
        }
    }
}
