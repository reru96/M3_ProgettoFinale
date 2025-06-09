using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Configurazione Arma")]
    [SerializeField] private GameObject weaponPrefab;  
    [SerializeField] private Transform weaponParent;   
    [SerializeField] private float destroyDelay = 0.5f; 
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EquipWeapon(other.transform);
            PlayPickupSound();
            Destroy(gameObject, destroyDelay);
        }
    }

    private void EquipWeapon(Transform player)
    {
        if (weaponPrefab == null) return;

      
        if (weaponParent == null)
        {
            weaponParent = player.Find("Player") ?? player; 
        }

      
        foreach (Transform child in weaponParent)
        {
            if (child.CompareTag("Weapon"))
            {
                Destroy(child.gameObject);
            }
        }

        
        GameObject newWeapon = Instantiate(weaponPrefab, weaponParent);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void PlayPickupSound()
    {

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }
}
