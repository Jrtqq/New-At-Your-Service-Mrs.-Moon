using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    [Serializable]
    public class Attack
    {
        [Header("Техническое")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private SignalTranslator _signalTranslator;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _linecastMask;

        [Header("Характеристики")]
        [SerializeField] private float _castTime = 1;
        [SerializeField] private float _attackAlertTime = 0.25f;
        [SerializeField] private float _meleeAttackDistance = 3;
        [SerializeField] private float _fadeTime = 1.5f;
        [SerializeField] private float _maxLineLength = 5;

        public float CastTime => _castTime;
        public float AttackAlertTime => _attackAlertTime;
        public float MeleeAttackDistance => _meleeAttackDistance;

        [Header("Функциональные поля (для оптимизации)")]
        private float _remainingLenght = 0;

        public IEnumerator CastRangeAttack(Vector3 target)
        {
            _signalTranslator.Warning();

            yield return new WaitForSeconds(AttackAlertTime);

            Vector3[] positions = GetPositions(target);
            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
            //_lineRenderer.Simplify(0.75f);
        }

        public IEnumerator CastMeleeAttack()
        {
            _signalTranslator.Warning();

            yield return new WaitForSeconds(AttackAlertTime);
        }

        public void Charge()
        {
            Debug.Log("партиклы набора силы");
        }

        private Vector3[] GetPositions(Vector3 target)
        {
            RaycastHit2D hit;
            List<Vector3> positions = new();
            _remainingLenght = _maxLineLength;

            target -= _attackPoint.position;
            Vector3 origin = _attackPoint.position;

            positions.Add(origin);

            while (_remainingLenght > 0)
            {
                hit = Physics2D.Raycast(origin, target, _remainingLenght, _linecastMask);

                if (hit.collider == null)
                {
                    positions.Add(origin + (target.normalized * _remainingLenght));
                    break;
                }
                else
                {
                    positions.Add(hit.point);

                    _remainingLenght -= hit.distance;
                    origin = hit.point;
                    target = Vector3.Reflect(target, hit.normal);
                    origin += new Vector3(hit.normal.x * 0.02f, hit.normal.y * 0.02f, 0);
                }
            }

            return positions.ToArray();
        }
    }
}