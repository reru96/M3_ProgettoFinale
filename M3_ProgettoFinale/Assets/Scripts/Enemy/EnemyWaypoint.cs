using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{

    public float movespeed = 5f;
    public float waitTime = 2f;
    public bool loopWaypoints = true;
    public float detectionRange = 3f;
    public int MaxWaypoint = 100;
    public AudioClip hitSound;
    public int damage = 1;

    private Transform waypointParent;
    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool isWaiting;
    private Transform player;
    private LifeController life;

    void Start()
    {

        life = GetComponent<LifeController>();
        GameObject waypointObject = GameObject.FindWithTag("Waypoint");
        if (waypointObject != null)
        {
            waypointParent = waypointObject.transform;
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isWaiting)
        {
            return;
        }

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            MoveToWaypoint();
        }

    }

    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Length)
        {
            if (loopWaypoints)
            {
                currentWaypointIndex = 0;
            }
            else
            {
                currentWaypointIndex = waypoints.Length - 1;
            }
        }

        isWaiting = false;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LifeController player = GetComponent<LifeController>();
        if (collision.collider.CompareTag("Player"))
        {

            
            player.AddHp(-damage);
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            if (bullet != null)
            {
                //audioSource.pitch = UnityEngine.Random.Range(0.1f, 1.1f);
                //audioSource.Play();
                
                AudioController.Play(hitSound, transform.position, 1);
                life.AddHp(-bullet.Damage);
            }
        }
    }

    void ChasePlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);

    }

}



