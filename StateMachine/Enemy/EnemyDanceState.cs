using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDanceState : EnemyBaseState
{

    public readonly int DanceHash = Animator.StringToHash("Dance");

    private float duration = 10f;

    public EnemyDanceState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DanceHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdelState(stateMachine));
        }

    }
    public override void Exit()
    {
    }

    
}
