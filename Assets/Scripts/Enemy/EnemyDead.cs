using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _chompRenderer;
    [SerializeField] private SpriteRenderer _enemyRenderer;

    private void Update()
    {
        if (_enemyRenderer.color.a > 0)
        {
            _enemyRenderer.color -= new Color(0, 0, 0, Time.deltaTime);
            _chompRenderer.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
