using System;
using PlayerScripts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerSearcher : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _playerCastMask;
        [SerializeField] private Transform _playerPosition;

        private Player _privatePlayer;
        private bool _isPlayerInRadius = false;
        private Transform _transform;

        public Player Player { get; private set; } = null;
        public Vector3 LastPlayerPosition { get; private set; } = Vector3.zero;

        public Action Spotted;
        public Action Lost;

        private void Awake()
        {
            if (_radius > 0)
                GetComponent<CircleCollider2D>().radius = _radius;

            _transform = transform;
            _privatePlayer = _playerPosition.GetComponent<Player>();
        }

        private void Update()
        {
            if (_isPlayerInRadius)
            {
                RaycastHit2D hit = Physics2D.Raycast(_transform.position, (_playerPosition.position - _transform.position), _radius, _playerCastMask);

                if (hit.collider != null)
                {
                    if (Player == null && hit.collider.CompareTag(PlayerTag) == true)
                    {
                        Player = _privatePlayer;
                        Spotted?.Invoke();
                    }
                    else if (Player != null && hit.collider.CompareTag(PlayerTag) != true)
                    {
                        LastPlayerPosition = _playerPosition.position;

                        Player = null;
                        Lost?.Invoke();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(PlayerTag))
            {
                _isPlayerInRadius = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(PlayerTag))
            {
                LastPlayerPosition = _playerPosition.position;

                _isPlayerInRadius = false;
                Player = null;
            }
        }
    }
}