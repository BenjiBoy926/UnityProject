using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}
