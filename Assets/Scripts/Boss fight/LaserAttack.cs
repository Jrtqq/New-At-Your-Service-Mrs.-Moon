using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LaserAttack : IBossAttack
{
    [SerializeField] private Laser[] _horizontalLasers;
    [SerializeField] private Laser[] _verticalLasers;
    [SerializeField] private float _laserCooldown = 0.3f;

    private Laser[][] _lasers;
    private WaitForSeconds _delay;

    private Sun _caster;

    public void Init(Sun caster)
    {
        _caster = caster;

        _lasers = new[] { _horizontalLasers, _verticalLasers };
        _delay = new WaitForSeconds(_laserCooldown);
    }

    public void StartCast()
    {
        _caster.StartCoroutine(Cast());
    }

    private IEnumerator Cast()
    {
        if (UnityEngine.Random.Range(0, 2) == 1)
            _lasers = _lasers.Reverse().ToArray();

        for (int i = 0; i < _lasers.Length; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
                _lasers[i] = _lasers[i].Reverse().ToArray();

            for (int j = 0; j < _lasers[i].Length; j++)
            {
                _lasers[i][j].StartCoroutine(_lasers[i][j].Cast());

                yield return _delay;
            }
        }
    }
}
