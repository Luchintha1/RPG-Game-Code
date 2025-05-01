using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{

    private Vector2 dodgingDirectionInput;
    private float remainingDodgeDuration;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private const float AnimatorDampTime = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;


        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
        }

        if(stateMachine.targetter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        Vector3 movement = TCalculateMovment(Time.deltaTime);
        Move(movement * stateMachine.TargettingMovmentSpeed, deltaTime);

        UpdateAnimations(Time.deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnTarget()
    {
        stateMachine.targetter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCoolDown) { return; }

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeDuration = stateMachine.DodgeDuration;
    }

    protected Vector3 TCalculateMovment(float deltaTime)
    {
        Vector3 movement = new Vector3();

        if(remainingDodgeDuration > 0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            remainingDodgeDuration -= deltaTime;

            if(remainingDodgeDuration < 0f)
            {
                remainingDodgeDuration = 0f;
            }
        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }

        movement.Normalize();

        return movement;
    }

    private void UpdateAnimations(float deltaTime)
    {
        Vector2 MovementValue = stateMachine.InputReader.MovementValue;

        if(MovementValue.y == 0f)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0f, AnimatorDampTime, deltaTime);
        }
        else
        {
            float value = MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, AnimatorDampTime, deltaTime);
        }

        if(MovementValue.x == 0f)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0f, AnimatorDampTime, deltaTime);
        }
        else
        {
            float value = MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, AnimatorDampTime, deltaTime);

        }
    }

    
}
