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
        [SerializeField] private MonoBehaviour _caster;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _linecastMask;

        [Header("Характеристики")]
        [SerializeField] private float _castTime = 1;
        [SerializeField] private float _attackAlertTime = 0.25f;
        [SerializeField] private float _maxLineLength = 5;
        [SerializeField] private float _fadeSpeed = 2f;

        private Coroutine _fader = null;

        public float CastTime => _castTime;
        public float AttackAlertTime => _attackAlertTime;

        [Header("Функциональные поля (для оптимизации)")]
        private float _remainingLength = 0;

        public void Charge()
        {
            //партиклы
        }

        public IEnumerator Cast(Vector3 target)
        {
            yield return new WaitForSeconds(AttackAlertTime);

            if (_fader != null)
            {
                _caster.StopCoroutine(_fader); 
            }

            ResetLine();

            Vector3[] positions = GetPositions(target);
            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
            //_lineRenderer.Simplify(0.1f);

            _caster.StartCoroutine(FadeAttack());
        }

        private Vector3[] GetPositions(Vector3 target)
        {
            RaycastHit2D hit;
            List<Vector3> positions = new();
            _remainingLength = _maxLineLength;

            target -= _attackPoint.position;
            Vector3 origin = _attackPoint.position;

            positions.Add(origin);

            while (_remainingLength > 0)
            {
                hit = Physics2D.Raycast(origin, target, _remainingLength, _linecastMask);

                if (hit.collider == null)
                {
                    positions.Add(origin + (target.normalized * _remainingLength));
                    _remainingLength = 0;

                    break;
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    positions.Add(hit.point);
                    _remainingLength = 0;

                    hit.collider.GetComponent<PlayerScripts.Player>().Die();
                }
                else if (hit.collider.CompareTag("Mirror"))
                {
                    positions.Add(hit.point);

                    _remainingLength -= hit.distance;
                    origin = hit.point;
                    target = Vector3.Reflect(target, hit.normal);
                    origin += new Vector3(hit.normal.x * 0.02f, hit.normal.y * 0.02f, 0);
                }
                else
                {
                    positions.Add(hit.point);
                    _remainingLength = 0;
                }
            }

            return positions.ToArray();
        }

        private IEnumerator FadeAttack()
        {
            while (_lineRenderer.startColor.a > 0)
            {
                _lineRenderer.startColor -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
                _lineRenderer.endColor -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void ResetLine()
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.startColor += new Color(0, 0, 0, 1);
            _lineRenderer.endColor += new Color(0, 0, 0, 1);
        }
    }
}