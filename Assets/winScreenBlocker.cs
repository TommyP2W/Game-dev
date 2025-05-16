using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScreenBlocker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("VictoryScreen").SetActive(false);    
    }

    // Update is called once per frame
    
}
