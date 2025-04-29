using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermissionDoors : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
       if (other.tag == "DoorLevel2")
        {
            text.text = "Press E to begin level 2";
        } else if (other.tag == "DoorLevel3")
        {
            text.text = "Press E to begin level 3";
        }
        else if (other.tag == "DoorLevel4")
        {
            text.text = "Press E to begin level 4";
        }
        else if (other.tag == "DoorLevel5")
        {
            text.text = "Press E to begin level 5";
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.tag == "DoorLevel2")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (other.tag == "DoorLevel3")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (other.tag == "DoorLevel4")
            {

            }
            else if (other.tag == "DoorLevel5")
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
