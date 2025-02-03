using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroFader : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private GameObject _specialText7Level;
    [SerializeField] private float _fadeSpeed = 0.1f;

    public IEnumerator Fade()
    {
        if (Progress.Instance.LastLevel == 6)
        {
            StartCoroutine(ShowSpecialText());
        }
        else
        {
            while (_fade.color.a < 1)
            {
                _fade.color += new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);

                yield return null;
            }

            SceneManager.LoadScene(Progress.Instance.LastLevel + 1);
        }
    }

    private IEnumerator ShowSpecialText()
    {
        _specialText7Level.SetActive(true);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(7);
    }
}
