using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Pen _pen;

    [ContextMenu(nameof(North))]
    private void North()
    {
        _pen.North(1);
    }
    [ContextMenu(nameof(South))]
    private void South()
    {
        _pen.South(1);
    }
    [ContextMenu(nameof(East))]
    private void East()
    {
        _pen.East(1);
    }
    [ContextMenu(nameof(West))]
    private void West()
    {
        _pen.West(1);
    }
}
