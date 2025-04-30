
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;


public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> gravePooledObjects;
    public List<GameObject> ghosts;
    public GameObject ghostsToPool;
    public GameObject objectToPool;
    public GameObject graveToPool;
    public int ghostsAmountToPool;
    public int amountToPool;
    public int graveAmountToPool;
    // Start is called before the first frame update
    void Start()
{
        pooledObjects = new List<GameObject>();
        gravePooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false); ;
            pooledObjects.Add(tmp);
        }

        for (int i = 0; i < ghostsAmountToPool; i++)
        {
            tmp = Instantiate(ghostsToPool);
            tmp.SetActive(false);
            ghosts.Add(tmp);
        }

        for (int i = 0; i < graveAmountToPool; i++)
        {
            tmp = Instantiate(graveToPool);
            tmp.SetActive(false);
            gravePooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject(string type)
    {
        if (type.Equals("soldier"))
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        } else if (type.Equals("grave")){
            for (int i = 0; i < graveAmountToPool; i++)
            {
                if (!gravePooledObjects[i].activeInHierarchy)
                {
                    return gravePooledObjects[i];
                }
            }
            return null;
        } else if (type.Equals("ghosts"))
        {
            for (int i = 0; i < ghostsAmountToPool; i++)
            {
                if (!ghosts[i].activeInHierarchy)
                {
                    return ghosts[i];
                }
            }
        }
        return null;
    }
        // Update is called once per frame
        void Awake()
    {
        SharedInstance = this;  
    }
}
