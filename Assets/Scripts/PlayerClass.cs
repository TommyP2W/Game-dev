using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.TextCore.Text;

public class PlayerClass : MonoBehaviour
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 100;
    public int maxStamina { get; set; } = 10;
    public int currentStamina { get; set; }
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public static bool movementBlocked { get; set; } = false;
    // Initialise this and put as upper part of range tomorrow for both damage types;
    public int damage_upper = 8;
    
    public int max_sanity = 100;
    public int current_sanity;
    public int experience = 0;
    public int risk = 0;

    public int armor_class = 7;
    private float shake = 0.0f;
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    private GameObject cam;
    public List<GridCell> ReqPlayerMovement;
    public PlayerAnimationController playerAnimController;
    public GameObject RequestedEnemy;
    public GameObject possessedEnemy;


    public Attacksvulnerablities.attackTypes attackType;
    public Attacksvulnerablities.attackTypes vulnerabilities;
    public Volume vim;
    public Vignette hurt;

    public void attack(string requested_attack)
    {
        if (RequestedEnemy != null && currentStamina != 0)
        {
            if (requested_attack.Equals("Melee") && currentStamina >= 2)
            {
                SoundManager.instance.playSwordSlash();
                // Checking if the enemy is within melee range
                foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]))
                {
                    if (cell.occupiedBy == RequestedEnemy)
                    {
                        
                        gameObject.transform.LookAt(RequestedEnemy.transform.position);
                        playerAnimController.startAttack();
                        int damage = UnityEngine.Random.Range(1, 20);
                        if (damage > RequestedEnemy.GetComponent<Characters>().armour_class)
                        {
                            playerAnimController.startAttack();
                            if (attackType == RequestedEnemy.GetComponent<Characters>().vulnerability)
                            {
                                RequestedEnemy.GetComponent<Characters>().currentHealth -= (int)(damage * 1.5f);
                            } else
                            {
                                RequestedEnemy.GetComponent<Characters>().currentHealth -= damage;
                            }


                            textController.showText(gameObject,RequestedEnemy, "Attack", damage: damage);

                            StartCoroutine(Flash.flash(RequestedEnemy));
                        }
                        else
                        {
                            textController.showText(gameObject, RequestedEnemy, "Attack");
                        }
                        currentStamina -= 2;
                        break;
                    } 
                }
            }
            else if (requested_attack.Equals("Lightning") && currentStamina >= 7)
            {
                gameObject.transform.LookAt(RequestedEnemy.transform.position);
                GameObject Lightning = GameObject.Find("Lightning");
                if (Lightning != null)
                {
                    Lightning.SetActive(true);
                    Lightning.GetComponent<Transform>().position = RequestedEnemy.transform.position;
                    StartCoroutine(LightningCountdown());
                    Debug.Log("Health before damage : " + RequestedEnemy.GetComponent<Characters>().currentHealth);

                    RequestedEnemy.GetComponent<Characters>().currentHealth -= UnityEngine.Random.Range(4, damage_upper + 3);

                    Debug.Log("Health after damage : " + RequestedEnemy.GetComponent<Characters>().currentHealth);

                    currentStamina -= 7;
                }

            }
            if (RequestedEnemy.GetComponent<Characters>().currentHealth <= 0)
            {
                RequestedEnemy.hideFlags = HideFlags.HideAndDontSave;
                // RequestedEnemy.SetActive(false);
            }
        }
    }

    //public void healthVisual()
    //{
        
    //}

    public void death()
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        ReqPlayerMovement = new List<GridCell>();
        playerAnimController = gameObject.GetComponent<PlayerAnimationController>();
    }

  
    // Update is called once per frame
    void Update()
    {

     

        if (currentHealth < maxHealth)
        {
            if (vim.profile.TryGet<Vignette>(out hurt))
            {
                hurt.intensity.Override(-((float)currentHealth / 100));
            }
        }
    }
    public void Start()
    {
        if (StatManager.healthTier != 0)
        {
            maxHealth = StatManager.prevHealth;
        }
    
        currentHealth = maxHealth;

        if (StatManager.damageTier != 0)
        {
            damage_upper = StatManager.prevDamage;
        } else
        {
            damage_upper = 8;
        }
        
        if (StatManager.armourTier == 0)
        {
            armor_class = 7;
        } else
        {
            armor_class = StatManager.prevArmour;
        }
        if (StatManager.staminaTier != 0)
        {
            maxStamina = StatManager.prevStamina;
        }
        currentStamina = maxStamina;
        current_sanity = max_sanity;

        attackType = Attacksvulnerablities.attackTypes.Sharp;
        vulnerabilities = Attacksvulnerablities.attackTypes.Sharp;

        cam = GameObject.FindGameObjectWithTag("CameraPivot");
        vim = GameObject.Find("pp").GetComponent<Volume>();

    }

public IEnumerator LightningCountdown(float interval = 2)
    {
        while (interval > 0)
        {
            yield return new WaitForSeconds(interval);
            interval--;
        }
        GameObject.Find("Lightning").SetActive(false);
        yield return null;

    }
}
