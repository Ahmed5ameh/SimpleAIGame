using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    bool Crash = false;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        //Use collision
        if (gameObject.tag == "AttackerEnemyAI")
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
            }
            if (distance <= agent.stoppingDistance && Crash == false)
            {
                Crash = true;
                //Update Slider UI
                PlayerManager.instance.HPBar.value -= 10;
            }
        }
        

        if (gameObject.tag == "FleeEnemy")
        {
            //T0D0
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
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

}
