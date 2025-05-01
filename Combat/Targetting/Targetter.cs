using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targetter : MonoBehaviour
{
    private List<Target> targets = new List<Target>();

    public Target currentTarget { get; private set; }
    private Camera mainCamera;

    [SerializeField] private CinemachineTargetGroup cinemaTargetingGroup;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if(target == null) { return; }

        target.OnDestroyedEvent += RemoveTarget;

        targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if(target == null) { return; }

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target tartget in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(tartget.transform.position);

            if (!tartget.GetComponentInChildren<Renderer>().isVisible) { continue; }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);

            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = tartget;
                closestTargetDistance = toCenter.sqrMagnitude;
            }

        }

        if(closestTarget == null) { return false; }

        currentTarget = closestTarget;

        cinemaTargetingGroup.AddMember(currentTarget.transform, 1f, 2f);
        return true;

    }
    
    public void CancelTarget()
    {
        if(currentTarget == null) { return; }

        cinemaTargetingGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    public void RemoveTarget(Target target)
    {
        if(currentTarget == target)
        {
            cinemaTargetingGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }

        target.OnDestroyedEvent -= RemoveTarget;
        targets.Remove(target);
    }
}
