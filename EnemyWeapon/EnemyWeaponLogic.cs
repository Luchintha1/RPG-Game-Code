using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponLogic : MonoBehaviour
{
    [SerializeField] private Collider enemyCollider;

    private List<Collider> alreadyBeenHit = new List<Collider>();

    private int damage;
    private float knockBack;

    private void OnEnable()
    {
        alreadyBeenHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == enemyCollider) { return; }

        if (alreadyBeenHit.Contains(other)) { return; }
        alreadyBeenHit.Add(other);

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - enemyCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockBack);
        }

    }

    public void SetAttack(int damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }

}
