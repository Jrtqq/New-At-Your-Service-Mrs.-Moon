using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDeathCounter : MonoBehaviour
{
    private static readonly Color MaxCounterColor = new(0.6792453f, 0.05446779f, 0.05446779f, 1);
    private static readonly Color ZeroCounterColor = new(0.3843138f, 0.6588235f, 0.6745098f, 1);

    [SerializeField] private Transform _parentStage;
    [SerializeField] private EnemyScripts.Enemy[] _enemiesOnStage;
    [SerializeField] private Restarter _restarter;
    [Header("Visualization")]
    [SerializeField] private TMP_Text _textComponent;
    [SerializeField] private bool shouldKill = true;

    private int _counter = 0;

    private void OnEnable()
    {
        _textComponent.color = shouldKill ? ZeroCounterColor : MaxCounterColor;
        UpdateText();

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

        UpdateText();

        if (_counter == _enemiesOnStage.Length)
        {
            FinishStage();
        }
    }

    private void FinishStage()
    {
        _restarter.GoToNextStage();
    }

    private void UpdateText()
    {
        int max = shouldKill ? _enemiesOnStage.Length : 0;

        _textComponent.text = $"{_counter}/{max}";

        if (_counter == _enemiesOnStage.Length)
        {
            _textComponent.color = MaxCounterColor;
        }
    }
}
