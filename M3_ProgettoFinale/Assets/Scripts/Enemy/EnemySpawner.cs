using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Punti di spawn disponibili")]
    public Transform[] spawnPoints;

    [Header("Prefab del nemico da spawnare")]
    public GameObject[] enemyPrefab;

    [Header("Intervallo tra gli spawn (secondi)")]
    public float spawnInterval = 2f;

    [Header("Audio di spawn")]
    public AudioClip spawnSound;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            AudioController.Play(spawnSound, transform.position, 1);
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        
        spawnPoints = System.Array.FindAll(spawnPoints, sp => sp != null);
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
        GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
    }
}


