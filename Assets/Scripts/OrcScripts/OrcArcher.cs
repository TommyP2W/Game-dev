using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcArcher : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 20;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 6;
    public int damage_upper = 12;

    public GameObject requestedEnemy { get; set; } = null;
    public RegOrcAnimationController controller;


    public void attack()
    {
        // If the attack is directed at the player
        if (requestedEnemy == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.LookAt(player.GetComponent<Transform>());
            int damage = UnityEngine.Random.Range(1, damage_upper);
            // If the attack misses or not
            if (damage > player.GetComponent<PlayerClass>().armor_class) { 

                player.GetComponent<PlayerClass>().currentHealth -= UnityEngine.Random.Range(1, damage);
                controller.anim.SetBool("isAttacking", true);
                textController.showText(gameObject,player, "Attack", damage: damage);


                attackAction = false;   
            } else

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
        gameObject.SetActive(false);
    }

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // If collided with player
    //    if (other.CompareTag("Player"))
    //    {
    //        chasing = true;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (!EndTurn.turnEnd)
    //    {
    //        chasing = false;
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<RegOrcAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attackAction);
        if (currentHealth <= 0)
        {
            death();
        }
    }
}
