using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class PlayerClass : MonoBehaviour
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 100;
    public int maxStamina { get; set; } = 10;
    public int currentStamina { get; set; }
    public bool chasing { get; set; }
    public bool isWalking { get; set; }
    // Initialise this and put as upper part of range tomorrow for both damage types;
    public int damage_upper = 8;
    
    public int sanity = 10;
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
    
    
    
    public void attack(string requested_attack)
    {
        if (RequestedEnemy != null && currentStamina != 0)
        {
            if (requested_attack.Equals("Melee") && currentStamina >= 2)
            {
                // Checking if the enemy is within melee range
                foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]))
                {
                    if (cell.occupiedBy == RequestedEnemy)
                    {
                        
                        gameObject.transform.LookAt(RequestedEnemy.transform.position);
                        playerAnimController.startAttack();
                        if (UnityEngine.Random.Range(1, 20) > RequestedEnemy.GetComponent<Characters>().armour_class)
                        {
                            //shake += 2;
                            playerAnimController.startAttack();
                            int damage = UnityEngine.Random.Range(1, damage_upper);
                            RequestedEnemy.GetComponent<Characters>().currentHealth -= damage;
                            Debug.Log("Current enemy health : " + RequestedEnemy.GetComponent<Characters>().currentHealth);
                            AttackManager.showAttackInfo(RequestedEnemy, damage);
                        }
                        //currentStamina -= 2;
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

      //  Debug.Log(RequestedEnemy.name);
        //if (shake > 0)
        //{
        //    cam.transform.localPosition = UnityEngine.Random.insideUnitSphere * shakeAmount;
        //    shake -= Time.deltaTime * decreaseFactor;
        //} else
        //{
        //    shake = 0.0f;
        //}
    }
    public void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        damage_upper = 8;
        armor_class = 7;
        cam = GameObject.FindGameObjectWithTag("CameraPivot");

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
