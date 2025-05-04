using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform maincam;
    Transform unit;
    Transform worldSpace;
    public static UIanager ts;
    public bool showing = false;
    public TextMeshProUGUI textMeshProUGUIelement;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public void Start()
    {
        ts = this;
        textMeshProUGUIelement = GetComponent<TextMeshProUGUI>();
       // maincam = Camera.main.transform;
        unit = GameObject.FindGameObjectWithTag("Player").transform;
        worldSpace = GameObject.Find("InfoCanvas").transform;

        transform.SetParent(worldSpace);
        textMeshProUGUIelement.enabled = false;

    }

    public static void showText(GameObject entity)
    {
        ts.show();
    }
    public void show()
    {
        textMeshProUGUIelement.enabled = true;


        transform.rotation = Quaternion.LookRotation(transform.position - maincam.position);
        transform.position = unit.position + Offset;
        showing = true;
    }

//    public void Ie

    public void Update()
    {
        if (showing)
        {
            if (textMeshProUGUIelement.enabled)
            {
                transform.Translate(Vector3.up * 2f * Time.deltaTime);
                gameObject.GetComponent<TextMeshProUGUI>().alpha -= Time.deltaTime / 2;

                if (gameObject.GetComponent<TextMeshProUGUI>().alpha <= 0)
                {
                    textMeshProUGUIelement.enabled = false;
                    showing = false;
                }
            }
        }
    }
}
