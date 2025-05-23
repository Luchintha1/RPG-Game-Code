using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    public readonly int ImpactHash = Animator.StringToHash("Impact");

    private float duration = 0.8f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0)
        {
            ResetState();
        }
    }

    public override void Exit()
    {
    }

    
}
