using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private Vector2 direction;

    public Vector2 Direction => direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        direction = new Vector2(h, v).normalized;
        Vector2 newPosition = rb.position + Direction * (speed * Time.deltaTime);
        rb.MovePosition( newPosition);
    }
}


