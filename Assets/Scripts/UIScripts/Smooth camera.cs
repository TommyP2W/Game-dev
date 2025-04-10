using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoothcamera : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 offset;
    [SerializeField] public static Transform Target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);


    }
}
