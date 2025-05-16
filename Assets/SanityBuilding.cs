using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SanityBuilding : MonoBehaviour
{
    private GameObject InfoText;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InfoText = GameObject.Find("BuildingText");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InfoText.GetComponent<TextMeshProUGUI>().text = "Pray (Press E)";
        }

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InfoText.SetActive(false);
                other.GetComponent<PlayerClass>().current_sanity += 20;
                textController.showText(gameObject, gameObject, "SanityRegenBuilding", 20);


                gameObject.SetActive(false);
            }
        }
    }
}
