using EnemyScripts;
using Pathfinding;
using PlayerScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(AIPath))]
public class BossEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _deadPrefab;
    [SerializeField] private LayerMask _playerCastMask;
    [SerializeField] private float _attackRadius = 4;
    [SerializeField] private Attack _attack;

    private Transform _player;
    private Transform _transform;
    private AIDestinationSetter _path;

    private Coroutine _coroutine = null;

    public void Init(Transform player)
    {
        _path = GetComponent<AIDestinationSetter>();

        _player = player;
        _transform = transform;
        _path.target = _player;
    }

    private void Update()
    {
        if (_coroutine == null && Vector3.Distance(_transform.position, _player.position) < _attackRadius && 
            Physics2D.Raycast(_transform.position, _player.position - _transform.position, _attackRadius, _playerCastMask).collider.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(StartAttack());
        }
    }

    public void Die()
    {
        Instantiate(_deadPrefab, _transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator StartAttack()
    {
        _path.target = _transform;
        _attack.Charge();
        yield return new WaitForSeconds(_attack.CastTime - _attack.AttackAlertTime);

        StartCoroutine(_attack.Cast(_player.position));

        yield return new WaitForSeconds(_attack.AttackAlertTime);
        _path.target = _player;
        _coroutine = null;
    }
}
