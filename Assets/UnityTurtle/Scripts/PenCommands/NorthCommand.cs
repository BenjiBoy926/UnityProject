using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthCommand : PenCommand
{
    [SerializeField] 
    private float _distance; 
    
    public NorthCommand(Pen pen, float distance) : base(pen) 
    { 
        _distance = distance; 
    }
    
    public override void Execute()
    {
        Pen.North(_distance);
    }
}
