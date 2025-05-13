using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleClass : MonoBehaviour, Characters
{
    // Start is called before the first frame update
   
        public int currentHealth { get; set; }
        public int maxHealth { get; set; } = 25;
        public bool chasing { get; set; } = false;
        public bool isWalking { get; set; } = false;
        public bool isFlying { get; set; } = false;
        public GameObject requestedEnemy { get; set; } = null;

        public bool attackAction { get; set; }
        public int armour_class { get; set; } = 8;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public int number_of_attacks = 0;
    
        public gargoyleController controller;
    public GameObject prefab;

        // Attack function for enemy orc warrior
        public void attack()
        {
            number_of_attacks++;
            if (requestedEnemy == null)
            {
                gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth -= UnityEngine.Random.Range(1, 12);
                controller.anim.SetBool("isAttacking", true);
                Debug.Log("Player Health " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth);

                attackAction = false;
            }
            else
            {
                gameObject.transform.LookAt(requestedEnemy.GetComponent<Transform>());
                requestedEnemy.GetComponent<Characters>().currentHealth -= UnityEngine.Random.Range(1, 12);
            controller.anim.SetBool("isFlying", false);
                controller.anim.SetBool("isAttacking", true);
                Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
                requestedEnemy = null;
            }
            number_of_attacks++;
            if (number_of_attacks == 2)
            {
                number_of_attacks = 0;
                attackAction = false;
            }
        }
     
        // Death function, if dead, deactivate
        public void death()
        {
            controller.anim.SetBool("Die", true);
        StatManager.experience += 7;
        }

        // If trigger enter player, chase
       
        // Start is called before the first frame update
        void Start()
        {
            controller = gameObject.GetComponent<gargoyleController>();
            currentHealth = maxHealth;
            chasing = false;
            Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);

        }

    // Update is called once per frame
    // Checking if the health is below or equal to zero, if so cause its death
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
            if (controller.anim.GetCurrentAnimatorStateInfo(0).IsName("attacking") && controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
               attack();
            }
        Debug.Log(chasing);
        }  

        public void actionSelector()
        {
            if (attackAction)
            {
            
                attack();

            }
            
            }
           
        }
    


