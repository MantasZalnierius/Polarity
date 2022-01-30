using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStateMachine : FiniteStateMachine
{
    public Runtime2DMovement movementController;
    [HideInInspector]
    public IdleState idleState;
    public WalkingLeftState movementLeft;
    public WalkingRightState movementRight;
    public JumpingState jumping;
    public State initalState;
    private void Awake()
    {
        idleState = new IdleState(this);
        movementLeft = new WalkingLeftState(this);
        movementRight = new WalkingRightState(this);
        jumping = new JumpingState(this);
        movementController = this.GetComponent<Runtime2DMovement>();
        initalState = idleState;
    }

    protected override State GetInitialState()
    {
        return initalState;
    }

    public void setInitalState(State t_initalState)
    {
        initalState = t_initalState;
    }
}
