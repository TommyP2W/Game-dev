using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int healthTier = 0;
    public static int damageTier = 0;
    public static int playerLevel = 0;
    public static int armourTier = 0;
    public static int staminaTier = 0;


    public static int prevHealth = 0;
    public static int prevDamage = 0;
    public static int prevArmour = 0;
    public static int prevStamina = 0;

    public static bool damageClass = false;
    public static bool armourClass = false;
    public static bool staminaClass = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        healthTier = 0;
        damageTier = 0;
        playerLevel = 0;
        armourTier = 0;
        staminaTier = 0;
       // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
