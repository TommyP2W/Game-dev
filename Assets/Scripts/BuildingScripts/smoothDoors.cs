using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class smoothDoors : MonoBehaviour
{

    private Vector3 offset;
    private Transform target;
    private float smoothTime = 4f;
    private Vector3 _currentVelocity = Vector3.zero;
    private GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {
        target = transform;
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("hello2");
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }


    private void LateUpdate()
    {

        if (Vector3.Distance(target.position, player.transform.position) < 4)
        {

            Vector3 targetPosition = new Vector3(transform.position.x, 2.2f, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        }


    }
  

}
