using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterControl;
    [SerializeField] private NavMeshAgent Agent;

    private float verticalVelocity;
    private Vector3 Impact;

    private Vector3 dampingVelocity;
    [SerializeField] public float drag = 0.3f;

    public Vector3 movement => Impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if(verticalVelocity < 0 && characterControl.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Impact = Vector3.SmoothDamp(Impact, Vector3.zero, ref dampingVelocity, drag);

        if(Agent != null)
        {
            if(Impact.sqrMagnitude < 0.2f * 0.2f)
            {
                Impact = Vector3.zero;
                Agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        Impact += force;

        if(Agent != null)
        {
            Agent.enabled = false;
        }
    }

    public void Jump(float JumpForce)
    {
        verticalVelocity += JumpForce;
    }
}
