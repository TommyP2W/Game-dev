using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Smoothcamera : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 offset;
    private float speed = 3.5f;
    [SerializeField] public static Camera cam;
    [SerializeField] public static Transform Target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - Target.position;
    }
    private void Start()
    {
        cam = GetComponent<Camera>(); 
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);


    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
            
        {
            float rotation = transform.eulerAngles.y + Input.GetAxis("Mouse X") * speed;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,rotation,transform.eulerAngles.z);
        }
    }
}
