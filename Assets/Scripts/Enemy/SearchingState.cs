using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class SearchingState : IState
    {
        private float _searchingTime = 2;
        private float _timer;

        private AIDestinationSetter _mover;
        private Transform _position;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public SearchingState(AIDestinationSetter mover, Transform enemy, PlayerSearcher playerSearcher, IStateController stateController)
        {
            _playerSearcher = playerSearcher;
            _stateController = stateController;
            _mover = mover;

            _position = enemy;
        }

        public void Enter()
        {
            _timer = _searchingTime;
            _mover.target = _playerSearcher.LastPlayerPosition;

            _playerSearcher.Spotted += BackToChasing;
        }

        public void Exit()
        {
            _playerSearcher.Spotted -= BackToChasing;

            _mover.target = _position;
        }

        public void FixedUpdate()
        {
            if (Vector3.Distance(_position.position, _playerSearcher.LastPlayerPosition.position) < 0.5f)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    _stateController.Switch<WalkingState>();
                }
            }
        }

        private void BackToChasing() => _stateController.Switch<ChasingState>();
    }
}