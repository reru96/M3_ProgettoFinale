using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
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
        EquipWeapon(weaponParent);
        PlayPickupSound();
        Destroy(gameObject, destroyDelay);
    }

    private Transform FindWeaponHolder(Transform player)
    {
        Transform holder = player.Find(weaponHolderName);
        return holder != null ? holder : player;
    }



    private void EquipWeapon(Transform weaponParent)
    {
        if (weaponPrefab == null) return;

        GameObject newWeapon = Instantiate(weaponPrefab, weaponParent);
        newWeapon.transform.localPosition = Vector2.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = Vector2.one * 0.2f;
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }


}
