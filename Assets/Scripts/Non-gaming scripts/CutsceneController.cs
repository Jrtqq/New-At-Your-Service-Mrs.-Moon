using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CutsceneFader _screenAfterCutscene;
    [SerializeField] private IntroFader _introScreenAfterCutscene;
    [SerializeField] private float _printingSpeed = 0.1f;
    [SerializeField] private Sprite _special7LevelSprite;
    [SerializeField] private Image _background;

    private CutsceneConfig _currentConfig;
    private int _dialogNumber = 0;

    private Coroutine _coroutine;
    private PlayerInput _input;

    private void Awake()
    {
        if (Progress.Instance.LastLevel == 7)
        {
            _background.sprite = _special7LevelSprite;
        }

        if (_introScreenAfterCutscene != null)
        {
            switch (Progress.Instance.LastLevel)
            {
                case 0:
                    _currentConfig = Progress.Instance.Intros[0];
                    break;
                case 4:
                    _currentConfig = Progress.Instance.Intros[1];
                    break;
                case 5:
                    _currentConfig = Progress.Instance.Intros[2];
                    break;
                case 6:
                    _currentConfig = Progress.Instance.Intros[3];
                    break;
            }
        }
        else
        {
            _currentConfig = Progress.Instance.Cutscenes[Progress.Instance.LastLevel - 1];
        }

        _input = new PlayerInput();

        TryGoNextText(new UnityEngine.InputSystem.InputAction.CallbackContext());
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Main.AnyKey.performed += TryGoNextText;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Main.AnyKey.performed -= TryGoNextText;
    }

    private void TryGoNextText(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            _text.text = _currentConfig.Text[_dialogNumber - 1];
            _title.text = _currentConfig.Names[_dialogNumber - 1];
            return;
        }
        else if (_currentConfig.Text.Length <= _dialogNumber)
        {
            _input.Disable();

            if (Progress.Instance.LastLevel == 7)
                Application.Quit();
            else
            {
                if (_screenAfterCutscene != null)
                    StartCoroutine(_screenAfterCutscene.Fade());
                else
                    StartCoroutine(_introScreenAfterCutscene.Fade());
            }

            return;
        }

        _text.text = "";
        _coroutine = StartCoroutine(PrintText(_currentConfig.Text[_dialogNumber]));
        _title.text = _currentConfig.Names[_dialogNumber];

        _dialogNumber++;
    }

    private IEnumerator PrintText(string text)
    {
        var delay = new WaitForSeconds(_printingSpeed);

        for (int i = 0; i < text.Length; i++)
        {
            _text.text = $"{_text.text}{text[i]}";
            yield return delay;
        }

        _coroutine = null;
    }
}
