using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [field : SerializeField] public string AnimationName { get; private set; }
    [field : SerializeField] public float TransitionDuration { get; private set; }

    public int ComboAttackIndex = -1;
    [field : SerializeField] public float ComboAttackTime { get; private set; }
    [field : SerializeField] public float ForceTime { get; private set; }
    [field : SerializeField] public float Force { get; private set; }
    [field : SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float KnockbBack { get; private set; }



}
