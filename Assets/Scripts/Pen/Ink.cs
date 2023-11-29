using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    [SerializeField]
    private Pen _pen;
    [SerializeField]
    private Line _lineTemplate;

    private void Awake()
    {
        _pen.Stroke += OnPenStroke;
    }
    private void OnDestroy()
    {
        _pen.Stroke -= OnPenStroke;
    }

    private void OnPenStroke(Stroke obj)
    {
        Line lineInstance = Instantiate(_lineTemplate);
        lineInstance.SetStroke(obj);
    }
}
