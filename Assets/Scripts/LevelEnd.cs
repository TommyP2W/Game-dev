using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject endText;
    GameObject[] enemies;
    public bool noEnemies = false;

    public void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        endText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endText.SetActive(true);
            endText.GetComponent<TextMeshProUGUI>().text = "Press E to continue your journey";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            endText.SetActive(false);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EndTurn.CoroutinesActive == 0)
                {
                    if (SceneManager.GetActiveScene().name == "Level2")
                    {
                        StatManager.finishedLevel2 = true;
                    }
                    else if (SceneManager.GetActiveScene().name == "Level3")
                    {
                        StatManager.finishedLevel3 = true;
                    }
                    else if (SceneManager.GetActiveScene().name == "Level4")
                    {
                        StatManager.finishedLevel4 = true;
                    }
                    else if (SceneManager.GetActiveScene().name == "Level5")
                    {
                        StatManager.finishedLevel5 = true;
                    }
                    SceneManager.LoadScene("Intermission");
                }
            }

        }
    }

    public void checkEnemies()
    {

        foreach (GameObject enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                return;
            }
        }
        noEnemies = true;
    }

    public void intermission()
    {
        SceneManager.LoadScene("Intermission");
    }


    public void Update()
    {
        
    }
}
