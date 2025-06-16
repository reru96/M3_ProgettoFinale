using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialExit : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemySpecial");

        if (enemies.Length == 0)
        {
            Debug.Log("Livello Completato");
            gameObject.SetActive(false);

        }

    }

}
