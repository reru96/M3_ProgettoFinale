using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject weaponDropPrefab;
    public Vector3 dropOffset = Vector3.zero;
    private bool isQuitting = false;

    void OnApplicationQuit()
    {
        isQuitting = true; 
    }

    void OnDestroy()
    {
        if (isQuitting || weaponDropPrefab == null) return;

        Instantiate(weaponDropPrefab, transform.position + dropOffset, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        { 
            Destroy(gameObject);    
        }
    }
}


