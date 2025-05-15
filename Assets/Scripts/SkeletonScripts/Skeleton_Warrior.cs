using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Warrior : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 30;
    public bool chasing { get; set; } = false;
    public bool isWalking { get; set; } = false;
    public bool attackAction { get; set; } = false;
    public int armour_class { get; set; } = 15;
    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public int damage_upper = 25;
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public Skeleton_controller controller;

    public GameObject prefab;
    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }

    public void attack()
    {
        SoundManager.instance.playSwordSlash();
        if (requestedEnemy == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.LookAt(player.GetComponent<Transform>());
            int damage = UnityEngine.Random.Range(1, damage_upper);
            // If the attack misses or not
            if (damage > player.GetComponent<PlayerClass>().armor_class)
            {

                controller.anim.SetBool("isAttacking", true);
                if (player.GetComponent<PlayerClass>().vulnerabilities == attackType)
                {
                    Debug.Log("Original damage" + damage);
                    damage = (int)(damage * 1.5f);
                }
                player.GetComponent<PlayerClass>().currentHealth -= damage;

                textController.showText(gameObject, player, "Attack", damage: damage);



                attackAction = false;
            }
            else

            {
                textController.showText(gameObject, player, "Attack");
            }
        }
        else
        {
            gameObject.transform.LookAt(requestedEnemy.GetComponent<Transform>());
            requestedEnemy.GetComponent<Characters>().currentHealth -= UnityEngine.Random.Range(1, 12);
            controller.anim.SetBool("isAttacking", true);
            Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
            requestedEnemy = null;
        }
    }

    public void death()
    {
        controller.anim.SetBool("Die", true);
        StatManager.experience += 6;
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
            SoundManager.instance.playSkeletonWarriorDeath();
            gameObject.SetActive(false);
        }
    }
}
