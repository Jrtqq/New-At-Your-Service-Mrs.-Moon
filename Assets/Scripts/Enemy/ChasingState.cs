using Pathfinding;
using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class ChasingState : IState
    {
        private const float AttackDistance = 10;

        private AIDestinationSetter _mover;
        private Transform _position;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public ChasingState(AIDestinationSetter mover, Transform enemy, PlayerSearcher playerSearcher, IStateController stateController)
        {
            _mover = mover;
            _playerSearcher = playerSearcher;
            _stateController = stateController;

            _position = enemy;
        }

        public void Enter()
        {
            _playerSearcher.Lost += SearchPlayer;

            _mover.target = _playerSearcher.Player.transform;
        }

        public void Exit()
        {
            _playerSearcher.Lost -= SearchPlayer;

            _mover.target = _position;
        }

        public void FixedUpdate()
        {
            if (Vector3.Distance(_position.position, _mover.target.position) < AttackDistance)
            {
                _stateController.Switch<AttackingState>();
            }
        }

        private void SearchPlayer() => _stateController.Switch<SearchingState>();
    }
}