using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy enemy;
    public AudioSource AudioSource;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void DeadSound()

    {
        AudioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
