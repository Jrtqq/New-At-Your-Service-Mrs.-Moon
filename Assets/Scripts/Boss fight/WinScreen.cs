using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private float _firstFadeDuration = 0.5f;
    [SerializeField] private float _secondFadeDuration = 4;

    private Image _image;

    private Coroutine _coroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Animate()
    {
        float t = 0;

        while (t < _firstFadeDuration)
        {
            Color color = _image.color;

            color.a = Mathf.Lerp(0, 1, t / _firstFadeDuration);
            _image.color = color;

            t += Time.deltaTime;
            yield return null;
        }

        t = 0;

        while (t < _secondFadeDuration)
        {
            _image.color = Color.Lerp(Color.white, Color.black, t / _secondFadeDuration);

            t += Time.deltaTime;
            yield return null;
        }

        Progress.Instance.LastLevel++;
        SceneManager.LoadScene(8);
    }
}
