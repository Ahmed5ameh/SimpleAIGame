using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointIndex = 0;
    private float dist;
    public float speed;
    public float detectionRadius = 5f;
    public float detectionAngel = 75f;
    Transform playerTransform;
    Rigidbody rb;
    EnemyNoAIController chaseScript;
    bool Crash = false;
    // Start is called before the first frame update
    void Start()
    {
        //chaseScript = GameObject.Find("EnemyNoAI").GetComponent<EnemyNoAIController>(); //WORKING AS WELL
        chaseScript = gameObject.GetComponent<EnemyNoAIController>();     //ERROR
        //chaseScript = GetComponent<EnemyNoAIController>();
        waypointIndex = Random.Range(0, waypoints.Length);
        transform.LookAt(waypoints[waypointIndex].position);
        rb = GetComponent<Rigidbody>();
        playerTransform = PlayerManager.instance.player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        LookForPlayer();
    }

    void Patrol()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            //IncreaseIndex();
            IncreaseIndexRandomly();
        }
        /*      //PREVIOUS IMPLEMENTATION
        Vector3 dir = transform.position - waypoints[waypointIndex].position;    
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        */
        Vector3 x = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
        rb.MovePosition(x);
    }

    /*
    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }
    */

    void IncreaseIndexRandomly()
    {
        waypointIndex = Random.Range(0, waypoints.Length);
        transform.LookAt(waypoints[waypointIndex].position);
    }

    void LookForPlayer()
    {
        Vector3 enemyPosition = transform.position;
        //Vector3 toPlayer = PlayerManager.instance.player.transform.position - enemyPosition;  //*BUGGED* GETTING _GameManager POSITION NOT THE PLAYER
        Vector3 toPlayer = playerTransform.position - enemyPosition; 
        toPlayer.y = 0;
        /*
        Debug.Log("playerPosition is" + PlayerManager.instance.player.GetComponent<Transform>().position);
        Debug.Log("toPlayer is" + toPlayer);
        Debug.Log("toPlayer.magnitude is" + toPlayer.magnitude);
        float ANGEL = Vector3.Angle(toPlayer.normalized, transform.forward);
        Debug.Log("ANGLE is " + ANGEL);
        */
        if (toPlayer.magnitude <= detectionRadius)
        {
            /*
             For normalized vectors Dot() returns 1 if they point in exactly the same direction, 
                                                 -1 if they point in completely opposite directions and
                                                  0 if the vectors are perpendicular.
            */ 
            if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(detectionAngel * 0.5f * Mathf.Deg2Rad)) 
            {
                Debug.Log("Player Has been detected.");
                chaseScript.Chase(transform, playerTransform, rb, 7.5f);
            }
            else
            {
                Patrol();
            }
            

            /*
            if(Mathf.Acos(Vector3.Dot(toPlayer.normalized, transform.forward)) * Mathf.Rad2Deg * 0.5f >= detectionAngel)
            {
                Debug.Log("Player Has been detected 2222222.");
            }
            */
        }
        else
        {
            transform.LookAt(waypoints[waypointIndex].position);
        }
    }

    IEnumerator Delay()
    {
        while (true)
        { // This creates a never-ending loop
            yield return new WaitForSeconds(2);
            Crash = false;
            // If you want to stop the loop, use: break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.yellow;
        //Vector3 toPlayer = PlayerManager.instance.player.GetComponent<Transform>().position - transform.position;
        /*
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = PlayerManager.instance.player.GetComponent<Transform>().position - enemyPosition;
        Gizmos.DrawLine(Vector3.zero, transform.forward);
        UnityEditor.Handles.DrawLine(Vector3.zero, toPlayer);
        */
        //UnityEditor.Handles.DrawLine(Vector3.zero, toPlayer);
        UnityEditor.Handles.DrawLine(Vector3.zero, transform.forward);
        UnityEditor.Handles.color = Color.magenta;
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            Quaternion.Euler(0, -detectionAngel * 0.5f, 0) * transform.forward,
            detectionAngel,
            detectionRadius);
    }
}
