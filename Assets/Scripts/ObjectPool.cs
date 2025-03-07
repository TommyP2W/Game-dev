using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    // Start is called before the first frame update
    void Start()
{
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false); ;
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
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
