using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    private GameObject zombieContextPrefab;
    [SerializeField]
    private GameObject spawner;
    
    [SerializeField]
    private float zombieinterval = 3.5f;
    //private float zombieinterval = 3.5;
    
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(spawnEnemy(zombieinterval, zombieContextPrefab));
    //}

    //// Update is called once per frame
    //// void Update()
    //// {
        
    //// }
    //private IEnumerator spawnEnemy(float interval, GameObject enemy){
    //    yield return new WaitForSeconds(interval);
    //    GameObject zombie = ObjectPool.SharedInstance.GetPooledObject();
    //    if (zombie != null)
    //    {
    //        zombie.transform.position = spawner.transform.position;
    //        zombie.transform.rotation = spawner.transform.rotation;
    //        zombie.SetActive(true);
    //    }
    //    StartCoroutine(spawnEnemy(interval,enemy));
    //}
}
