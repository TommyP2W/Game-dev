using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarrior : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 25;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public void attack()
    {
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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
