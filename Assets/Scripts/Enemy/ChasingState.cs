using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class ChasingState : IState
    {
        private const float AttackDistance = 5;

        private Mover _mover;
        private Transform _transform;
        private Transform _playerTransform;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public ChasingState(Mover mover, Transform transform, PlayerSearcher playerSearcher, IStateController stateController)
        {
            _mover = mover;
            _playerSearcher = playerSearcher;
            _stateController = stateController;

            _transform = transform;
        }

        public void Enter()
        {
            _playerSearcher.Lost += SearchPlayer;

            _playerTransform = _playerSearcher.Player.transform;
        }

        public void Exit()
        {
            _playerSearcher.Lost -= SearchPlayer;
        }

        public void FixedUpdate()
        {
            if (Vector3.Distance(_transform.position, _playerTransform.position) < AttackDistance)
            {
                _mover.Move(_transform.position);
                _stateController.Switch<AttackingState>();
            }
            else
            {
                _mover.Move(_playerTransform.position);
            }
        }

        private void SearchPlayer() => _stateController.Switch<SearchingState>();
    }
}