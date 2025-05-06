using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    public GameObject prefab;
    // Start is called before the first frame update
 

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject fire_prefab = Instantiate(prefab, transform.position, Quaternion.identity);
            StartCoroutine(fire(fire_prefab));
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth -= 10;
        }
    }

    public IEnumerator fire(GameObject fire)
    {
        yield return new WaitForSeconds(1);
        Destroy(fire);
    }
    // Update is called once per frame
    
}
