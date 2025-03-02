using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene("Scene2", LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync("SampleScene");
        Debug.Log("You have entered the scene changer");
    }
}
