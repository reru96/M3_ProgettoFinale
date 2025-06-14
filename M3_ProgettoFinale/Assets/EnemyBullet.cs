using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Animation animClip;

    public int lifetime = 2;
    public AudioClip hitSound;


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
        animClip = GetComponent<Animation>();
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        animClip.Play();
    }
    private void FixedUpdate()
    {
        _rb.velocity = dir * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            AudioController.Play(hitSound, transform.position, 1);
            Destroy(gameObject);
        }
    }
}
