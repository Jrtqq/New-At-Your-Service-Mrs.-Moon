using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCounter : MonoBehaviour
{
    [SerializeField] private Transform _parentStage;
    [SerializeField] private EnemyScripts.Enemy[] _enemiesOnStage;
    [SerializeField] private Restarter _restarter;

    private int _counter = 0;

    private void OnEnable()
    {
        for (int i = 0; i < _enemiesOnStage.Length; i++)
        {
            _enemiesOnStage[i].Died += AddDeath;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _enemiesOnStage.Length; i++)
        {
            _enemiesOnStage[i].Died -= AddDeath;
        }
    }

    private void AddDeath(Transform deadEnemy)
    {
        _counter++;
        deadEnemy.parent = _parentStage;

        if (_counter == _enemiesOnStage.Length) 
            FinishStage();
    }

    private void FinishStage()
    {
        _restarter.GoToNextStage();
    }
}
