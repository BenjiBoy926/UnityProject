using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    [SerializeField]
    private float _width = 1;
    [SerializeField]
    private bool _isUp = false;
    [SerializeField]
    private Stroke _currentStroke;

    public event System.Action<Stroke> Stroke = delegate { };

    public void SetWidth(float width)
    {
        _width = width;
    }
    public void PenUp()
    {
        _isUp = true;
    }
    public void PenDown()
    {
        _isUp = false;
    }
    public void North(float distance)
    {
        Move(Vector3.up * distance);
    }
    public void South(float distance)
    {
        Move(Vector3.down * distance);
    }
    public void East(float distance)
    {
        Move(Vector3.right * distance);
    }
    public void West(float distance)
    {
        Move(Vector3.left * distance);
    }

    private void Move(Vector3 offset)
    {
        _currentStroke = new Stroke(_width, _isUp, transform.position, transform.position + offset);
        transform.position += offset;
        Stroke(_currentStroke);
    }
}
