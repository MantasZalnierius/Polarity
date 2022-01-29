using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string name;
    protected FiniteStateMachine stateMachine;

    public State(string name, FiniteStateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}