using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject weaponDropPrefab;
    public Vector3 dropOffset = Vector3.zero;
    public int chance = 10;
    private bool isQuitting = false;
    private Rigidbody2D rb;
    private LifeController life;

    private void Start()
    {
        life = GetComponent<LifeController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (isQuitting || weaponDropPrefab == null) return;

        OnDie();

    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void OnDie()
    {

        if (life.CurrentHP == 0)
        {
            if (isQuitting || weaponDropPrefab == null) return;
            int randomChance = Random.Range(1, 101);
            if (randomChance <= chance)
            {
                Instantiate(weaponDropPrefab, transform.position + dropOffset, Quaternion.identity);
            }
        }
    }
}


