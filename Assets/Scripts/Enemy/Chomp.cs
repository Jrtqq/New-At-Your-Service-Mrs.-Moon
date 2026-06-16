using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Chomp : MonoBehaviour
    {
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private SpriteRenderer _chompRenderer;
        [SerializeField] private Transform _player;
        [SerializeField] private float _renderDistance = 3;
        [SerializeField] private float _maxTransparencyDistance = 1;

        private Player playerReference;

        private void Awake()
        {
            if (_player)
                playerReference = _player.GetComponent<Player>();
        }

        public void SetPlayerManually(Transform player)
        {
            _player = player;
            playerReference = player.GetComponent<Player>();
        }

        private void Update()
        {
            if (playerReference.IsBat == true)
            {
                Color color = _chompRenderer.color;
                color.a = 0;
                _chompRenderer.color = color;
            }
            else
            {
                float d = Vector3.Distance(_player.position, _enemyTransform.position);
                float t = Mathf.Clamp(d, _maxTransparencyDistance, _renderDistance) - _maxTransparencyDistance;

                Color color = _chompRenderer.color;
                color.a = Mathf.Lerp(1, 0, t / (_renderDistance - _maxTransparencyDistance));

                _chompRenderer.color = color;
            }
        }
    }
}
