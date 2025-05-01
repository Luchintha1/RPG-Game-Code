using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float DetectingRange { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float AttactRange { get; private set; }
    [field : SerializeField] public EnemyWeaponLogic Weapon { get; private set; }
    [field : SerializeField] public int EnemWeaponDamage { get; private set; }
    [field: SerializeField] public float AttackKnockBack { get; private set; }
    [field : SerializeField] public Health health { get; private set; }
    [field : SerializeField] public Target target { get; private set; }
    [field : SerializeField] public Ragdoll ragdoll { get; private set; }


    public Health Player { get; private set; }

    private void Start()
    {
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        SwitchState(new EnemyIdelState(this));
    }

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
        SwitchState(new EnemyImpactState(this));
    }

    private void OnDead()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectingRange);
    }
}
