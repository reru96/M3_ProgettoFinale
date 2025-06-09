using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public int lifetime = 2;
    [SerializeField] private int damage = 1;
    public AudioSource audioSource;

    public int Damage => damage;


    public Vector2 dir
    {
        get;
        set;
    }

    public float Speed => speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Destroy(gameObject, lifetime);

    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            Destroy(gameObject);
        }

    }

}
