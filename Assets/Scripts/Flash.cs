using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // Start is called before the first frame update
    public static Flash flas;
    
    void Start()
    {
        flas = this;
    }


   

    public static IEnumerator flash(GameObject RequestedEnemy)
    {
        RequestedEnemy.transform.Find("body").GetComponentInChildren<Renderer>().material.EnableKeyword("_EMISSION");
        Color original = RequestedEnemy.transform.Find("body").GetComponentInChildren<Renderer>().material.GetColor("_EmissionColor");
        RequestedEnemy.transform.Find("body").GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", Color.red * 500f);
        
        yield return new WaitForSeconds(2f);

        RequestedEnemy.transform.Find("body").GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", original);

        yield return null;
    }

         
    // Update is called once per frame
    void Update()
    {
        
    }
}
