using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneFader : MonoBehaviour
{
    private const int IntroSceneIndex = 8;

    [SerializeField] private TMP_Text _textboxAfterFading;
    [SerializeField] private float _textPrintingSpeed = 0.1f;
    [SerializeField] private Image _fade;
    [SerializeField] private float _fadeSpeed = 1;

    private string _textAfterFading = "Level ";

    private void Awake()
    {
        _textAfterFading = $"{_textAfterFading}{Progress.Instance.LastLevel + 1}";
    }

    public IEnumerator Fade()
    {
        while (_fade.color.a < 1)
        {
            _fade.color += new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        var delay = new WaitForSeconds(_textPrintingSpeed);

        for (int i = 0; i < _textAfterFading.Length; i++)
        {
            _textboxAfterFading.text = $"{_textboxAfterFading.text}{_textAfterFading[i]}";
            yield return delay;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        var delay = new WaitForSeconds(_textPrintingSpeed);

        while (_textboxAfterFading.text.Length > 0)
        {
            _textboxAfterFading.text = _textboxAfterFading.text.Remove(_textboxAfterFading.text.Length - 1, 1);

            yield return delay;
        }

        if (Progress.Instance.Cutscenes[Progress.Instance.LastLevel - 1].IsIntroNext)
            SceneManager.LoadScene(IntroSceneIndex);
        else
            SceneManager.LoadScene(Progress.Instance.LastLevel);
    }
}
