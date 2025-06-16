using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2f;
    public int explosionDamage = 20;
    public float triggerDistance = 1.5f;
    public float delayBeforeExplosion = 0.5f;

    [Header("References")]
    public GameObject explosionEffect; 
    public AudioClip explosionSound;
    public Animator anim;

    private Transform player;
    private bool hasExploded = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasExploded || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        hasExploded = true;

       
        if (anim != null)
            anim.SetTrigger("Explode");

        yield return new WaitForSeconds(delayBeforeExplosion);

       
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                LifeController hp = hit.GetComponent<LifeController>();
                if (hp != null)
                    hp.AddHp(-explosionDamage);
            }
        }

        Destroy(gameObject);
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
