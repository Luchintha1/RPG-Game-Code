using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdelState : EnemyBaseState
{

    public readonly int EnemyLocomotionHash = Animator.StringToHash("EnemyLocomotion");
    public readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyIdelState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(EnemyLocomotionHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        Move(Time.deltaTime);

        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
    }

    public override void Exit()
    {

    }

    
}
