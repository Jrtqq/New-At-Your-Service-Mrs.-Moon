using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public class AttackingState : IState
    {
        private Attack _attack;
        private Transform _transform;
        private Transform _playerTransform;
        private PlayerSearcher _playerSearcher;
        private IStateController _stateController;

        public AttackingState(Transform transform, PlayerSearcher playerSearcher, IStateController stateController, Attack attack)
        {
            _playerSearcher = playerSearcher;
            _stateController = stateController;
            _attack = attack;

            _transform = transform;
        }

        public void Enter()
        {
            _playerTransform = _playerSearcher.Player.transform;
            _playerSearcher.StartCoroutine(Cast());
        }

        public void Exit() { }

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

            if (Vector3.Distance(_transform.position, _playerTransform.position) <= _attack.MeleeAttackDistance)
            {
                _playerSearcher.StartCoroutine(_attack.CastMeleeAttack());
            }
            else
            {
                _playerSearcher.StartCoroutine(_attack.CastRangeAttack(_playerTransform.position));
            }

            yield return new WaitForSeconds(_attack.AttackAlertTime);
            SwitchState();
        }
    }
}