using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseWeapon
{
    public float spreadAngle = 30f;

    protected override void Fire()
    {
        FireBullet(0f);      // Center bullet
        FireBullet(spreadAngle);  // Right spread
        FireBullet(-spreadAngle); // Left spread
    }

    void FireBullet(float angleOffset)
    {
      
        Vector2 baseDirection = firePoint.right;

        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, angleOffset);
        Vector2 shootDirection = spreadRotation * baseDirection;

        float fireAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, fireAngle);

        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation).GetComponent<Bullet>();
        bullet.dir = shootDirection;
    }
}
