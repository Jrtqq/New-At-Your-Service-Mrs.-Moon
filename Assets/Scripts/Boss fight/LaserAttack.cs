using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class LaserAttack : IBossAttack
{
    [SerializeField] private Laser[] _horizontalLasers;
    [SerializeField] private Laser[] _verticalLasers;
    [SerializeField] private float _laserCooldown = 0.3f;

    private WaitForSeconds _delay;

    private Sun _caster;

    public void Init(Sun caster)
    {
        _caster = caster;

        _delay = new WaitForSeconds(_laserCooldown);
    }

    public void StartCast()
    {
        _caster.StartCoroutine(Cast());
    }

    private IEnumerator Cast()
    {
        Laser[] lasers;

        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            lasers = _horizontalLasers;
        }
        else
        {
            lasers = _verticalLasers;
        }

        lasers = lasers.ToArray();

        if (UnityEngine.Random.Range(0, 2) == 1)
            Array.Reverse(lasers);

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].StartCoroutine(lasers[i].Cast());

            yield return _delay;
        }
    }
}
