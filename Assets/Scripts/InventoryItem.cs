using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory item")]
public class InventoryItem :ScriptableObject
{
    // Start is called before the first frame update

    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
