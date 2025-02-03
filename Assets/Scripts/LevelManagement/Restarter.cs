using PlayerScripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restarter : MonoBehaviour
{
    public static int CurrentStage { get; private set; } = 0;

    [SerializeField] private GameObject[] _stages;
    [SerializeField] private StageAnimation _animator;
    [SerializeField] private Player _player;
    [SerializeField] private float _restartdelay = 0.5f;
    [SerializeField] private LevelCompleter _completer;

    public Action Loaded;

    private void Awake()
    {
        LoadStage();
    }

    private void OnEnable()
    {
        _player.Dead += StartRestart;
    }

    private void OnDisable()
    {
        _player.Dead -= StartRestart;
    }

    public static void ResetStageCounter()
    {
        CurrentStage = 0;
    }

    public void GoToNextStage()
    {
        if (CurrentStage < _stages.Length - 1)
        {
            StartCoroutine(
                _animator.FadeIn(_stages[CurrentStage].transform, () => LoadStage()));

            CurrentStage++;
        }
        else
        {
            _completer.gameObject.SetActive(true);
        }
    }

    private void LoadStage()
    {
        _player.transform.position = Vector3.zero;
        StartCoroutine(_animator.FadeOut(_stages[CurrentStage].transform, () => Loaded?.Invoke()));
    }

    private void StartRestart()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(_restartdelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
