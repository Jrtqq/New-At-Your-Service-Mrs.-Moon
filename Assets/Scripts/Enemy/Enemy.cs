using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Enemy : MonoBehaviour, IStateController
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private PlayerSearcher _playerSearcher;
        [SerializeField] private Attack _attack;

        private IState[] _states;
        private IState _currentState;
        private void Awake()
        {
            _mover.Init();

            _states = new IState[]{
                new WalkingState(_mover, _playerSearcher, this),
                new ChasingState(_mover, transform, _playerSearcher, this),
                new SearchingState(_mover, transform, _playerSearcher, this),
                new AttackingState(transform, _playerSearcher, this, _attack)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Waypoint"))
                StartCoroutine(_mover.OnPointReaching(collision.transform));
        }

        public void Switch<State>() where State : IState
        {
            _currentState.Exit();
            _currentState = _states.Where(x => x is State).FirstOrDefault();
            _currentState.Enter();
        }
    }
}