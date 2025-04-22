using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FOWPlane : MonoBehaviour
{
    private bool explored = false;
    public List<GameObject> hiddenEnemies;
    // Start is called before the first frame update

    public void Start()
    {
        hiddenEnemies = new List<GameObject>();
    }
    private void unhide() {
        Debug.Log(hiddenEnemies.Count);
        if (hiddenEnemies != null)
        {
            for (int i = 0; i < hiddenEnemies.Count; i++)
            {
                hiddenEnemies[i].SetActive(true);
            }
        gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hhaudhas");
        if (other.gameObject.tag == "Player")
        {
            explored = true;
            if (hiddenEnemies.Count > 0)
            {
                unhide();
            }
        }
        if (!explored)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.layer == LayerMask.NameToLayer("Buildings")){
                Debug.Log("Skibidi toilet");
                hiddenEnemies.Add(other.gameObject);
                Debug.Log("dasidja"); 
                other.gameObject.SetActive(false);
                Debug.Log("JIA");
            }
        }
    }

}
