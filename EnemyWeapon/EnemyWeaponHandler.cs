using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject EnemyWeapon;

    public void EnemWeaponEnable()
    {
        EnemyWeapon.SetActive(true);
    }

    public void EnemWeaponDisable()
    {
        EnemyWeapon.SetActive(false);
    }
}
