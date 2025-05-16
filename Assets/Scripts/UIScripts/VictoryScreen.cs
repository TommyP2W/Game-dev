using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public void Awake()
    {
        resetStats();
    }
    public void resetStats()
    {
        StatManager.resetStats();
    }
    
    public void menu()
    {
        SceneManager.LoadScene("menu");
    }
    public void gameExit()
    {
        Application.Quit();
    }
}
