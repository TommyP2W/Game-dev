using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject Text;
    static public AttackManager instance;
    public static GameObject trapSlider;

    void Start()
    {
        trapSlider = GameObject.Find("TrapSlider");
        trapSlider.SetActive(false);
        Text = GameObject.Find("DamageText");
        Debug.Log("text" + Text);
        if (Text != null)
        {
            Text.SetActive(false);
            Debug.Log("Hello");
        }
        instance = this;
    }

    public static void showAttackInfo(GameObject Target, int damage)
    {
        Text.SetActive(true);

        
        Text.GetComponent<TextMeshProUGUI>().text = "Damage Dealt: " + damage.ToString();
        TextMeshProUGUI text = Text.GetComponent<TextMeshProUGUI>();
        instance.StartCoroutine(FadeText(4));

    }


    public static IEnumerator FadeText(float t)
    {
        TextMeshProUGUI i = GameObject.Find("DamageText").GetComponent<TextMeshProUGUI>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        GameObject.Find("DamageText").SetActive(false);
    } 
        
}
