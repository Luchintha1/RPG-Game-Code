using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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
    protected void FacePlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 playerDirection = stateMachine.Player.transform.position - stateMachine.transform.position;
        playerDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(playerDirection);
    }

    protected bool IsInChaseRange()
    {
        if(stateMachine.Player.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.DetectingRange * stateMachine.DetectingRange;

    }

}
