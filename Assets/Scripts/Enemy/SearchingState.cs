using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class SearchingState : IState
    {
        private float _searchingTime = 2;
        private float _timer;

        private Mover _mover;
        private Transform _transform;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public SearchingState(Mover mover, Transform transform, PlayerSearcher playerSearcher, IStateController stateController)
        {
            _playerSearcher = playerSearcher;
            _stateController = stateController;
            _mover = mover;

            _transform = transform;
        }

        public void Enter()
        {
            _timer = _searchingTime;

            _playerSearcher.Spotted += BackToChasing;
        }

        public void Exit()
        {
            _playerSearcher.Spotted -= BackToChasing;
        }

        public void FixedUpdate()
        {
            if (Vector3.Distance(_transform.position, _playerSearcher.LastPlayerPosition) < 0.5f)
            {
                _mover.Move(_transform.position);
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                    _stateController.Switch<WalkingState>();
            }
            else
            {
                _mover.Move(_playerSearcher.LastPlayerPosition);
            }
        }

        private void BackToChasing() => _stateController.Switch<ChasingState>();
    }
}