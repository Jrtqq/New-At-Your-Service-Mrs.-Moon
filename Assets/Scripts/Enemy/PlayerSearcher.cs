using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerSearcher : MonoBehaviour
    {
        public Player Player { get; private set; } = null;
        public Vector3 LastPlayerPosition { get; private set; } = Vector3.zero;

        public Action Spotted;
        public Action Lost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                Player = player;

                Spotted?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                LastPlayerPosition = Player.transform.position;
                Player = null;

                Lost?.Invoke();
            }
        }
    }
}