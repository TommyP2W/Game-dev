using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarrior : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 25;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public bool attackAction { get; set; }
    public RegOrcAnimationController controller;
    public void attack()
    {
        gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth -= UnityEngine.Random.Range(1, 12);
        controller.anim.SetBool("isAttacking", true);
        Debug.Log("Player Health " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth);

        attackAction = false;
    }

    public void death()
    {
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
    public void selectAction()
    {
        if (attackAction)
        {
            attack();
        }
       
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<RegOrcAnimationController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }
}
