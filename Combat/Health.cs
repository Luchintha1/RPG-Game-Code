using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public event Action ImpactEvent;

    public event Action OnDieEvent;

    private bool isInVulnerable;

    public bool IsDead => currentHealth == 0;

    public void SettingInVulnerable(bool isinValurable)
    {
        this.isInVulnerable = isinValurable;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if(currentHealth == 0) { return; }

        if(isInVulnerable) { return; }

        currentHealth -= damage;

        ImpactEvent?.Invoke();


        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            OnDieEvent?.Invoke();
        }

            Debug.Log(currentHealth);

        
    }
}
