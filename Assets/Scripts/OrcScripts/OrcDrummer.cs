using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcDrummer : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 40;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public bool playDrumAction = false;
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 9;
    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public Drummer_animation_controller controller;
    public GameObject prefab;

    public void attack()
    {
        if (requestedEnemy == null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth -= UnityEngine.Random.Range(1, 18);
            Debug.Log("Player Health " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth);
            controller.anim.SetBool("isAttacking", true);

        } else
        {
            gameObject.transform.LookAt(requestedEnemy.GetComponent<Transform>());
            requestedEnemy.GetComponent<Characters>().currentHealth -= UnityEngine.Random.Range(1, 18);
            controller.anim.SetBool("isAttacking", true);
            Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
            requestedEnemy = null;
        }
    }

    private void playDrum()
    {
        controller.anim.SetBool("Drum_Playing", true);
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Enemy");
        textController.showText(gameObject, gameObject, "Drumming");
        if (gameobjects == null)
        {
            // Debug.Log("null");
        }
        if (gameobjects != null)
        {
            foreach (GameObject enemy in gameobjects)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 1000f)
                {
                    if (enemy == null)
                    {
                        Debug.Log("null");
                    }
                    enemy.GetComponent<Characters>().chasing = true;

                }
            }
        }
    }
    public void death()
    {
        controller.anim.SetBool("Die", true);
    }

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        } else
        {
            if (playDrumAction)
            {
                playDrum();
            }
        }
    }
   
 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = gameObject.GetComponent<Drummer_animation_controller>();
        attackType = Attacksvulnerablities.attackTypes.Blunt;
        vulnerability = Attacksvulnerablities.attackTypes.Sharp;
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);


    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0 )
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
