using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {

            other.gameObject.GetComponent<PlayerClass>().currentHealth += 10;
            textController.showText(other.gameObject, other.gameObject, "healItemPickup", 10);

            gameObject.SetActive(false);
        }
    }
}
