using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Animation animClip;


    public int lifetime = 2;
    public AudioClip hitSound;
    public AudioClip ThrowSound;

    public int Damage => damage;
    public float Speed
    {
        get => speed;
        set => speed = value;   
    }
    public Vector2 dir
    {
        get;
        set;
    }

    private Rigidbody2D _rb;

    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        AudioController.Play(ThrowSound, transform.position, 0.10f);
        Destroy(gameObject, lifetime); 
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        _rb.velocity = dir * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {

            LifeController life = collision.collider.GetComponent<LifeController>();
            AudioController.Play(hitSound, transform.position, 0.10f);
            life.AddHp(-damage);
            Destroy(gameObject);
        }

        if (!collision.collider.CompareTag("Enemy"))
        {
            AudioController.Play(hitSound, transform.position, 0.10f);
            Destroy(gameObject);
        }
    }
}