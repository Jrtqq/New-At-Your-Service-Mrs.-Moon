using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_renderer.color.a > 0)
        {
            _renderer.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
