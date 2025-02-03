using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyApearance : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        gameObject.transform.parent = null;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (_renderer.color.a > 0)
        {
            _renderer.color -= new Color(0, 0, 0, Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);
    }
}
