using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orcTrapper : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 25;
    public bool chasing { get; set; } = false;
    public bool isWalking { get; set; } = false;
    public GameObject requestedEnemy { get; set; } = null;

    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 8;
    public Attacksvulnerablities.attackTypes vulnerability { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Attacksvulnerablities.attackTypes attackType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public RegOrcAnimationController controller;

    [SerializeField] public GameObject trap;
    // Attack function for enemy orc warrior
    public void attack()
    {
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
            controller.anim.SetBool("isAttacking", true);
            Debug.Log("Enemy Health " + requestedEnemy.GetComponent<Characters>().currentHealth);
            requestedEnemy = null;
        }
    }
    public void layTrap()
    {
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(transform.position)]);
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (!neighbours[i].armed)
            {
                Vector3 position = GridManager.grid.CellToWorld(neighbours[i].position);
                position.x += 0.5f;
                position.z += 0.5f;
                GameObject bear_trap = Instantiate(trap, position, Quaternion.identity);
                bear_trap.transform.Rotate(180f,0,0);
                bear_trap.SetActive(true);
                return;
            }
        }
    }

    // Death function, if dead, deactivate
    public void death()
    {
        controller.anim.SetBool("Die", true);
    }

    // If trigger enter player, chase
    private void OnTriggerEnter(Collider other)
    {
        // If collided with player
        if (other.CompareTag("Player"))
        {
            chasing = true;
        }
    }
    // If trigger exit player, stop chasing
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
        controller = gameObject.GetComponent<RegOrcAnimationController>();
        currentHealth = maxHealth;
        chasing = false;

        attackType = Attacksvulnerablities.attackTypes.Sharp;
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
    }

    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        } else
        {
            if (UnityEngine.Random.Range(0,2) == 1)
            {
                layTrap();
            }
        }
    }
}
