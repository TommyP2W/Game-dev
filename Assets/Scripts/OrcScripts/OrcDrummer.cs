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
    public Drummer_animation_controller controller;

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
   
    private void OnTriggerEnter(Collider other)
    {
        // If collided with player
        if (other.CompareTag("Player"))
        {
            chasing = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!EndTurn.turnEnd)
        {
            chasing = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = gameObject.GetComponent<Drummer_animation_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
