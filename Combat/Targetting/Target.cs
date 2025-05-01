using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyedEvent;

    private void OnDestroy()
    {
        OnDestroyedEvent?.Invoke(this);
    }
}
