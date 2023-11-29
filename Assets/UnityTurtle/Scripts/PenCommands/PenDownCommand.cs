using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenDownCommand : PenCommand
{
    public PenDownCommand(Pen pen) : base(pen) { }

    public override void Execute()
    {
        Pen.PenDown();
    }
}
