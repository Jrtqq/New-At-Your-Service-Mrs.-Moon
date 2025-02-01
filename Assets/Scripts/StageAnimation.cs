using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Image _fade;

    private float _currentTime;
    private float _totalTime;
    private float _max;
    private Vector3 _startPosition;

    public IEnumerator FadeOut(Transform stage, Action callback)
    {
        stage.gameObject.SetActive(true);

        _currentTime = 0;
        _totalTime = _curve.keys[^1].time;

        _startPosition = stage.position;
        _max = _curve.Evaluate(_totalTime);

        stage.position -= Vector3.up * _max;
        Vector3 temp = stage.position;

        while (_currentTime < _totalTime)
        {
            stage.position = temp + Vector3.up * _curve.Evaluate(_currentTime);
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _max - _curve.Evaluate(_currentTime));

            _currentTime += Time.deltaTime;
            yield return null;
        }

        stage.position = _startPosition;
        callback?.Invoke();
    }

    public IEnumerator FadeIn(Transform stage, Action callback)
    {
        _currentTime = 0;
        _totalTime = _curve.keys[^1].time;

        _startPosition = stage.position;

        while (_currentTime < _totalTime)
        {
            stage.position = _startPosition - Vector3.up * _curve.Evaluate(_currentTime);
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _curve.Evaluate(_currentTime));

            _currentTime += Time.deltaTime;
            yield return null;
        }

        stage.gameObject.SetActive(false);
        callback?.Invoke();
    }
}
