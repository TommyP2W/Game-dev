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
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public RegOrcAnimationController controller;

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
        controller.anim.SetBool("Die", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = gameObject.GetComponent<RegOrcAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (currentHealth <= 0)
        {
            death();
        }
        if (attackAction == false)
        {
            healAction = true;
        }
        if (controller.anim.GetCurrentAnimatorStateInfo(0).IsName("dying") && controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupiedBy = null;
            GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupied = false;
            gameObject.SetActive(false);
        }
    }
}
