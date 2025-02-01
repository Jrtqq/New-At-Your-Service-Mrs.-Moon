using System;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerSearcher : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _playerCastMask;

        private Transform _privatePlayer = null;
        private bool _isPlayerInRadius = false;
        private Transform _transform;

        [field: SerializeField] public Transform LastPlayerPosition { get; private set; }
        public Transform Player { get; private set; } = null;

        public Action Spotted;
        public Action Lost;

        private void Awake()
        {
            if (_radius > 0)
                GetComponent<CircleCollider2D>().radius = _radius;

            _transform = transform;

            LastPlayerPosition.parent = null;
        }

        private void Update()
        {
            if (_isPlayerInRadius)
            {
                CheckIfPlayerBehindWalls();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(PlayerTag))
            {
                if (_privatePlayer == null)
                    _privatePlayer = collision.transform;

                _isPlayerInRadius = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(PlayerTag))
            {
                LastPlayerPosition.position = _privatePlayer.position;

                _isPlayerInRadius = false;
                Player = null;
            }
        }

        private void CheckIfPlayerBehindWalls()
        {
            RaycastHit2D hit = Physics2D.Raycast(_transform.position, _privatePlayer.position - _transform.position, _radius, _playerCastMask);

            if (hit.collider != null)
            {
                if (Player == null && hit.collider.CompareTag(PlayerTag) == true)
                {
                    Player = _privatePlayer;
                    Spotted?.Invoke();
                }
                else if (Player != null && hit.collider.CompareTag(PlayerTag) != true)
                {
                    LastPlayerPosition.position = _privatePlayer.position;

                    Player = null;
                    Lost?.Invoke();
                }
            }
        }
    }
}