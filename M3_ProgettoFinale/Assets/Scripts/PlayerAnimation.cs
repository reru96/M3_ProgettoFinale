using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    private PlayerController player;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Animate();
    }

    private void Animate()
    {
        bool isTurning = player.Direction.magnitude != 0;
        bool isMoving = player.Direction.magnitude > 0.1f;
        anim.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            anim.SetFloat("x", player.Direction.x);
            anim.SetFloat("y", player.Direction.y);
        }


        if (isTurning)
        {
            anim.SetFloat("x", player.Direction.x);
            anim.SetFloat("y", player.Direction.y);
        }

      
    }
}
