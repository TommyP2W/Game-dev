using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SkeletonBoss : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 45;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 12;
    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public bool statsBoosted = false;
    public int statRetention = 0;
    public Skeleton_controller controller;
    public GameObject prefab;
    public void actionSelector()
    {
        if (statRetention > 0)
        {
            statRetention--;
        } else
        {
            statsBoosted = false;
        }
        if (attackAction)
        {
            attack();
        }
        if (!statsBoosted)
        {
            boostStats(UnityEngine.Random.Range(0,3));
        }
    }

    public void attack()
    {
        if (requestedEnemy == null)
        {
            gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth -= UnityEngine.Random.Range(1, 18);
            Debug.Log("Player Health " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth);
            controller.anim.SetBool("isAttacking", true);

        }
        else
        {
            gameObject.transform.LookAt(requestedEnemy.GetComponent<Transform>());
            requestedEnemy.GetComponent<Characters>().currentHealth -= UnityEngine.Random.Range(1, 18);
            controller.anim.SetBool("isAttacking", true);
            Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
            requestedEnemy = null;
        }


    }

    public void death()
    {
        controller.anim.SetBool("Die", true);

    }

    public void boostStats(int choice)
    {
        statRetention = 5;
        statsBoosted = true;
        Debug.Log("BoostedStats");
        Debug.Log(choice);
        switch (choice)
        {
            case 0:
                currentHealth += UnityEngine.Random.Range(0, 4);
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                break;
            case 1:
                armour_class += UnityEngine.Random.Range(0, 2);
                break;

            default:
                return;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<Skeleton_controller>();
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);

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
