using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoAIController : MonoBehaviour
{
    public Transform targetPlayer;
    public float speed = 4f;
    Rigidbody rb;
    bool Crash = false;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        Chase(transform, targetPlayer, rb, 4f);
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

    public void Chase(Transform self, Transform targetToChaseAKA_Player, Rigidbody Rrb, float sSpeed)
    {
        Vector3 pos = Vector3.MoveTowards(self.position, targetToChaseAKA_Player.position, sSpeed * Time.fixedDeltaTime);
        Rrb.MovePosition(pos);
        self.LookAt(targetToChaseAKA_Player.position);
        float distance = Vector3.Distance(targetToChaseAKA_Player.position, self.position);
        if (distance <= 1 && Crash == false)
        {
            Crash = true;
            //Update Slider UI
            PlayerManager.instance.HPBar.value -= 10;
        }
    }

} 
