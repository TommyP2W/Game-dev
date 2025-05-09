using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject camera;

    private void Start()
    {
        camera = GameObject.Find("CameraPivot");
    }
    void Update()
    {
        transform.LookAt(camera.transform);
        GetComponent<Slider>().value = (float)((gameObject.GetComponentInParent<Characters>().currentHealth / gameObject.GetComponentInParent<Characters>().maxHealth) * 100); 
    }
}
