using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    [SerializeField] private float _gameOverScreenDiration; //опасно ставить значение больше 0.75, потому что враги почему то будут атаковать после ресета и мне лень это фиксить:(
    [SerializeField] private Object[] _restartableObjects;

    private List<IRestartable> _objects = new();

    private PlayerScripts.Player _restarter;

    private void OnValidate()
    {
        if (_gameOverScreenDiration < 0.5f) _gameOverScreenDiration = 0.5f;
    }

    private void Awake()
    {
        for (int i = 0; i < _restartableObjects.Length; i++)
        {
            _objects.Add(_restartableObjects[i].GetComponent<IRestartable>());
        }

        _restarter = FindObjectOfType<PlayerScripts.Player>();
    }

    private void OnEnable()
    {
        _restarter.Dead += Restart;
    }

    private void OnDisable()
    {
        _restarter.Dead -= Restart;
    }

    public void Restart() => StartCoroutine(ShowGameOver());

    private IEnumerator ShowGameOver()
    {
        //эндскрин
        yield return new WaitForSeconds(_gameOverScreenDiration);

        ResetObjects();
    }

    private void ResetObjects()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].Restart();
        }
    }
}
