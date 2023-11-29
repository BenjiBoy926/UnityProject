using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenUpCommand : PenCommand
{
    public PenUpCommand(Pen pen) : base(pen) { }

    public override void Execute()
    {
        Pen.PenUp();
    }
}
