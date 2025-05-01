using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDanceState : PlayerBaseState
{

    public readonly int DanceHash = Animator.StringToHash("Dance");

    public PlayerDanceState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DanceHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(!stateMachine.InputReader.IsDanceing)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Exit()
    {
    }

    
}
