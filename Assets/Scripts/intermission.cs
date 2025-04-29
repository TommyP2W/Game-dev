using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class intermission : MonoBehaviour
{
    public float cameraX;
    public float cameraY;

    public float horizontal;
    public float vertical;
    private Camera cam;
    public float sensitivity = 0.001f;
    public GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cameraX = Input.GetAxis("Mouse Y");
        cameraY = Input.GetAxis("Mouse X");
        cam.transform.Rotate(-Input.GetAxis("Mouse Y"), cameraY * sensitivity * Time.deltaTime, 0f);
        Vector3 rotate = cam.transform.rotation.eulerAngles;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Character.transform.rotation = Quaternion.Euler(0f, rotate.y, 0f);
        Character.transform.Translate(Vector3.forward * Time.deltaTime * vertical);
        Character.transform.Translate(Vector3.right * Time.deltaTime * horizontal);
    }
}
