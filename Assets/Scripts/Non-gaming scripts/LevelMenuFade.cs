using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuFade : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public int NextSceneIndex = 0;
    [HideInInspector] public bool IsFirstLevel = false;

    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private TMP_Text[] _poem;
    [SerializeField] private TMP_Text _skipTitle;

    private Image _image;
    private Color _step;
    private int _currentPoemString = 0;

    private bool _isFading = true;
    private bool _isReadyToSkip = false;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _step = new Color(0, 0, 0, _speed * Time.deltaTime);

        for (int i = _poem.Length - 1; i >= 0; i--)
        {
            _poem[i].gameObject.SetActive(true);
            _poem[i].raycastTarget = false;
        }

        _skipTitle.gameObject.SetActive(true);
        _skipTitle.raycastTarget = false;

        StartCoroutine(Fade());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isFading == false)
        {
            if (_isReadyToSkip)
            {
                SceneManager.LoadScene(NextSceneIndex);
            }
            else
            {
                StartTextFadingOut();
            }
        }
    }

    private IEnumerator Fade()
    {
        _isFading = true;

        while (_image.color.a < 1)
        {
            _image.color += _step;

            yield return null;
        }

        if (IsFirstLevel)
        {
            StartTextFadingOut();
        }
        else
        {
            SceneManager.LoadScene(NextSceneIndex);
        }

        _isFading = false;
    }

    private void StartTextFadingOut()
    {
        if (_currentPoemString >= _poem.Length)
        {
            StartCoroutine(FadeOutText(_skipTitle));
        }
        else
        {
            StartCoroutine(FadeOutText(_poem[_currentPoemString]));
            _currentPoemString++;
        }
    }

    private IEnumerator FadeOutText(TMP_Text text)
    {
        if (text == _skipTitle)
            _isReadyToSkip = true;

        while (text.color.a < 1)
        {
            text.color += _step;

            yield return null;
        }
    }
}
