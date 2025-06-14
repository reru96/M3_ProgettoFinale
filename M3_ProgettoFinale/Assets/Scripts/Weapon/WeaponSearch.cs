using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSearch : BaseWeapon
{

    protected override void Fire()
    {
      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = FindNearestEnemy(enemies);

        if (nearestEnemy != null)
        {
            Vector2 dir = (nearestEnemy.position - firePoint.position).normalized; 
            Bullet bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.dir = dir;
  
        }
    }

    protected override void Update()
    {
        base.Update();
       
    }


    private Transform FindNearestEnemy(GameObject[] enemies)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            float distance = Vector2.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;

    }
}

