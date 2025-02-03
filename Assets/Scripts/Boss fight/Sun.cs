using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [Header("Техническое")]
    [SerializeField] private LaserAttack _laserAttack;
    [SerializeField] private SummonAttack _summonAttack;

    [Header("Характеристики")]
    [SerializeField] private float _cooldown = 5;

    private Queue<IBossAttack> _attacks = new();
    private float _timer = 0;

    private bool _isActive = false;

    private void Awake()
    {
        _timer = _cooldown - 1;

        _laserAttack.Init(this);

        _attacks.Enqueue(_laserAttack);
        _attacks.Enqueue(_summonAttack);
    }

    private void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;

            if (_timer >= _cooldown)
            {
                Attack();
                _timer = 0;
            }
        }
    }

    public void StartFight()
    {
        _isActive = true;
    }

    private void Attack()
    {
        IBossAttack current = _attacks.Dequeue();

        current.StartCast();
        _attacks.Enqueue(current);
    }
}
