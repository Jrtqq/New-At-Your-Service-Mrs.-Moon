using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneStartFade : MonoBehaviour
{
    private Image _image;
    private float _speed = 0.4f;

    private Color _step;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _step = new Color(0, 0, 0, Time.deltaTime * _speed);
    }

    private void Update()
    {
        _image.color -= _step;

        if (_image.color.a == 0)
            Destroy(gameObject);
    }
}
