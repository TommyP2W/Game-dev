using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private GameObject player;
    private NavMeshAgent agent;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim.SetBool("isWalking", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
        agent.speed = 5f;
    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        agent.speed = 3.5f;
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            agent.speed = 2f;
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", true);
            agent.speed = 3.7f;

        }
    }
}
