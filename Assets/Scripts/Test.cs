using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private LayerMask _linecastMask;

    [SerializeField]private float _minRayDinstance = 0.1f;
    [SerializeField]private float _offset = 0.1f;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _lineRenderer.SetPositions(GetPositions(_player.position));
        }
    }

    private Vector3[] GetPositions(Vector3 target)
    {
        RaycastHit2D hit;
        List<Vector3> positions = new();
        //float _remainingLenght = _maxLineLength;

        //target -= transform.position;
        //Vector3 origin = transform.position;

        //positions.Add(origin);

        //while (_remainingLenght > 0)
        //{
        //    hit = Physics2D.Raycast(origin, target, _remainingLenght, _linecastMask);

        //    if (hit.collider == null)
        //    {
        //        positions.Add(origin + (target.normalized * _remainingLenght));
        //        break;
        //    }
        //    else
        //    {
        //        _remainingLenght -= hit.distance;

        //        if (hit.distance < _minRayDinstance)
        //        {
        //            Vector3 perp = Vector3.Cross(hit.normal, Vector3.forward);

        //            origin = new Vector3(hit.normal.x, hit.normal.y, 0) + perp * Mathf.Sign(Vector3.Dot(perp, target)) * (_minRayDinstance - hit.distance);
        //        }

        //        positions.Add(hit.point);

        //        origin = hit.point;
        //        target = Vector3.Reflect(target, hit.normal);
        //        origin += new Vector3(hit.normal.x * 0.02f, hit.normal.y * 0.02f, 0);
        //    }
        //}
        target -= transform.position;
        Vector3 temp;
        Vector3 perp = Vector3.zero;

        positions.Add(transform.position);


        hit = Physics2D.Raycast(transform.position, target, math.INFINITY, _linecastMask);
        Debug.Log(hit.normal);
        temp = hit.point + hit.normal * _offset;
        positions.Add(temp);

        hit = Physics2D.Raycast(temp, Vector3.Reflect(target, hit.normal), math.INFINITY, _linecastMask);
        temp = hit.point + hit.normal * _offset;

        if (hit.distance < _minRayDinstance)
        {
            perp = Vector3.Cross(new Vector3(Mathf.Abs(hit.normal.x), Mathf.Abs(hit.normal.y)), Vector3.forward);
            Debug.Log("fixing");
            temp += perp * _minRayDinstance * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(perp, new Vector3(hit.point.x, hit.point.y) - positions[^1])) * Mathf.Sign(Vector3.Dot(perp, new Vector3(hit.point.x, hit.point.y) - positions[^1]));
        }
        Debug.Log(Vector3.Distance(temp, positions[^1]));
        positions.Add(temp);


        return positions.ToArray();
    }

    private void OnDrawGizmos()
    {
        Vector3[] positions = GetPositions(_player.position);

        Gizmos.color = Color.blue;

        for (int i = 0; i < positions.Length; i++)
        {
            Gizmos.DrawSphere(positions[i], 0.01f);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawLine(positions[0], positions[1]);
        Gizmos.DrawLine(positions[1], positions[2]);
    }
}
