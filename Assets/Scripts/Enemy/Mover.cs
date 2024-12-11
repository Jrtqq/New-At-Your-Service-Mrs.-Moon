using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace EnemyScripts
{
    [Serializable]
    public class Mover
    {
        [SerializeField] private Transform[] _waypointsArray;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;

        private Queue<Transform> _waypoints;
        public Vector3 CurrentPoint { get; private set; }

        public void Init()
        {
            _waypoints = new(_waypointsArray);

            if (_waypoints.Count < 1 || _waypoints == null)
            {
                _waypoints = new();
                CurrentPoint = _transform.position;
            }
            else
            {
                CurrentPoint = _waypoints.Dequeue().position;
            }
        }

        public void Move(Vector3 target)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(target - _transform.position, 1) * _speed * Time.deltaTime;
        }

        public IEnumerator OnPointReaching(Transform point)
        {
            if (_waypoints.Count > 0 && point.position == CurrentPoint)
            {
                CurrentPoint = _transform.position;

                yield return new WaitForSeconds(2);

                _waypoints.Enqueue(point);
                CurrentPoint = _waypoints.Dequeue().position;
            }
        }
    }
}