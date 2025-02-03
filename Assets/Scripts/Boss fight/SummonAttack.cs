using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SummonAttack : IBossAttack
{
    [SerializeField] private Transform _player;
    [SerializeField] private BossEnemy _prefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private int _amount;

    public void StartCast()
    {
        for (int i = 0; i < _amount; i++)
        {
            UnityEngine.Object.Instantiate(_prefab, _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)].position, Quaternion.identity).Init(_player);
        }
    }
}
