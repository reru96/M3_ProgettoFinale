using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMouseShooter : BaseWeapon
{

    public float projectileSpeed = 10f;
    public float rotationOffset = -90f;



    protected override void Update()
    {
        RotateTowardsMouse();
        base.Update();
    }

    protected override void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector3 mousePosition = GetMouseWorldPosition();
        Vector2 shootDirection = (mousePosition - firePoint.position).normalized;


        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);


        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.dir = shootDirection;

            bulletComponent.Speed = projectileSpeed;
        }
        else
        {
            Debug.LogError("Il prefab del proiettile non ha il componente Bullet!");
        }
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

}