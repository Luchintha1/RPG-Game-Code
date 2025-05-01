using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 closestPoint;
    private Vector3 ledgeFoward;


    public readonly int HangingHash = Animator.StringToHash("Hanging");

    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 closestPoint, Vector3 ledgeFoward) : base(stateMachine) 
    {
        this.closestPoint = closestPoint;
        this.ledgeFoward = ledgeFoward;
    } 

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeFoward, Vector3.up);

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

    }

    public override void Exit()
    {

    }

    
}
