using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScythe : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    public Transform firePoint;              
    public float orbitRadius = 2.0f;
    public float orbitSpeed = 180.0f;
    public float duration = 3.0f;

    public int dmg => damage;

    private float angle;
    private float timer;

    void Start()
    {
        if (firePoint == null)
        {
            Debug.LogError("Scythe: firePoint non assegnato!");
            Destroy(gameObject);
            return;
        }

        angle = Random.Range(0f, 360f);
        timer = duration;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            angle += orbitSpeed * Time.deltaTime;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbitRadius;
            transform.position = firePoint.position + offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<LifeController>()?.AddHp(-dmg);
        }
    }

}
