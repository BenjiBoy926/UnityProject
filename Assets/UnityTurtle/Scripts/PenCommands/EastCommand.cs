using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastCommand : PenCommand
{
    [SerializeField]
    private float _distance;

    public EastCommand(Pen pen, float distance) : base(pen)
    {
        _distance = distance;
    }

    public override void Execute()
    {
        Pen.East(_distance);
    }
}
