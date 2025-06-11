using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : BaseWeapon
{

    public float spreadAngle = 30f;
    private Vector2 shootDirection = Vector2.down;

    protected override void Fire()
    {

        UpdateShootDirection();

        FireBullet(0f);
        FireBullet(+spreadAngle);
        FireBullet(-spreadAngle);
    }

    void UpdateShootDirection()
    {

        shootDirection.x = Input.GetAxisRaw("Horizontal");
        shootDirection.y = Input.GetAxisRaw("Vertical");


        if (shootDirection.sqrMagnitude > 0.1f)
        {
            shootDirection.Normalize();
        }
        else
        {
            shootDirection = Vector2.down;
        }
    }

    void FireBullet(float angleOffset)
    {

        Quaternion spreadRot = Quaternion.Euler(0, 0, angleOffset);
        Vector2 finalDirection = spreadRot * shootDirection;
        float angle = Mathf.Atan2(finalDirection.y, finalDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);


        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation).GetComponent<Bullet>();
        bullet.dir = finalDirection;
    }
}
