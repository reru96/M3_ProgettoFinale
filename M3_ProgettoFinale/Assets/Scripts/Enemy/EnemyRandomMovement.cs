using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float directionChangeInterval = 2f;
    [SerializeField] private Animator anim;

    public AudioClip hitSound;
    public AudioClip deathSound;

    private LifeController life;
    private Rigidbody2D rb;
    private Vector2 randomDirection;
    private float directionTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = GetComponent<LifeController>();


        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }


        GetNewRandomDirection();
        directionTimer = directionChangeInterval;
    }

    void Update()
    {
        if (life != null && !life.IsAlive) return;

        MoveRandomly();
        Animate();
    }

    void MoveRandomly()
    {

        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            GetNewRandomDirection();
            directionTimer = directionChangeInterval;
        }


        if (rb != null)
        {
            rb.velocity = randomDirection * speed;
        }
        else
        {

            transform.Translate(randomDirection * speed * Time.deltaTime);
        }
    }

    void GetNewRandomDirection()
    {

        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        LifeController player = GetComponent<LifeController>();
        if (collision.collider.CompareTag("Player"))
        {

           
            player.AddHp(-1);
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            if (bullet != null && life != null)
            {   
              
                AudioController.Play(hitSound, transform.position, 1);
                life.AddHp(-bullet.Damage);
            }
        }
    }

    private void Animate()
    {
        if (anim == null) return;

        bool dead = life != null && !life.IsAlive;
        anim.SetBool("Die", dead);

        if (dead) return;


        anim.SetFloat("x", randomDirection.x);
        anim.SetFloat("y", randomDirection.y);
        anim.SetBool("isMoving", randomDirection.magnitude > 0.1f);
    }
}
