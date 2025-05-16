using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    //private void Start()
    //{
    //    foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
    //    {
    //        if (obj.scene.name == "DontDestroyOnLoad")
    //        {
    //            Destroy(obj);
    //        }
    //    }
    //}
    public void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
