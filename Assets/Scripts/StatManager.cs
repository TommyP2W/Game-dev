using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int healthTier = 0;
    public static int damageTier = 0;
    public static int playerLevel = 0;
    public static int armourTier = 0;
    public static int staminaTier = 0;

    public static int experience = 0;

    public static int prevHealth = 0;
    public static int prevDamage = 0;
    public static int prevArmour = 0;
    public static int prevStamina = 0;

    public bool upgradePlayerLevel = false;

    public static bool damageClass = false;
    public static bool armourClass = false;
    public static bool staminaClass = false;

    public static bool finishedLevel2 = false;
    public static bool finishedLevel3 = false;
    public static bool finishedLevel4 = false;
    public static bool finishedLevel5 = false;
    public static StatManager instance;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        healthTier = 0;
        damageTier = 0;
        playerLevel = 0;
        armourTier = 0;
        staminaTier = 0;
        damageClass = false;
        armourClass = false;
        staminaClass = false;

        finishedLevel2 = false;
        finishedLevel3 = false;
        finishedLevel4 = false;
        finishedLevel5 = false;

    }

    public static void resetStats()
    {
           healthTier = 0;
        damageTier = 0;
       playerLevel = 0;
       armourTier = 0;
       staminaTier = 0;

       experience = 0;

        prevHealth = 0;
         prevDamage = 0;
        prevArmour = 0;
       prevStamina = 0;

        damageClass = false;
         armourClass = false;
         staminaClass = false;

        finishedLevel2 = false;
        finishedLevel3 = false;
        finishedLevel4 = false;
        finishedLevel5 = false;
    }

    void Start()
    {
        healthTier = 0;
        damageTier = 0;
        playerLevel = 0;
        armourTier = 0;
        staminaTier = 0;
        damageClass = false;
        armourClass = false;
        staminaClass = false;

        finishedLevel2 = false;
        finishedLevel3 = false;
        finishedLevel4 = false;
        finishedLevel5 = false;
        // DontDestroyOnLoad(gameObject);
    }

    public void upgradePlayerLevelHelper()
    {
        upgradePlayerLevel = false;
        experience = 0;
        playerLevel++;
    }
    // Update is called once per frame
    void Update()
    {
        if (experience >= 100)
        {
            upgradePlayerLevel = true;
            
        }
        if (upgradePlayerLevel)
        {
            upgradePlayerLevelHelper();
        }
    }
}