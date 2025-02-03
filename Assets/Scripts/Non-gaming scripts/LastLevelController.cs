using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _firstLoadPosition;

    public static bool IsFirstLoad = true;

    private void Start()
    {
        if (IsFirstLoad)
        {
            _player.position = _firstLoadPosition.position;
            _camera.position = _firstLoadPosition.position;
        }

        IsFirstLoad = false;
    }
}
