using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    public readonly int AttackeHash = Animator.StringToHash("AttackAnimation");

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.EnemWeaponDamage, stateMachine.AttackKnockBack);
        stateMachine.Animator.CrossFadeInFixedTime(AttackeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        FacePlayer();

        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

    }

    public override void Exit()
    {
    }

    
}
