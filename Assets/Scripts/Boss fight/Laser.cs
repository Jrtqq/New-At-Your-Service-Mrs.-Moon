using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(BoxCollider2D))]
public class Laser : MonoBehaviour
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private ContactFilter2D _playerMask;
    [SerializeField] private float _fadeSpeed = 0.4f;
    [SerializeField] private float _prepareTime = 0.25f;
    [SerializeField] private float _preparingWidth = 0.5f;
    [SerializeField] private float _hitWidth = 2f;
    [SerializeField] private Color _prepareColor;
    [SerializeField] private Color _hitColor;

    private LineRenderer _lineRenderer;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator Cast()
    {
        DrawPreparingLine();

        yield return new WaitForSeconds(_prepareTime);

        TryHitPlayer();
    }

    public void TryHitPlayer()
    {
        DrawHitLine();

        Collider2D[] result = new Collider2D[1];
        Physics2D.OverlapCollider(_collider, _playerMask, result);

        if (result[0] != null)
            result[0].GetComponent<PlayerScripts.Player>().Die();
    }

    private void DrawPreparingLine()
    {
        ResetLine();

        _lineRenderer.widthMultiplier = _preparingWidth;
        _lineRenderer.startColor = _prepareColor;
        _lineRenderer.endColor = _prepareColor;

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { _firstPoint.position, _secondPoint.position });
    }

    private void DrawHitLine()
    {
        ResetLine();

        _lineRenderer.widthMultiplier = _hitWidth;
        _lineRenderer.startColor = _hitColor;
        _lineRenderer.endColor = _hitColor;

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { _firstPoint.position, _secondPoint.position});

        StartCoroutine(FadeLine());
    }

    private IEnumerator FadeLine()
    {
        while (_lineRenderer.startColor.a > 0)
        {
            _lineRenderer.startColor -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);
            _lineRenderer.endColor -= new Color(0, 0, 0, _fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void ResetLine()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.widthMultiplier = 1;
        _lineRenderer.startColor += new Color(0, 0, 0, 1);
        _lineRenderer.endColor += new Color(0, 0, 0, 1);
    }
}