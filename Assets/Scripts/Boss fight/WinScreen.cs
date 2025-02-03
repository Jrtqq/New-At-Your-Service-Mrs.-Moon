using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;

    private Image _image;

    private Color _step;
    private Color _secondStep;

    private void Awake()
    {
        _step = new(0, 0, 0, Time.deltaTime * _speed);
        _secondStep = new(Time.deltaTime * _speed, Time.deltaTime * _speed, Time.deltaTime * _speed, 0);
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (_image.color.a < 1)
        {
            _image.color += _step;
            yield return null;
        }
        while (_image.color.r > 0)
        {
            _image.color -= _secondStep;
            yield return null;
        }

        Progress.Instance.LastLevel++;
        SceneManager.LoadScene(8);
    }
}
