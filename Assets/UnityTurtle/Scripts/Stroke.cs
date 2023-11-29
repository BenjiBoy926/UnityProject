using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stroke
{
    public float Width => _width;
    public bool IsUp => _isUp;
    public Vector3 Start => _start;
    public Vector3 End => _end;

    [SerializeField]
    private float _width;
    [SerializeField]
    private bool _isUp;
    [SerializeField]
    private Vector3 _start;
    [SerializeField]
    private Vector3 _end;

    public Stroke(float width, bool isUp, Vector3 start, Vector3 end)
    {
        _width = width;
        _isUp = isUp;
        _start = start;
        _end = end;
    }
}
