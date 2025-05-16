

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo :MonoBehaviour {

    private GameObject PlayerInfoWindow;
    private GameObject slider;
    //private int healthTier = 0;
    //private int damageTier = 0;
    //private int armourTier = 0;
    //private int staminaTier = 0;

    private PlayerClass playerClass;
    private GameObject skillInformation;
    private GameObject sanityInformation;
    private GameObject experienceBar;
    private GameObject sanityBar;
    public StatManager stats;

    private TextMeshProUGUI playerLevel;

    private TextMeshProUGUI DamageTierInfo;
    private TextMeshProUGUI MaxHealthInfo;
    private TextMeshProUGUI MaxStaminaInfo;
    private TextMeshProUGUI ACInfo;
    private TextMeshProUGUI STInfo;

    public void Start()
    {
        stats = GameObject.Find("StatManager").GetComponent<StatManager>();
        PlayerInfoWindow = GameObject.Find("PlayerInfo");
        Debug.Log("name " + PlayerInfoWindow.name);


        if (stats != null)
        {
            Debug.Log(stats.name);
        } else
        {
            Debug.Log("null");
        }
        upgradeArmourHelper(StatManager.armourTier);
        upgradeHealthHelper(StatManager.healthTier);
        upgradeDamageHelper(StatManager.damageTier);
        upgradeStaminaHelper(StatManager.staminaTier);

        if (StatManager.damageClass)
        {
            upgradeDamage();
        } 
        if (StatManager.armourClass)
        {
            upgradeTank();
        }
        if (StatManager.staminaClass)
        {
            upgradeStamina();
        }


        PlayerInfoWindow.SetActive(false);

    }
    public void Awake()
    {
           // Colour brown for wooden sword
        Color colour = new Color(150f/255, 70f/255, 0f/255);

        // Changing the colour of the material for the sword and the player, supposed to represent wood as the lowest level of equipment.
        GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = colour;
        GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = colour;

        // Slight issue here, character model does not separate armour and skin, so changes the entire model colour for now.
        GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = colour;
        GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = colour;

        // Deactivating the skill panel on startup
// skillInformation = GameObject.Find("SkillInformation");
        // skillInformation.SetActive(false);
        experienceBar = GameObject.Find("ExperienceBar");
        sanityBar = GameObject.Find("SanityBar");
        //experienceBar.SetActive(false);
        //sanityBar.SetActive(false);

        //PlayerInfoWindow = GameObject.Find("PlayerInfo");
        slider = GameObject.Find("PreviewSlider");
        skillInformation = GameObject.Find("SkillInformation");
        sanityInformation = GameObject.Find("SanityInformation");

        // Getting the references for now, can set the references before deactivation
        DamageTierInfo = GameObject.Find("DamageTierInfo").GetComponent<TextMeshProUGUI>();
        MaxHealthInfo = GameObject.Find("MaxHealthInfo").GetComponent<TextMeshProUGUI>();
        MaxStaminaInfo = GameObject.Find("MaxStaminaInfo").GetComponent<TextMeshProUGUI>();
        ACInfo = GameObject.Find("ACInfo").GetComponent<TextMeshProUGUI>();
        STInfo = GameObject.Find("MaxStaminaInfo").GetComponent<TextMeshProUGUI>();
        playerLevel = GameObject.Find("PlayerLevel").GetComponent<TextMeshProUGUI>();


        //PlayerInfoWindow.SetActive(false);
        playerClass = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>();
 
    }


 
    // Responsible for opening info panel when button clicked.
    public void openInfo()
    {
        if (PlayerInfoWindow.activeInHierarchy)
        {
            PlayerInfoWindow.SetActive(false);
            return;
        }

        PlayerInfoWindow.SetActive(true);
        ACInfo.text = "Armour class : " + playerClass.armor_class;
        Debug.Log(playerClass.armor_class);
        STInfo.text = "Max Stamina : " + playerClass.maxStamina;
        DamageTierInfo.text = "Damage Tier : " + StatManager.damageTier;
        MaxHealthInfo.text = "Max Health : " + playerClass.maxHealth;
        MaxStaminaInfo.text = "Max Stamina : " + playerClass.maxStamina;
        playerLevel.text = "Player Level : " + StatManager.playerLevel;
        sanityBar.GetComponent<Slider>().value = (float)playerClass.current_sanity / 100;
        experienceBar.GetComponent<Slider>().value = (float)StatManager.experience * 0.01f;

        sanityInformation.GetComponentInChildren<TextMeshProUGUI>().text = "Current Sanity Level : " + playerClass.current_sanity + "\n Risk Level : " + Sanity.risk;


        if (sanityBar.GetComponent<Slider>().value >= 0.50f && sanityBar.GetComponent<Slider>().value < 0.75f)
        {
            GameObject.Find("SanityFill").GetComponent<Image>().color = Color.yellow;
        }
        if (sanityBar.GetComponent<Slider>().value >= 0.25f && sanityBar.GetComponent<Slider>().value < 0.50f)
        {
            GameObject.Find("SanityFill").GetComponent<Image>().color = Color.magenta;
        }
        if (sanityBar.GetComponent<Slider>().value < 0.25f)
        {
            GameObject.Find("SanityFill").GetComponent<Image>().color = Color.red;
        }



        return;
    }

    public void upgradeHealth(int requested_tier)
    {
        if (requested_tier == 1) {
            StatManager.playerLevel = 1;
            if (StatManager.healthTier == 0 && StatManager.playerLevel == 1)
            {
                StatManager.prevHealth = playerClass.maxHealth + 5;
                StatManager.healthTier = 1;
                upgradeHealthHelper(1);
            }
        } else if (requested_tier == 2) {
            StatManager.playerLevel = 2;
            if (StatManager.healthTier == 1 && StatManager.playerLevel == 2)
            {
                StatManager.prevHealth = playerClass.maxHealth + 10;
                StatManager.healthTier = 2;
                upgradeHealthHelper(2);

            }
        } else if (requested_tier == 3) {
            StatManager.playerLevel = 3;
            if (StatManager.healthTier == 2 && StatManager.playerLevel == 3)
            {
                StatManager.healthTier = 3;
                StatManager.prevHealth = playerClass.maxHealth + 15;
                upgradeHealthHelper(3);
            }
        }
        MaxHealthInfo.text = "Max Health : " + playerClass.maxHealth;
        return;
    }

    public void upgradeHealthHelper(int tier) 
    {
        switch (tier)
        {
            case 1:
                playerClass.maxHealth = StatManager.prevHealth;
                GameObject.Find("Tier1HP").GetComponentInChildren<Button>().interactable = false;
                break;
            case 2:
                playerClass.maxHealth = StatManager.prevHealth;
                GameObject.Find("Tier1HP").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("Tier2HP").GetComponentInChildren<Button>().interactable = false;
                break;
            case 3:
                playerClass.maxHealth = StatManager.prevHealth;
                GameObject.Find("Tier1HP").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("Tier2HP").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("Tier3HP").GetComponentInChildren<Button>().interactable = false;
                break;
        }
    }

    public void upgradeArmour(int requested_tier)
    {
        if (requested_tier == 1)
        {
            StatManager.playerLevel = 1;
            if (StatManager.armourTier == 0 && StatManager.playerLevel == 1)

            {
                StatManager.prevArmour = playerClass.armor_class + 2;

                StatManager.armourTier = 1;
                upgradeArmourHelper(1);
               
            }
        }
        else if (requested_tier == 2)
        {
            StatManager.playerLevel = 2;
            if (StatManager.armourTier == 1 && StatManager.playerLevel == 2)
            {
                StatManager.prevArmour = playerClass.armor_class + 2;
                StatManager.armourTier = 2;
                upgradeArmourHelper(2);

            }
        }
        else if (requested_tier == 3)
        {
            StatManager.playerLevel = 3;
            if (StatManager.armourTier == 2 && StatManager.playerLevel == 3)
            {
                StatManager.prevArmour = playerClass.armor_class + 2;
                StatManager.armourTier = 3;
                upgradeArmourHelper(3);
            }
        }
        ACInfo.text = "Armour class : " + playerClass.armor_class;
        return;
    }
    public void upgradeArmourHelper(int tier)
    {
        switch (tier)
        {
            case 1:
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.grey;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.grey;

                GameObject.Find("Tier1AC").GetComponentInChildren<Button>().interactable = false;
                playerClass.armor_class = StatManager.prevArmour;
                break;
            case 2:
                playerClass.armor_class = StatManager.prevArmour;
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.green;

                GameObject.Find("Tier1AC").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2AC").GetComponentInChildren<Button>().interactable = false;
                break;
            case 3:
                playerClass.armor_class = StatManager.prevArmour;
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.yellow;

                GameObject.Find("Tier1AC").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2AC").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("Tier3AC").GetComponentInChildren<Button>().interactable = false;
                break;

        }
    }
    // Onclick event functions, was supposed to be all integrated into showSkillInfo but unity does not allow event functions with more than one parameter inputs.
    public void showDamageInfo(int tier)
    {
        showSkillInfo("Damage", tier);
    }
    public void showHealthInfo(int tier)
    {
        showSkillInfo("Health", tier);

    }
    public void showArmourInfo(int tier)
    {
        showSkillInfo("Damage", tier);
    }
    public void showStaminaInfo(int tier)
    {
        showSkillInfo("Stamina", tier);
    }

    public void showSkillInfo(string infoType, int tier)
    {

        // Using dictionaries currently rather than separate functions for skill information display
        skillInformation.SetActive(true);

        if (infoType.Equals("Health"))
        {
            Dictionary<int, int> healthinfo = new Dictionary<int, int> { { 1, 1 }, { 2, 10 }, { 3, 15 } };
            skillInformation.GetComponentInChildren<TextMeshProUGUI>().text = "Health Tier " + tier + " Upgrade \n Increases max health by :" + healthinfo[tier];
        } else if (infoType.Equals("Damage"))
        {
            Dictionary<int, int> damageinfo = new Dictionary<int, int> { { 1, 2 }, { 2, 3 }, { 3, 3 } };
            skillInformation.GetComponentInChildren<TextMeshProUGUI>().text = "Damage Tier " + tier + " Upgrade \n Increases upper boundary damage by : " + damageinfo[tier];
        } else if (infoType.Equals("AC"))
        {
            Dictionary<int, int> ACInfo = new Dictionary<int, int> { { 1, 2 }, { 2, 2 }, { 3, 2 } };
            skillInformation.GetComponentInChildren<TextMeshProUGUI>().text = "Armour Tier " + tier + " Upgrade \n Increases armour class by : " + ACInfo[tier];

        } else if (infoType.Equals("Stamina"))
        {
            Dictionary<int, int> STInfo = new Dictionary<int, int> { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            skillInformation.GetComponentInChildren<TextMeshProUGUI>().text = "Stamina Tier " + tier + " Upgrade \n Increases max stamina by : " + STInfo[tier];

        }
    }
   
    public void hideInfo()
    {
        skillInformation.GetComponentInChildren<TextMeshProUGUI>().text = "";
        skillInformation.SetActive(false);
    }
    public void upgradeDamage(int requested_tier)
    {
        if (requested_tier == 1)
        {
            StatManager.playerLevel = 1;

            if (StatManager.damageTier == 0 && StatManager.playerLevel == 1)

            {

                StatManager.damageTier = 1;
                StatManager.prevDamage = playerClass.damage_upper  + 2;
                upgradeDamageHelper(1);
            
            }
        }
        else if (requested_tier == 2)
        {
            StatManager.playerLevel = 2; 
            if (StatManager.damageTier == 1 && StatManager.playerLevel == 2)
            {

                StatManager.damageTier = 2;
                StatManager.prevDamage = playerClass.damage_upper + 3;
                upgradeDamageHelper(2);


            }
        }
        else if (requested_tier == 3)
        {
            StatManager.playerLevel = 3;
            if (StatManager.damageTier == 2 && StatManager.playerLevel == 3)

            {

                StatManager.damageTier = 3;
                StatManager.prevDamage = playerClass.damage_upper + 4;
                upgradeDamageHelper(3);

            }
        }
        DamageTierInfo.text = "Damage Tier : " + StatManager.damageTier + " Upper boundary : " + playerClass.damage_upper;
        return;
    }

    public void upgradeDamageHelper(int tier)
    {
        switch (tier)
        {
            case 1:
                playerClass.damage_upper = StatManager.prevDamage;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("Tier1DMG").GetComponentInChildren<Button>().interactable = false;
                break;
            case 2:
                playerClass.damage_upper = StatManager.prevDamage;
                GameObject.Find("Tier1DMG").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2DMG").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.yellow;

                break;
            case 3:
                playerClass.damage_upper = StatManager.prevDamage;
                GameObject.Find("Tier1DMG").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2DMG").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier3DMG").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.green;
                break;
        }
    }
    public void upgradeStamina(int requested_tier)
    {
        if (requested_tier == 1)
        {
            StatManager.playerLevel = 1;
            if (StatManager.staminaTier == 0 && StatManager.playerLevel == 1)
            {

                StatManager.staminaTier = 1;
                StatManager.prevStamina = playerClass.maxStamina + 2;
                upgradeStaminaHelper(1);                
            }
        }
        else if (requested_tier == 2)
        {
            StatManager.playerLevel = 2;
            if (StatManager.staminaTier == 1 && StatManager.playerLevel == 2)
            {

                StatManager.staminaTier = 2;
                StatManager.prevStamina = playerClass.maxStamina + 3;

                upgradeStaminaHelper(2);
            }
        }
        else if (requested_tier == 3)
        {
            StatManager.playerLevel = 3;
            if (StatManager.staminaTier == 2 && StatManager.playerLevel == 3)
            {

                StatManager.staminaTier = 3;
                StatManager.prevStamina = playerClass.maxStamina + 4;
                upgradeStaminaHelper(3);
            }
        }
        STInfo.text = "Max Stamina : " + playerClass.maxStamina;
        return;
    }
    public void upgradeStaminaHelper(int tier)
    {
        switch (tier)
        {
            case 1:
                playerClass.maxStamina = StatManager.prevStamina;
                GameObject.Find("Tier1ST").GetComponentInChildren<Button>().interactable = false;

                break;
            case 2:
                playerClass.maxStamina = StatManager.prevStamina;
                GameObject.Find("Tier1ST").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2ST").GetComponentInChildren<Button>().interactable = false;
                break;
            case 3:
                playerClass.maxStamina = StatManager.prevStamina;
                GameObject.Find("Tier1ST").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier2ST").GetComponentInChildren<Button>().interactable = false;

                GameObject.Find("Tier3ST").GetComponentInChildren<Button>().interactable = false;

                break;
        }
    }
    public void SpinCharacterPreview()
    {


        GameObject.Find("PreviewCharacter").GetComponent<Transform>().rotation = Quaternion.Euler(0, slider.GetComponent<Slider>().value * 360, 0);

    }

    public void upgradeTank()
    {
        if (StatManager.armourTier == 3 && StatManager.healthTier == 3)
        {
            StatManager.armourClass = true;

            //StatManager.armourTier = 4;
            StatManager.prevArmour = playerClass.armor_class + 4;
            playerClass.armor_class = StatManager.prevArmour;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 0.75f);
            GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.cyan;

        }
    }
    public void upgradeDamage()
    {
        if (StatManager.damageTier == 3)
        {
            StatManager.damageClass = true;
            //StatManager.damageTier = 4;
            StatManager.prevDamage = playerClass.damage_upper + 10;
            playerClass.damage_upper = StatManager.prevDamage;
            GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.magenta;
            GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.magenta;
        }
    }
    public void upgradeStamina()
    {
        if (StatManager.staminaTier == 3)
        {
            StatManager.staminaClass = true;
            //StatManager.damageTier = 4;
            StatManager.prevStamina = playerClass.maxStamina + 6;
            playerClass.maxStamina = StatManager.prevStamina;
            GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.magenta;
            GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.magenta;
        }
    }
   
}