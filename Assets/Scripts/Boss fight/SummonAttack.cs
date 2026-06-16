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
        int spawnPosIndex;

        for (int i = 0; i < _amount; i++)
        {
            spawnPosIndex = UnityEngine.Random.Range(0, _spawnPositions.Length);
            UnityEngine.Object.Instantiate(_prefab, _spawnPositions[spawnPosIndex].position, Quaternion.identity).Init(_player);
        }
    }
}
