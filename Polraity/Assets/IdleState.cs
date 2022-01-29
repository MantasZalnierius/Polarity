using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private MovingStateMachine _sm;

    public IdleState(MovingStateMachine stateMachine) : base("Idle", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKey(_sm.movementController.rightKey))
        {
            _sm.movementController.setRigidBodyVelocity(_sm.movementController.getVelocity());
            stateMachine.ChangeState(_sm.movementRight);
        }
        else if (Input.GetKey(_sm.movementController.leftKey))
        {
            _sm.movementController.setRigidBodyVelocity(_sm.movementController.getVelocity());
            stateMachine.ChangeState(_sm.movementLeft);
        }
        else if (Input.GetKeyDown(_sm.movementController.jumpKey) && _sm.movementController.getIsGrounded())
        {
            _sm.movementController.setIsJumping(true);
            _sm.movementController.setIsGrounded(false);
            stateMachine.ChangeState(_sm.jumping);
        }
        else
        {
            stopLeftAndRightMovement();
        }
    }

    public void stopLeftAndRightMovement()
    {
        Vector2 temp = _sm.movementController.getVelocity();
        if (_sm.movementController.getVelocity().x != 0.0f)
        {
            _sm.movementController.setDeclaration(_sm.movementController.acclearation / _sm.movementController._movementTime * Time.deltaTime); // Declaration = v/t.
            if (_sm.movementController.getVelocity().x < 0.0f)
            {
                temp.x += _sm.movementController.getDeclaration(); // reduce vel by declaration.
                _sm.movementController.setVelocity(temp);

                if (_sm.movementController.getVelocity().x >= -_sm.movementController._LOWEST_WALKING_SPEED)
                {
                    temp = _sm.movementController.getVelocity();
                    temp.x = 0.0f;
                    _sm.movementController.setVelocity(temp);
                }
            }
            else if (_sm.movementController.getVelocity().x > 0.0f)
            {
                temp = _sm.movementController.getVelocity();
                temp.x -= _sm.movementController.getDeclaration(); // reduce vel by declaration.
                _sm.movementController.setVelocity(temp);

                if (_sm.movementController.getVelocity().x <= _sm.movementController._LOWEST_WALKING_SPEED)
                {
                    temp = _sm.movementController.getVelocity();
                    temp.x = 0.0f;
                    _sm.movementController.setVelocity(temp);
                }
            }
            temp.y = _sm.movementController.getRigidBody().velocity.y;
            _sm.movementController.setRigidBodyVelocity(temp);
            _sm.movementController.setVelocity(temp);
        }
    }
}
