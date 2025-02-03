using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class WalkingState : IState
    {
        private AIDestinationSetter _mover;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        private Queue<Transform> _waypoints;
        private Transform _position;
        private Coroutine _coroutine = null;

        private Transform _currentPoint = null;
        private float _endReachedDistance = 0.1f;
        private float _pointChangeDelay = 0.5f;
        private bool _isStatic = false;
        private bool _isCurrentlyActive = true;

        public WalkingState(Transform enemy, AIDestinationSetter mover, PlayerSearcher playerSearcher, Transform[] waypoints, IStateController stateController)
        {
            _mover = mover;
            _playerSearcher = playerSearcher;
            _stateController = stateController;
            _position = enemy;


            if (waypoints == null || waypoints.Length <= 0)
            {
                _waypoints = new(0);
                _isStatic = true;
            }
            else
            {
                _waypoints = new(waypoints);
                _isStatic = false;
            }
        }

        public void Enter() 
        {
            _playerSearcher.Spotted += StartChase;

            if (_isStatic)
            {
                _mover.target = _position;
            }
            else
            {
                _mover.target = _waypoints.Dequeue();
                _currentPoint = _mover.target;
            }

            _isCurrentlyActive = true;
        }

        public void Exit()
        {
            _playerSearcher.Spotted -= StartChase;

            if (_isStatic == false)
            {
                _waypoints.Enqueue(_currentPoint);
                _mover.target = _position;
            }

            if (_coroutine != null)
                _playerSearcher.StopCoroutine(_coroutine);

            _isCurrentlyActive = false;
        }

        public void FixedUpdate()
        {
            if (_isStatic == false && _isCurrentlyActive && Vector3.Distance(_mover.target.position, _position.position) < _endReachedDistance)
            {
                _coroutine = _playerSearcher.StartCoroutine(OnPointReaching());
            }
        }

        private void StartChase() => _stateController.Switch<ChasingState>();

        public IEnumerator OnPointReaching()
        {
            if (_waypoints.Count > 0)
            {
                _mover.target = _position;
                _isCurrentlyActive = false;

                yield return new WaitForSeconds(_pointChangeDelay);

                _waypoints.Enqueue(_currentPoint);
                _mover.target = _waypoints.Dequeue();
                _currentPoint = _mover.target;

                _isCurrentlyActive = true;
            }

            _coroutine = null;
        }
    }
}