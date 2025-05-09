using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Warrior : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 30;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 15;
    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public Skeleton_controller controller;
    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }

    public void attack()
    {
        attackAction = false;
    }

    public void death()
    {
        controller.anim.SetBool("Die", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<Skeleton_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            death();
        }

        if (controller.anim.GetCurrentAnimatorStateInfo(0).IsName("dying") && controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupiedBy = null;
            GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupied = false;
            gameObject.SetActive(false);
        }
    }
}
