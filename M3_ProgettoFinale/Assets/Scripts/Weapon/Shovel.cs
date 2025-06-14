using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public int damage = 2;
    public float rotationSpeed = 360f;
    public float lifetime = 5f;

   // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    // Update is called once per frame

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LifeController hp = collision.GetComponent<LifeController>();
            if (hp != null) hp.AddHp(-damage);
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
