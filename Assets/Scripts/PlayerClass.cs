using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public List<GridCell> ReqPlayerMovement;
    public void attack(GameObject character)
    {
        
    }

    public void death()
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        ReqPlayerMovement = new List<GridCell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
