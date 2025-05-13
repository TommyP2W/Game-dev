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
        InfoText = GameObject.Find("BuildingText");
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InfoText.GetComponent<TextMeshProUGUI>().text = "Feeling lucky?";
        }

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                InfoText.SetActive(false);
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    StatManager.experience += 5;
                    textController.showText(gameObject, gameObject, "FortuneWell", 5);


                }
                else
                {
                    other.gameObject.GetComponent<PlayerClass>().currentStamina -= 4;
                    textController.showText(gameObject, gameObject, "FortuneWell", 0);

                }
                gameObject.SetActive(false);
            }
        }
    }

 
}
