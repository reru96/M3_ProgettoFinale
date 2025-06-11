using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float fireRange;
    [SerializeField] Bullet _bulletprefab;
    [SerializeField] private Transform firePoint;
    private float nextFireTime = 0.25f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireTime)
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Transform nearestEnemy = FindNearestEnemy(enemies);

            if (nearestEnemy != null)
            {
                Vector2 dir = (nearestEnemy.position - firePoint.position).normalized;
                Shoot(dir);
                nextFireTime = Time.time + fireRate;
            }
        }
    }
    private void Shoot(Vector2 dir)
    {
        Bullet b = Instantiate(_bulletprefab, firePoint.position, firePoint.rotation);
        b.dir = dir;
        Destroy(b.gameObject, fireRange / b.Speed);

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
