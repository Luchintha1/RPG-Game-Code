using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public readonly int JumpFallingHash = Animator.StringToHash("JumpFalling");

    private Vector3 momentum;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpFallingHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.isGrounded)
        {
            ResetState();
        }

        FaceTarget();
    }

    public override void Exit()
    {
    }
    

    /*public void HandleLedgeDetect(Vector3 closestPoint, Vector3 ledgeFoward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeFoward));
    }*/

}
