using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWidthCommand : PenCommand
{
    [SerializeField]
    private float _width;

    public SetWidthCommand(Pen pen, float width) : base(pen)
    {
        _width = width;
    }

    public override void Execute()
    {
        Pen.SetWidth(_width);
    }
}
