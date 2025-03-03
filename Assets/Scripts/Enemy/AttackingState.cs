using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class AttackingState : IState
    {
        private Attack _attack;
        private Transform _playerTransform;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        private Coroutine _coroutine = null;

        public AttackingState(PlayerSearcher playerSearcher, IStateController stateController, Attack attack)
        {
            _playerSearcher = playerSearcher;
            _stateController = stateController;
            _attack = attack;
        }

        public void Enter()
        {
            _playerTransform = _playerSearcher.Player.transform;

            _coroutine = _playerSearcher.StartCoroutine(Cast());
        }

        public void Exit() 
        { 
            if (_coroutine != null)
                _playerSearcher.StopCoroutine(_coroutine);
        }

        public void FixedUpdate() { }

        private void SwitchState()
        {
            if (_playerSearcher.Player == null)
            {
                _stateController.Switch<SearchingState>();
            }
            else
            {
                _stateController.Switch<ChasingState>();
            }
        }

        private IEnumerator Cast()
        {
            _attack.Charge();
            yield return new WaitForSeconds(_attack.CastTime - _attack.AttackAlertTime);
            
            _playerSearcher.StartCoroutine(_attack.Cast(_playerTransform.position));

            yield return new WaitForSeconds(_attack.AttackAlertTime);
            SwitchState();
        }
    }
}