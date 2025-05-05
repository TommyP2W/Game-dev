using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShaman : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 10;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public bool healAction { get; set; }
    public int armour_class { get; set; } = 4;
    public GameObject requestedEnemy { get; set; } = null;

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        } else if (healAction) {
            heal();
        }
    }


    public void attack()
    {
        attackAction = false;
    }
    public void heal()
    {
        GameObject lowestHealthObject = null;
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(transform.position)]);
        for (int i = 0; i < 4; i++) {
            List<GridCell> nextDoor = GridTest.getNeighbours(GridManager.gridLayout[neighbours[i].position]);
            for (int j = 0; j < nextDoor.Count; j++)
            {
                neighbours.Add(nextDoor[j]);
            }
        }

        foreach (GridCell cell in neighbours)
        {
            Debug.Log(cell);
            if (cell.occupiedBy != null)
            {
                Debug.Log("hello");
                Debug.Log(cell.occupiedBy.name);
                Debug.Log(cell.occupiedBy.tag);
                if (!cell.occupiedBy.tag.Equals("Player") && cell.occupiedBy != this.gameObject)
                {
                    Debug.Log("After conditional");
                    if (lowestHealthObject == null)
                    {
                        lowestHealthObject = cell.occupiedBy;
                    }
                    else
                    {
                        
                        if (cell.occupiedBy.GetComponent<Characters>().currentHealth < lowestHealthObject.GetComponent<Characters>().currentHealth)
                        {
                            lowestHealthObject = cell.occupiedBy;
                        }
                    }
                }
            }
        }
        if (lowestHealthObject != null)
        {
            Debug.Log(lowestHealthObject.GetComponent<Characters>().currentHealth);
            lowestHealthObject.GetComponent<Characters>().currentHealth += UnityEngine.Random.Range(1, 4);
            if (lowestHealthObject.GetComponent<Characters>().currentHealth > lowestHealthObject.GetComponent<Characters>().maxHealth)
            {
                lowestHealthObject.GetComponent<Characters>().currentHealth = lowestHealthObject.GetComponent<Characters>().maxHealth;
            }
            Debug.Log("Healed");
        }
        textController.showText(gameObject, lowestHealthObject, "Heal");
        healAction = false;
    }
    public void death()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (attackAction == false)
        {
            healAction = true;
        }
    }
}
