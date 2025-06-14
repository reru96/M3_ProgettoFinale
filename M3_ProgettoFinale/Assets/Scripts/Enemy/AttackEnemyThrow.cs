using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyThrow : MonoBehaviour
{
    public float throwRange = 8f;
    public float throwCooldown = 3f;
    public GameObject shovelPrefab;
    public Transform throwPoint;
    public float projectileSpeed = 10f;

    private Transform player;
    private float lastThrowTime;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= throwRange && Time.time > lastThrowTime + throwCooldown)
        {
            ThrowShovel();
            lastThrowTime = Time.time;
        }
    }

    // Update is called once per frame
    void ThrowShovel()
    {
        

        if (shovelPrefab != null && throwPoint != null)
        {
            GameObject shovel = Instantiate(shovelPrefab, throwPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - throwPoint.position).normalized;

           
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            shovel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Rigidbody2D rb = shovel.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = direction * projectileSpeed;
        }
    }

}
