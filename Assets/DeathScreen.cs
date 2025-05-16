//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class DeathScreen : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public void tryAgain()
//    {
//        if (EndTurn.CoroutinesActive == 0)
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }
//    }
//    public void menu()
//    {
//        if (EndTurn.CoroutinesActive == 0)
//        {
//            SceneManager.LoadScene("menu");
//        }
//    }

//    public void Update()
//    {
//        if (EndTurn.CoroutinesActive == 0)
//        {
//            Time.timeScale = 0;
//        } else
//        {
//            Time.timeScale = 1;
//        }
//    }
//}
