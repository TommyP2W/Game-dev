using UnityEngine;

public class SkeletonClass : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 20;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    public bool attackAction { get; set; }
    public int armour_class { get; set; } = 3;
    public int damage_upper = 15;

    public GameObject requestedEnemy { get; set; } = null;
    public Attacksvulnerablities.attackTypes vulnerability { get; set; }
    public Attacksvulnerablities.attackTypes attackType { get; set; }

    public Skeleton_controller controller;
    public GameObject prefab;
    public void actionSelector()
    {
        if (attackAction)
        {
            attack();
        }
    }

    public void attack()
    {
        SoundManager.instance.playUnarmedOrcAttack();
        controller.anim.SetBool("isAttacking", true);

        if (requestedEnemy == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            gameObject.transform.LookAt(player.GetComponent<Transform>());
            int AC_Check = UnityEngine.Random.Range(1, 20);
            // If the attack misses or not
            if (AC_Check > player.GetComponent<PlayerClass>().armor_class)
            {

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
        StatManager.experience += 4;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<Skeleton_controller>();

        if (controller == null)
        {
            Debug.Log("NULL");
        } else
        {
            Debug.Log("NOT NULL");
        }
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity, transform);

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
            SoundManager.instance.playSkeletonDeath();
            gameObject.SetActive(false);
        }
    }
}
