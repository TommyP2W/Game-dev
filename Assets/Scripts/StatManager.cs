using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthTier = 0;
    public int damageTier = 0;
    public int playerLevel = 0;
    public int armourTier = 0;
    public int staminaTier = 0;

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
