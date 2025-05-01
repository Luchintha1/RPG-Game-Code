using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private CharacterController Controller;

    private Collider[] allcolliders;

    private Rigidbody[] allrigidbodies;

    void Start()
    {
        allcolliders = GetComponentsInChildren<Collider>(true);
        allrigidbodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allcolliders)
        {
            if (collider.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }

        }

        foreach(Rigidbody rigidbody in allrigidbodies)
        {
            if (rigidbody.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        animator.enabled = !isRagdoll;
        Controller.enabled = !isRagdoll;

    }
    
}
