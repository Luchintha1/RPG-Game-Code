using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public Vector2 MovementValue { get; private set; }

    private Controls controls;

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public bool IsAttacking { get; private set; }

    public bool IsBlocking { get; private set; }

    public bool IsDanceing { get; private set; }

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();

    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    void Controls.IPlayerActions.OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();

    }

    void Controls.IPlayerActions.OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        MovementValue = context.ReadValue<Vector2>();

    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnTargetLock(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if(context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsDanceing = true;
        }
        else if(context.canceled)
        {
            IsDanceing = false;
        }
    }

}
