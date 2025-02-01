using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _interpolationFactor = 5;

    private Transform _transform;
    private Vector3 _nextFramePosition;
    private int _zOffset = -10;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _nextFramePosition = Vector3.Lerp(_transform.position, _player.position, _interpolationFactor * Time.deltaTime);
        _transform.position = new Vector3(_nextFramePosition.x, _nextFramePosition.y, _zOffset);
    }
}
