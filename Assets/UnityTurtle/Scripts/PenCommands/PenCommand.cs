using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PenCommand
{
    protected Pen Pen => _pen;

    [SerializeField]
    private Pen _pen;

    public PenCommand(Pen pen)
    {
        _pen = pen;
    }
    public abstract void Execute();
}
