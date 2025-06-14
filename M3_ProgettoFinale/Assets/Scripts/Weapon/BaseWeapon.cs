using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public AudioClip fireSound;

    private float fireTimer;
    private int level = 0;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            firePoint = player.transform.Find("firePoint");
        }

        if (firePoint == null)
        {
            Debug.LogError("firePoint non trovato nel Player!");
        }
    }

    protected virtual void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }
  
    protected abstract void Fire();
    public virtual void LevelUp()
    {
        level++;
        fireRate += 0.2f;
        Debug.Log($"{gameObject.name} livellata! Nuovo danno: {level}");
    }

    protected void PlayFireSound()
    {
        AudioController.Play(fireSound, transform.position, 1);
    }
}