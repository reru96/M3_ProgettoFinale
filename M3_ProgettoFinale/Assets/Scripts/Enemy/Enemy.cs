using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;

    public AudioClip hitSound;
    public AudioClip deathSound;
    //[SerializeField] private AudioSource audioSource;

    private LifeController life;
    private Rigidbody2D rb;
    private float x;
    private float y;
    private Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = GetComponent<LifeController>();
        anim = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            Animate();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = player.position;
        dir = (targetPos - currentPos).normalized;
        x = dir.x;
        y = dir.y;
        transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
           
            GameObject playerObject = collision.collider.gameObject;

          
            LifeController lifeController = playerObject.GetComponent<LifeController>();

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
                //audioSource.pitch = UnityEngine.Random.Range(0.1f, 1.1f);
                //audioSource.Play();
                AudioController.Play(hitSound, transform.position, 1);
                life.AddHp(-bullet.Damage);
            }
        }
    }

    private void Animate()
    {
        float direction = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime).magnitude;
        bool isMoving = direction > 0.1f;
        bool isTurning = direction != 0;
        bool dead = !life.IsAlive;
     
        if (isTurning)
        {
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
        }

        if (isMoving)
        {
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
        }

        anim.SetBool("isMoving", isMoving);

        anim.SetBool("Die", dead);
    }
}