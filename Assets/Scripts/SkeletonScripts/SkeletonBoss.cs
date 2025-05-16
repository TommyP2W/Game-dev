using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SkeletonBoss : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 45;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 15;
    public int damage_upper = 19;
    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public bool statsBoosted = false;
    public int statRetention = 0;
    public Skeleton_controller controller;
    public GameObject prefab;
    public GameObject victoryScreen;
    public void actionSelector()
    {
        if (statRetention > 0)
        {
            statRetention--;
        } else
        {
            statsBoosted = false;
        }
        if (attackAction)
        {
            attack();
        }
        if (!statsBoosted)
        {
            boostStats(UnityEngine.Random.Range(0,3));
        }
    }

    public void attack()
    {
        if (requestedEnemy == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.LookAt(player.GetComponent<Transform>());
            int AC_Check = UnityEngine.Random.Range(1, 20);
            // If the attack misses or not
            if (AC_Check > player.GetComponent<PlayerClass>().armor_class)
            {

                controller.anim.SetBool("isAttacking", true);
                int damage = UnityEngine.Random.Range(1, damage_upper);

                if (player.GetComponent<PlayerClass>().vulnerabilities == attackType)
                {
                    if (UnityEngine.Random.Range(0, 4) == 3)
                    {
                        Debug.Log("Original damage" + damage);
                        damage = (int)(damage * 1.5f);

                    }
                }

                player.GetComponent<PlayerClass>().currentHealth -= damage;

                textController.showText(gameObject, player, "Attack", damage: damage);
                attackAction = false;
            }
            else

            {
                textController.showText(gameObject, player, "Attack");
            }
        }
        else
        {
            int AC_Check = UnityEngine.Random.Range(1, 20);
            // If the attack misses or not
            if (AC_Check > requestedEnemy.GetComponent<Characters>().armour_class)
            {

                controller.anim.SetBool("isAttacking", true);
                int damage = UnityEngine.Random.Range(1, damage_upper);

                if (requestedEnemy.GetComponent<Characters>().vulnerability == attackType)
                {
                    if (UnityEngine.Random.Range(0, 4) == 3)
                    {
                        Debug.Log("Original damage" + damage);
                        damage = (int)(damage * 1.5f);

                    }
                }
                requestedEnemy.GetComponent<Characters>().currentHealth -= damage;

                textController.showText(gameObject, requestedEnemy, "Attack", damage: damage);
                attackAction = false;
            }
            else

            {
                textController.showText(gameObject, requestedEnemy, "Attack");
            }
        }



    }

    public void death()
    {
        controller.anim.SetBool("Die", true);

    }

    public void boostStats(int choice)
    {
        statRetention = 5;
        statsBoosted = true;
        Debug.Log("BoostedStats");
        Debug.Log(choice);
        switch (choice)
        {
            case 0:
                currentHealth += UnityEngine.Random.Range(0, 4);
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                break;
            case 1:
                armour_class += UnityEngine.Random.Range(0, 2);
                break;

            default:
                return;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<Skeleton_controller>();
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);
        victoryScreen = GameObject.Find("VictoryScreen");
        victoryScreen.SetActive(false);

    }

    // Update is called once per frame
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
            victoryScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
