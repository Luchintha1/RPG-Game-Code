using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{

    //private float previousFrameTime = 0f;
    private Attack attack;

    private bool alreadyAplliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine) 
    {
        attack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        stateMachine.WeaponDamage.SetAttack(attack.Damage, attack.KnockbBack);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizeTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizeTime < 1f)
        {
            if (normalizeTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizeTime);
            }
            
        }
        else
        {
            ResetState();

            /*if (stateMachine.targetter.currentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }*/
        }

        //previousFrameTime = normalizeTime;

    }

    public override void Exit()
    {
    }

    private void TryComboAttack(float normalizeTime)
    {
        if(attack.ComboAttackIndex == -1) {  return;}

        if(normalizeTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboAttackIndex));
    }

    private void TryApplyForce()
    {

        if(alreadyAplliedForce == true) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAplliedForce = true;
    }
           
}
