using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WellOfFortune : MonoBehaviour
{
    private GameObject InfoText;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InfoText = GameObject.Find("LeverText");
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InfoText.GetComponent<TextMeshPro>().text = "Trade stamina for a chance at fortune!";
        }

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //if (Player)
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
