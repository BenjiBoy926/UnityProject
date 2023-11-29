using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthCommand : PenCommand
{
    [SerializeField]
    private float _distance;

    public SouthCommand(Pen pen, float distance) : base(pen)
    {
        _distance = distance;
    }

    public override void Execute()
    {
        Pen.South(_distance);
    }
}
