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
    public GameObject RequestedEnemy;
    public Animator animator;
    public void attack()
    {
        if (RequestedEnemy != null)
        {
            RequestedEnemy.GetComponent<Characters>().currentHealth -= 5;
            Debug.Log("Current enemy health : " + RequestedEnemy.GetComponent<Characters>().currentHealth);
            animator.SetBool("isAttacking", true);
        }
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
    public void Start()
    {
        animator = GetComponent<Animator>();

    }
}
