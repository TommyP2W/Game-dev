using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool up;
    public bool down;
    public GameObject leverText;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        leverText.SetActive(false);

    }
    //private void Awake()
    //{
    //    leverText = GameObject.Find("LeverText");
    //    leverText.SetActive(false);
    //}

    public void pullUp()
    {
        animator.SetBool("pullDown", true);
        animator.SetBool("pullUp", true);

    }
    public void pullDown()
    {
        animator.SetBool("pullUp", false);
        animator.SetBool("pullDown", true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            leverText.SetActive(true);

            if (leverText != null)
            {
                leverText.transform.position = gameObject.transform.position;
                leverText.transform.rotation = leverText.transform.rotation;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        leverText.SetActive(false);
    }

    public void checkCombination()
    {
        int up = 0;
        int down = 0;
        foreach (GameObject Lever in GameObject.FindGameObjectsWithTag("Lever"))
        {
           if (gameObject.GetComponent<LeverSwitch>().up)
            {
                up++;
            }
           else if (gameObject.GetComponent<LeverSwitch>().down)
            {
                down++;
            }
        }

        if (up == 3 && down == 1)
        {
            foreach (GameObject GATEBLOCKER in GameObject.FindGameObjectsWithTag("gate"))
            {
                GATEBLOCKER.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
    }
    void Update()
    {
        //if (leverText != null)
        //{
        //    leverText.SetActive(false);

        //} else
        //{
        //    Debug.Log("null text");
        //}
    }
}
