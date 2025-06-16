using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AttackEnemyShoot : MonoBehaviour
{
    private Rigidbody2D rb;
    public float attackRange = 3f;
    public int damage = 1;
    public float attackCooldown = 1f;
    public EnemyBullet bullet;
    public Transform firePoint;
    public AudioClip hitSound;
    private LifeController life;
    

    public Transform player;
    public float lastAttackTime;
    public Animator anim;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        life = GetComponent<LifeController>();  
       
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
        bool attack = true;
        Vector3 attackPos = (player.position - transform.position).normalized;
        anim.SetFloat("x", attackPos.x);
        anim.SetFloat("y", attackPos.y);
        anim.SetBool("isAttacking", attack);
        EnemyBullet b = Instantiate(bullet, firePoint.position, Quaternion.identity).GetComponent<EnemyBullet>(); 
        b.dir = attackPos;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            LifeController lifeController = collision.collider.GetComponent<LifeController>();

            if (lifeController != null)
            {

                lifeController.AddHp(-1);
            }
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            if (bullet != null)
            {
                AudioController.Play(hitSound,transform.position, 1);
                life.AddHp(-bullet.Damage);
            }
        }
    }
}
