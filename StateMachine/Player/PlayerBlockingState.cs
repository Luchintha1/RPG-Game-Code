using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    public readonly int BlockHash = Animator.StringToHash("Block");

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, 0.1f);
        stateMachine.health.SettingInVulnerable(true);
    }
    public override void Tick(float deltaTime)
    {

        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }

        if(stateMachine.targetter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

    }

    public override void Exit()
    {
        stateMachine.health.SettingInVulnerable(false);
    }

    
}
