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
    public static int health = 10;
    public Camera cam;
    // Update is called once per frame

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {

       
    }
}
