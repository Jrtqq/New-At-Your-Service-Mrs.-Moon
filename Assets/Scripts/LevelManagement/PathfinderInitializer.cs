using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class PathfinderInitializer : MonoBehaviour
{
    [SerializeField] private AstarPath _path;
    [SerializeField] private TilemapCollider2D[] _holes;
    [SerializeField] private Restarter _stageLoader;

    private void OnEnable()
    {
        _stageLoader.Loaded += Scan;
    }

    private void OnDisable()
    {
        _stageLoader.Loaded -= Scan;
    }

    private void Scan()
    {
        for (int i = 0; i < _holes.Length; i++)
        {
            _holes[i].enabled = true;
        }

        _path.Scan();

        for (int i = 0; i < _holes.Length; i++)
        {
            _holes[i].enabled = false;
        }
    }
}
