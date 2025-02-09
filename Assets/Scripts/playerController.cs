using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class playerController : MonoBehaviour
{
    public NavMeshAgent agent;
    //public GameObject player;
    // Start is called before the first frame update
    public Camera cam;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("HspZomNpc");
            foreach (GameObject zombie in zombies)
            {
                Destroy(zombie);
            };
        }
        //agent.SetDestination(player.transform.position);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit)){
        //        // Move our agent
        //        agent.SetDestination(hit.point);
        //    };
        //}
    }
}
