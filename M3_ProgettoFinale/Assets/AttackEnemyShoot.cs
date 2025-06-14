using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyShoot : MonoBehaviour
{
    private Rigidbody2D rb;
    public float attackRange = 3f;
    public int damage = 1;
    public float attackCooldown = 1f;
    public EnemyBullet bullet;
    public Transform firePoint;
    

    public Transform player;
    public float lastAttackTime;
    public Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
       
    }

    
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
       
        Vector3 attackPos = (player.position - transform.position).normalized;
        anim.SetFloat("x", attackPos.x);
        anim.SetFloat("y", attackPos.y);
        anim.SetTrigger("Attack");
        EnemyBullet b = Instantiate(bullet, firePoint.position, Quaternion.identity).GetComponent<EnemyBullet>(); 
        b.dir = attackPos;
        
    }
}
