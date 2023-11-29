using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _renderer;
    [SerializeField]
    private float _widthScale = 0.1f;
    [SerializeField]
    private Vector3[] _positions = new Vector3[2];

    public void SetStroke(Stroke stroke)
    {
        _renderer.enabled = !stroke.IsUp;
        SetWidth(stroke.Width);
        SetStartAndEndPoints(stroke.Start, stroke.End);
    }
    private void SetWidth(float width)
    {
        _renderer.startWidth = _renderer.endWidth = width * _widthScale;
    }
    private void SetStartAndEndPoints(Vector3 start, Vector3 end)
    {
        _positions[0] = start;
        _positions[1] = end;
        _renderer.SetPositions(_positions);
    }
}
