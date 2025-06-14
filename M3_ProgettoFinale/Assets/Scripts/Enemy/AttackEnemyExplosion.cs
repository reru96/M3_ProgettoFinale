using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyExplosion : MonoBehaviour
{
    public float explosionRadius = 3f;
    public int explosionDamage = 5;
    public GameObject explosionEffect;
    public float triggerDistance = 1.5f;

    private Transform player;
    private bool hasExploded = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (hasExploded || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

       
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                LifeController playerHp = hit.GetComponent<LifeController>();
                if (playerHp != null) playerHp.AddHp(-explosionDamage);
            }
        }

      
        Destroy(gameObject);
    }

}
