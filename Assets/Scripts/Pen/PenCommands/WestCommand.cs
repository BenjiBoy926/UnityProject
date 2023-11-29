using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestCommand : PenCommand
{
    [SerializeField]
    private float _distance;

    public WestCommand(Pen pen, float distance) : base(pen)
    {
        _distance = distance;
    }

    public override void Execute()
    {
        Pen.West(_distance);
    }
}
