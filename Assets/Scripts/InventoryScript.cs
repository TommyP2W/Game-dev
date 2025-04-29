using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class InventoryItems
{
    public InventoryItem data { get; private set;}
    public int stackSize { get; set; }
    public InventoryItems(InventoryItem data)
    {
        data = data;
        AddToStack();

    }

    public void AddToStack()
    {
        stackSize++;

    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
}