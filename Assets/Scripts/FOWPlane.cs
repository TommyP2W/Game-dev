using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FOWPlane : MonoBehaviour
{
    public bool explored = false;
    public List<GameObject> hiddenEnemies;
    public List<GameObject> gateBlockers;
    public List<GameObject> hiddenBuildings;
    // Start is called before the first frame update

    public void Start()
    {
        hiddenEnemies = new List<GameObject>();
        gateBlockers = new List<GameObject>();
        hiddenBuildings = new List<GameObject>();
    }
    private void unhide() {
        Debug.Log(hiddenEnemies.Count);
        if (hiddenEnemies != null && hiddenEnemies.Count > 0)
        {
            for (int i = 0; i < hiddenEnemies.Count; i++)
            {
                hiddenEnemies[i].SetActive(true);
            }
               gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        Debug.Log(hiddenBuildings);
        if (hiddenBuildings != null && hiddenBuildings.Count > 0)
        {
            for (int i = 0; i < hiddenBuildings.Count; i++)
            {
                hiddenBuildings[i].SetActive(true);
            }
        }
        if (gateBlockers != null && gateBlockers.Count > 0)
        {
            for (int i = 0; i < gateBlockers.Count; i++)
            {
                gateBlockers[i].SetActive(true);
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            explored = true;
           
            unhide();
            
        }
        if (!explored)
        {
            if (other.gameObject.tag == "Enemy")
            {

                hiddenEnemies.Add(other.gameObject);

                other.gameObject.SetActive(false);

                Debug.Log("JIA");
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
            {
                hiddenBuildings.Add(other.gameObject);
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Gate"))
            {
                gateBlockers.Add(other.gameObject);
                other.gameObject.SetActive(false);

            }
        }
    }
        
    

    public void unblock()
    {
        for (int i = 0;i < gateBlockers.Count;i++) {
        
                gateBlockers[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
    public void Update()
    {
        if (explored)
        {
            for (int i = 0; i < hiddenEnemies.Count; i++)
            {
                if (!hiddenEnemies[i].activeSelf)
                {
                    hiddenEnemies.Remove(hiddenEnemies[i]);
                }
            }
            if (hiddenEnemies.Count == 0)
            {
                Debug.Log("no enemies left");
                unblock();
            }
        }
    }
}
