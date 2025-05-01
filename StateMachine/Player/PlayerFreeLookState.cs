using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;


        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, 0.1f);
    }

    private void OnTarget()
    {
        if(!stateMachine.targetter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
        }

        if (stateMachine.InputReader.IsDanceing)
        {
            stateMachine.SwitchState(new PlayerDanceState(stateMachine));
        }


        Vector3 movement = CalculateMovement();

        // Direction Relative to the world
        /*movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0f;
        movement.z = stateMachine.InputReader.MovementValue.y;*/

        Move(movement * stateMachine.FreeLookMovmentSpeed, deltaTime);

        //stateMachine.Controller.Move(movement * stateMachine.FreeLookMovmentSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }


    private Vector3 CalculateMovement()
    {
        // Direction Relative to the Camera
        Vector3 foward = stateMachine.MainCameraTransform.forward;
        foward.y = 0f;
        foward.Normalize();

        Vector3 right = stateMachine.MainCameraTransform.right;
        right.y = 0f;
        right.Normalize();

        return foward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;

    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

}

