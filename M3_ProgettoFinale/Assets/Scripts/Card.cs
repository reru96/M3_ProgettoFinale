using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : BaseWeapon
{
    protected override void Fire()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
