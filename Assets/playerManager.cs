using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public GameObject startingPos;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        startingPos = GameObject.Find("startpos");
        if (startingPos != null)
        {
            gameObject.transform.position = startingPos.transform.position;
        }
    }
}
