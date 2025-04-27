using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{


    public Camera camera;

    public bool isPossessed;


    public void Possess(GameObject enemy)
    {
        camera = Camera.main;
        camera.transform.position = enemy.transform.position;
    }

    public void Start()
    {
        
    }
}