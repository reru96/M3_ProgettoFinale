using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForward : BaseWeapon
{
    protected override void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}