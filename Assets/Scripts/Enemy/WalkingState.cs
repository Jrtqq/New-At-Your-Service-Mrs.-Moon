using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class WalkingState : IState
    {
        private Mover _mover;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public WalkingState(Mover mover, PlayerSearcher playerSearcher, IStateController stateController)
        {
            _mover = mover;
            _playerSearcher = playerSearcher;
            _stateController = stateController;
        }

        public void Enter() 
        {
            _playerSearcher.Spotted += StartChase;
        }

        public void Exit()
        {
            _playerSearcher.Spotted -= StartChase;
        }

        public void FixedUpdate()
        {
            _mover.Move(_mover.CurrentPoint);
        }

        private void StartChase() => _stateController.Switch<ChasingState>();
    }
}