

using System.Collections;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
  
    public float moveSpeed = 3f;
    public float waitTime = 1.5f;
    public bool loopWaypoints = true;

 
    public float detectionRange = 4f;
    public int damageToPlayer = 1;

    
    public int maxWaypoints = 100;

    public AudioClip hitSound;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    public Transform waypointParent;
    private Transform player;
    private LifeController enemyLife;


    void Start()
    {
       
        enemyLife = GetComponent<LifeController>();

        
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError($"{name}: Nessun oggetto con tag 'Player' trovato.");
            enabled = false;
            return;
        }

      
        if (waypointParent == null)
        {
            Debug.LogError($"{name}: waypointParent non assegnato. Assegnalo nel campo Inspector.");
            enabled = false;
            return;
        }

        
        int count = Mathf.Min(waypointParent.childCount, maxWaypoints);
        waypoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

        if (waypoints.Length == 0)
        {
            Debug.LogError($"{name}: Nessun waypoint trovato nel genitore.");
            enabled = false;
        }
    }


    void Update()
    {
        if (waypoints == null || player == null || isWaiting) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer <= detectionRange)
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
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

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
            currentWaypointIndex = loopWaypoints ? 0 : waypoints.Length - 1;
        }

        isWaiting = false;
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            LifeController playerLife = collision.collider.GetComponent<LifeController>();
            if (playerLife != null)
            {
                playerLife.AddHp(-damageToPlayer);
            }
        }

        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (hitSound != null)
                    AudioSource.PlayClipAtPoint(hitSound, transform.position);
                enemyLife.AddHp(-bullet.Damage);
            }
        }
    }
}


