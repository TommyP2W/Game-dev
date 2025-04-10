using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Warrior : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 30;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public void attack()
    {
    }

    public void death()
    {
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
