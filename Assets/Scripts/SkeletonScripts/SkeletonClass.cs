using UnityEngine;

public class SkeletonClass : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 20;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 3;
    public GameObject requestedEnemy { get; set; } = null;
    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }

    public void attack()
    {
    }

    public void death()
    {
        GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupiedBy = null;
        GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)].occupied = false;
        gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            death();
        } 
    }
}
