using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermissionDoors : MonoBehaviour
{

    public TextMeshProUGUI text;
  

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
            if (other.tag == "DoorLevel2" && !StatManager.finishedLevel2)
            {
                SceneManager.LoadScene("Level2");
            }
            else if (other.tag == "DoorLevel3" && !StatManager.finishedLevel3)
            {
                SceneManager.LoadScene("Level3");
            }
            else if (other.tag == "DoorLevel4" && !StatManager.finishedLevel4)
            {
                SceneManager.LoadScene("Level4");
            }
            else if (other.tag == "DoorLevel5" && !StatManager.finishedLevel5)
            {
                SceneManager.LoadScene("Level5");
            }
        }
    }

    // Update is called once per frame
   
}
