using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMinerClass : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 20;
    public bool chasing { get; set; } = false;
    public bool isWalking { get; set; } = false;
    public bool attackAction { get; set; } = false;
    public int armour_class { get; set; } = 8;
    public int damage_upper = 25;


    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }


    public DwarfController controller;
    public GameObject prefab;



    public void attack()
    {
        // If the attack is directed at the player

        if (requestedEnemy == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.LookAt(player.GetComponent<Transform>());
            controller.anim.SetBool("isAttacking", true);
            SoundManager.instance.playDwarfMelee();
            int damage = UnityEngine.Random.Range(20, damage_upper);
            // If the attack misses or not
            if (damage > player.GetComponent<PlayerClass>().armor_class)
            {

        
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
            Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
            requestedEnemy = null;
        }
    }

    public void death()
    {
        controller.anim.SetBool("Die", true);
        StatManager.experience += 2;
    }

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }


    //}
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<DwarfController>();
        attackType = Attacksvulnerablities.attackTypes.Sharp;
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attackAction);
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
