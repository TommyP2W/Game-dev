using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject playerobj;
    // Start is called before the first frame update
    void Start()
    {
        playerobj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //agent = GetComponent<NavMeshAgent>();

        //agent.SetDestination(playerobj.transform.position);
    }
}
