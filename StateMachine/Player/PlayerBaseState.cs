using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{

    // Reference to the player

    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.movement) * deltaTime); 
    }

    protected void Move(float deltaTime)
    {
        stateMachine.Controller.Move((stateMachine.ForceReceiver.movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        if(stateMachine.targetter.currentTarget == null) { return; }

        Vector3 targetDirection = stateMachine.targetter.currentTarget.transform.position - stateMachine.transform.position;
        targetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    protected Vector3 CalculateMovment()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        movement.Normalize();

        return movement;
    }

    protected void ResetState()
    {
        if(stateMachine.targetter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
}
