using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIanager : MonoBehaviour
{
    // Start is called before the first frame update
    Transform maincam;
    Transform unit;
    Transform worldSpace;
    public static UIanager ts;
    public bool showing = false;
    public TextMeshProUGUI textMeshProUGUIelement;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public void Awake()
    {
        textMeshProUGUIelement = GetComponent<TextMeshProUGUI>();
        maincam = GameObject.FindGameObjectWithTag("CameraPivot").transform;
        unit = GameObject.FindGameObjectWithTag("Player").transform;
        worldSpace = GameObject.Find("InfoCanvas").transform;
        transform.SetParent(worldSpace);
    }

  
    public void show(GameObject origin,GameObject entity, string option, int amount = 0)
    {
        gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        gameObject.GetComponent<TextMeshProUGUI>().alpha = 1;
        transform.rotation = Quaternion.LookRotation(transform.position - maincam.position);
        transform.position = entity.transform.position + Offset;
        if (option.Equals("Attack")) {
            if (amount == 0)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "Missed!";
            } else
            {
                if (origin.GetComponent<OrcArcher>() != null)
                {
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Struck with piercing arrow of " + amount + " damage!";
                } else
                {
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Struck with piercing strike of " + amount + " damage!";

                }
            }
        } else if (option.Equals("Heal"))
        {
             gameObject.GetComponent<TextMeshProUGUI>().text = "A devilish prescence heals " + entity.name + " for " + amount + " HP !" ;
        } else if (option.Equals("Summon")){
            gameObject.GetComponent<TextMeshProUGUI>().text = "Darkness summons upon thee!";
        } else if (option.Equals("Drumming"))
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "A thunderous roar fills the space among us.";
        } else if (option.Equals("FortuneWell"))
        {
            if (amount == 0)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "Too bad! (-4 HP)";
            } else
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gained 4 XP!";
            }
        }

        showing = true;
    }

//    public void Ie

    public void Update()
    {
        if (showing)
        {
            if (gameObject.GetComponent<TextMeshProUGUI>().enabled)
            {
                transform.Translate(Vector3.up * 2f * Time.deltaTime);
                gameObject.GetComponent<TextMeshProUGUI>().alpha -= Time.deltaTime / 2;

                if (gameObject.GetComponent<TextMeshProUGUI>().alpha <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
