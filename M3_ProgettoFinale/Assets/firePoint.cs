using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class firePoint : MonoBehaviour
{
    public Transform target;

    public Animator animator;

   
    private readonly Vector3 leftPos = new Vector3(-10f, 0f, 0f);
    private readonly Vector3 rightPos = new Vector3(10f, 0f, 0f);
    private readonly Vector3 upPos = new Vector3(0f, 5f, 0f);
    private readonly Vector3 downPos = new Vector3(0f, -5f, 0f);

    void Start()
    {
        animator = GetComponentInParent<Animator>();
       
    
    }

    void Update()
    {
        if (animator == null) return;

        float h = animator.GetFloat("x");
        float v = animator.GetFloat("y");

        Vector3 targetPos = Vector3.zero;

        float deadZone = 0.1f;
        if (h < -deadZone) targetPos = leftPos;
        else if (h > deadZone) targetPos = rightPos;
        else if (v > deadZone) targetPos = upPos;
        else if (v < -deadZone) targetPos = downPos;

        transform.localPosition = targetPos;
    }

}
