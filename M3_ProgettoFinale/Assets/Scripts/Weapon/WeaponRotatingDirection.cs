using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotatingDirection : BaseWeapon
{
    protected override void Fire()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject scythe = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        scythe.GetComponent<RotatingScythe>().firePoint = firePoint;
    }
}
