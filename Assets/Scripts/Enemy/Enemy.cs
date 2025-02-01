using System.Collections;
using Pathfinding;
using System.Linq;
using UnityEngine;
using PlayerScripts;
using System;

namespace EnemyScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof (AIPath))]
    public class Enemy : MonoBehaviour, IStateController
    {
        [SerializeField] private GameObject _deadPrefab;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private AIDestinationSetter _mover;
        [SerializeField] private PlayerSearcher _playerSearcher;
        [SerializeField] private Attack _attack;

        private IState[] _states;
        private IState _currentState;

        private Vector3 _startPosition;

        private Player _player;

        public Action<Transform> Died;
        private bool _isDead = false;

        private void Awake()
        {
            _startPosition = transform.position;

            _states = new IState[]{
                new WalkingState(transform, _mover, _playerSearcher, _waypoints, this),
                new ChasingState(_mover, transform, _playerSearcher, this),
                new SearchingState(_mover, transform, _playerSearcher, this),
                new AttackingState(_playerSearcher, this, _attack)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void Switch<State>() where State : IState
        {
            _currentState.Exit();
            _currentState = _states.Where(x => x is State).FirstOrDefault();
            _currentState.Enter();
        }

        public void Die()
        {
            if (_isDead == false)
            {
                Died?.Invoke(Instantiate(_deadPrefab, transform.position, Quaternion.identity).transform);
                Destroy(gameObject);
            }
        }
    }
}