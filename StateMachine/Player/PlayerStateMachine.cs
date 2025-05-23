using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Every State of the player is called from here 
public class PlayerStateMachine : StateMachine
{

    [field : SerializeField] public InputReader InputReader { get; private set; }
    [field : SerializeField] public CharacterController Controller { get; private set; }
    [field : SerializeField] public float FreeLookMovmentSpeed { get; private set; }
    [field: SerializeField] public float TargettingMovmentSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Targetter targetter { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }
    [field : SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field : SerializeField] public Attack[] Attacks { get; private set; }
    [field : SerializeField] public WeaponDamage WeaponDamage { get; private set; } 
    [field : SerializeField] public Health health { get; private set; }
    [field: SerializeField] public Ragdoll ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field : SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float DodgeCoolDown { get; private set; }
    [field : SerializeField] public float JumpForce { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    public Transform MainCameraTransform { get; private set; }
    private void OnEnable()
    {
        health.ImpactEvent += ImpactHandler;
        health.OnDieEvent += OnDead;
    }

    private void OnDisable()
    {
        health.ImpactEvent -= ImpactHandler;
        health.OnDieEvent -= OnDead;
    }

    private void ImpactHandler()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void OnDead()
    {
        SwitchState(new PlayerDeadState(this));
    }
    
    public void SetDodgeTime(float dodgeTime)
    {
        PreviousDodgeTime = dodgeTime;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
