using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;
    public static textController inst;

    private void Awake()
    {
        inst = this;
        Debug.Log(inst);
        Debug.Log(prefab);
    }

    public static void showText(GameObject origin, GameObject entity, string option, int damage = 0)
    {
        GameObject text = Instantiate(inst.prefab, entity.transform.position, Quaternion.identity);
        text.GetComponent<UIanager>().show(origin, entity, option, damage);
    }

    // Update is called once per frame
   
}
