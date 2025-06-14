using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Arma da equipaggiare")]
    [SerializeField] private GameObject weaponPrefab;

    [Header("Dove attaccare l'arma nel player")]
    [SerializeField] private string weaponHolderName = "WeaponSlot"; 

    [Header("Audio di raccolta")]
    [SerializeField] private AudioClip pickupSound;

    [Header("Delay prima della distruzione")]
    [SerializeField] private float destroyDelay = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Transform weaponParent = FindWeaponHolder(other.transform);
        if (weaponParent == null) return;
        HandleWeaponPickup(weaponParent);
        //PlayPickupSound();
        AudioController.Play(pickupSound, transform.position, 1);
        Destroy(gameObject, destroyDelay);
    }

    private Transform FindWeaponHolder(Transform player)
    {
        Transform holder = player.Find(weaponHolderName);
        return holder != null ? holder : player;
    }


    void HandleWeaponPickup(Transform weaponSlot)
    {
        
        GameObject newWeaponObj = Instantiate(weaponPrefab);
        BaseWeapon newWeapon = newWeaponObj.GetComponent<BaseWeapon>();
        System.Type newType = newWeapon.GetType();

       
        foreach (BaseWeapon existing in weaponSlot.GetComponentsInChildren<BaseWeapon>())
        {
            if (existing.GetType() == newType)
            {
               
                existing.LevelUp();
                Destroy(newWeaponObj);
                return;
            }
        }

        newWeapon.transform.SetParent(weaponSlot);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = Vector3.one * 0.2f;
    }
    //private void PlayPickupSound()
    //{
    //    if (pickupSound != null)
    //    {
    //        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    //    }
    //}
}