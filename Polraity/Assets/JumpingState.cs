using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    private MovingStateMachine _sm;
    public JumpingState(MovingStateMachine stateMachine) : base("moving", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        handleJumpInput();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(_sm.movementController.leftKey))
        {
            _sm.movementController.setRigidBodyVelocity(_sm.movementController.getVelocity());
            stateMachine.ChangeState(_sm.movementLeft);
        }
        else if (Input.GetKeyDown(_sm.movementController.rightKey))
        {
            _sm.movementController.setRigidBodyVelocity(_sm.movementController.getVelocity());
            stateMachine.ChangeState(_sm.movementRight);
        }
        if (Input.GetKey(_sm.movementController.jumpKey) && _sm.movementController.getIsJumping())
        {
            continuousJump();
        }
        if (Input.GetKeyUp(_sm.movementController.jumpKey))
        {
            _sm.movementController.setIsJumping(false);
        }
        else if (_sm.movementController.getIsGrounded())
        {
            stateMachine.ChangeState(_sm.idleState);
        }
    }

    public void handleJumpInput()
    {
        Vector3 temp = _sm.movementController.getRigidBody().velocity;
        temp = Vector2.up * _sm.movementController.impluseJumpVel; // Impluse megaman into the air by a set amount.
        temp.x = _sm.movementController.getRigidBody().velocity.x;
        _sm.movementController.setRigidBodyVelocity(temp);
        _sm.movementController.setJumpTimeCounter(_sm.movementController.TimeToReachMaxHeight); // reset jumptimecounter.
        _sm.movementController.setIsJumping(true);
        _sm.movementController.setIsGrounded(false);
    }

    public void continuousJump()
    {
        if (_sm.movementController.getJumpTimeCounter() > 0)
        {
            Vector3 temp = _sm.movementController.getRigidBody().velocity;
            temp = Vector2.up * _sm.movementController.impluseJumpVel * 1.3f;
            temp.x = _sm.movementController.getRigidBody().velocity.x;
            _sm.movementController.setRigidBodyVelocity(temp);
            _sm.movementController.setJumpTimeCounter(_sm.movementController.getJumpTimeCounter() - Time.deltaTime);
        }
        else // Else he is falling.
        {
            _sm.movementController.setIsJumping(false);
        }
    }
}