

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo :MonoBehaviour {

    private GameObject PlayerInfoWindow;
    private GameObject slider;
    private int healthTier = 0;
    private int damageTier = 0;
    private int playerLevel = 0;
    private int armourTier = 0;
    private int staminaTier = 0;

    private PlayerClass playerClass;
    private GameObject skillInformation;

    private TextMeshProUGUI DamageTierInfo;
    private TextMeshProUGUI MaxHealthInfo;
    private TextMeshProUGUI MaxStaminaInfo;
    private TextMeshProUGUI ACInfo;
    private TextMeshProUGUI STInfo;

    public void Start()
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
        skillInformation = GameObject.Find("SkillInformation");
        skillInformation.SetActive(false);

        PlayerInfoWindow = GameObject.Find("PlayerInfo");
        slider = GameObject.Find("PreviewSlider");

        // Getting the references for now, can set the references before deactivation
        DamageTierInfo = GameObject.Find("DamageTierInfo").GetComponent<TextMeshProUGUI>();
        MaxHealthInfo = GameObject.Find("MaxHealthInfo").GetComponent<TextMeshProUGUI>();
        MaxStaminaInfo = GameObject.Find("MaxStaminaInfo").GetComponent<TextMeshProUGUI>();
        ACInfo = GameObject.Find("ACInfo").GetComponent<TextMeshProUGUI>();
        STInfo = GameObject.Find("MaxStaminaInfo").GetComponent<TextMeshProUGUI>();


        PlayerInfoWindow.SetActive(false);
        playerClass = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>();       
    }

    // Responsible for opening info panel when button clicked.
    public void openInfo()
    {
        if (PlayerInfoWindow.activeSelf)
        {
            PlayerInfoWindow.SetActive(false);
            return;
        }

        PlayerInfoWindow.SetActive(true);
        ACInfo.text = "Armour class : " + playerClass.armor_class;
        STInfo.text = "Max Stamina : " + playerClass.maxStamina;
        DamageTierInfo.text = "Damage Tier : " + damageTier;
        MaxHealthInfo.text = "Max Health : " + playerClass.maxHealth;
        MaxStaminaInfo.text = "Max Stamina : " + playerClass.maxStamina;

        return;
    }

    public void upgradeHealth(int requested_tier)
    {
        if (requested_tier == 0) {
            playerLevel = 1;
            if (healthTier == 0 && playerLevel == 1)
            {
                healthTier = 1;
                playerClass.maxHealth += 5;
                GameObject.Find("Tier1HP").GetComponentInChildren<Button>().interactable = false;
;
            }
        } else if (requested_tier == 1) {
            playerLevel = 2;
            if (healthTier == 1 && playerLevel == 2)
            {
                healthTier = 2;
                playerClass.maxHealth += 10;
                GameObject.Find("Tier2HP").GetComponentInChildren<Button>().interactable = false;

            }
        } else if (requested_tier == 2) {
            playerLevel++;
            if (healthTier == 2 && playerLevel == 3)
            {
                healthTier = 3;
                playerClass.maxHealth += 15;
                GameObject.Find("Tier3HP").GetComponentInChildren<Button>().interactable = false;
            }
        }
        MaxHealthInfo.text = "Max Health : " + playerClass.maxHealth;
        return;
    }
    public void upgradeArmour(int requested_tier)
    {
        if (requested_tier == 0)
        {
            playerLevel = 1;
            if (armourTier == 0 && playerLevel == 1)
            {
                armourTier = 1;
                playerClass.armor_class += 2;
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.grey;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.grey;

                GameObject.Find("Tier1AC").GetComponentInChildren<Button>().interactable = false;
                ;
            }
        }
        else if (requested_tier == 1)
        {
            playerLevel = 2;
            if (armourTier == 1 && playerLevel == 2)
            {
                armourTier = 2;
                playerClass.armor_class += 2;
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.green;


                GameObject.Find("Tier2AC").GetComponentInChildren<Button>().interactable = false;

            }
        }
        else if (requested_tier == 2)
        {
            playerLevel++;
            if (armourTier == 2 && playerLevel == 3)
            {
                armourTier = 3;
                playerClass.armor_class += 2;
                GameObject.Find("Character_Knight_01").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("Character_Knight_prev").GetComponent<Renderer>().material.color = Color.yellow;


                GameObject.Find("Tier3AC").GetComponentInChildren<Button>().interactable = false;
            }
        }
        ACInfo.text = "Armour class : " + playerClass.armor_class;
        return;
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
        if (requested_tier == 0)
        {
            playerLevel = 1;

            if (damageTier == 0 && playerLevel == 1)
            {
                damageTier = 1;
                playerClass.damage_upper += 2;
                GameObject.Find("Tier1DMG").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if (requested_tier == 1)
        {
            playerLevel = 2; 
            if (damageTier == 1 && playerLevel == 2)
            {
                damageTier = 2;
                playerClass.damage_upper += 3;
                GameObject.Find("Tier2DMG").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.yellow;

            }
        }
        else if (requested_tier == 2)
        {
            playerLevel = 3;
            if (damageTier == 2 && playerLevel == 3)
            {
                damageTier = 3;
                playerClass.damage_upper += 4;
                GameObject.Find("Tier3DMG").GetComponentInChildren<Button>().interactable = false;
                GameObject.Find("SM_Wep_Broadsword_01").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("SM_Wep_Broadsword_02").GetComponent<Renderer>().material.color = Color.green;
            }
        }
        DamageTierInfo.text = "Damage Tier : " + damageTier + " Upper boundary : " + playerClass.damage_upper;
        return;

    }
    public void upgradeStamina(int requested_tier)
    {
        if (requested_tier == 0)
        {
            playerLevel = 1;
            if (staminaTier == 0 && playerLevel == 1)
            {
                staminaTier = 1;
                playerClass.maxStamina += 2;
                GameObject.Find("Tier1ST").GetComponentInChildren<Button>().interactable = false;
                
            }
        }
        else if (requested_tier == 1)
        {
            playerLevel = 2;
            if (staminaTier == 1 && playerLevel == 2)
            {
                staminaTier = 2;
                playerClass.maxStamina += 3;
                GameObject.Find("Tier2ST").GetComponentInChildren<Button>().interactable = false;

            }
        }
        else if (requested_tier == 2)
        {
            playerLevel++;
            if (staminaTier == 2 && playerLevel == 3)
            {
                staminaTier = 3;
                playerClass.maxStamina += 4;
                GameObject.Find("Tier3ST").GetComponentInChildren<Button>().interactable = false;
            }
        }
        STInfo.text = "Max Stamina : " + playerClass.maxStamina;
        return;
    }

    public void SpinCharacterPreview()
    {


        GameObject.Find("PreviewCharacter").GetComponent<Transform>().rotation = Quaternion.Euler(0, slider.GetComponent<Slider>().value * 360, 0);

    }


}