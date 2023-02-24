using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeController : MonoBehaviour
{
    Transform targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - targetPlayer.position;
        if(direction.sqrMagnitude < 1000f)
        {
            transform.Translate(direction.normalized * Time.deltaTime * 7.5f, Space.World);
            transform.forward = direction.normalized;
        }
         
        while(Vector3.Distance(targetPlayer.position, transform.position) <= 1)
        {
            Debug.Log("ENEMY DEAD!");
            gameObject.SetActive(false);
            break; 
        }
    }
}
