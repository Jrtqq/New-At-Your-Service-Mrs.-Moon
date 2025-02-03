using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraMover : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _halfExtendsBounds;
    [SerializeField] private Transform _player;
    [SerializeField] private float _maxOffsetX = 2;
    [SerializeField] private float _maxOffsetY = 3;
    [SerializeField] private float _targetSize = 6.75f;
    [SerializeField] private float _lerpSpeed = 5;

    private Camera _camera;
    private Transform _transform;
    private float _error = 0.02f;
    private int _zOffset = 10;

    private Vector3 CurrentPlayerOffet => _center.position - _player.position;
    private Vector3 TargetPosition => _center.position - 
        new Vector3(CurrentPlayerOffet.x / _halfExtendsBounds.position.x * _maxOffsetX, 
        CurrentPlayerOffet.y / _halfExtendsBounds.position.y * _maxOffsetY, 
        _zOffset);

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        _halfExtendsBounds.position -= _center.position;
        _lerpSpeed *= Time.deltaTime;
    }

    private void OnEnable()
    {
        StartCoroutine(Resize());
    }

    private void FixedUpdate()
    {
        _transform.position = Vector3.Lerp(_transform.position, TargetPosition, _lerpSpeed);
    }

    private IEnumerator Resize()
    {
        while (_camera.orthographicSize < _targetSize - _error)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetSize, _lerpSpeed);

            yield return null;
        }

        _camera.orthographicSize = _targetSize;
    }
}
