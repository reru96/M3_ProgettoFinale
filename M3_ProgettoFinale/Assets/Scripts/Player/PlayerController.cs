using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;
    private float x;
    private float y;
    private Vector2 dir;
    public Vector2 Direction => dir; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {     
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        dir = new Vector2(x, y).normalized;
        Vector2 newPosition = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    } 
}