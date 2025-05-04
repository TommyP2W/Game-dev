using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BearTrap : MonoBehaviour
{

    //public List<GridCell> neighbours;
    public static bool checkingTraps = false;
    public bool slider = false;
    public bool trapActivated = false;
    public bool directionLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerClass>().currentHealth -= 5;
            //gameObject.SetActive(false);
            PlayerClass.movementBlocked = true;
            other.GetComponent<PlayerClass>().RequestedEnemy = null;
            trapActivated = true;
            slider = true;
            AttackManager.trapSlider.SetActive(true);
        }
    }

    public void blockMovementCheck(float value)
    {
        if (value >= 0.4f &&  value <= 0.6f)
        {
            PlayerClass.movementBlocked = false;
            gameObject.SetActive(false);
            AttackManager.trapSlider.SetActive(false);
        }  else
        {
            AttackManager.trapSlider.SetActive(false);
            slider = false;
        }
    }

    public void checkForPlayer()
    {
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]);
        GridCell currentCell = GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)];
        if (currentCell.occupied)
        {
            if (currentCell.occupiedBy.tag == "Player")
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
        foreach (GridCell cell in neighbours)
        {
            if (cell.occupied)
            {
                if (cell.occupiedBy.tag == "Player")
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }

        checkingTraps = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingTraps)
        {
            Debug.Log("Hello");
            checkForPlayer();

        }
        if (EndTurn.turnEnd)
        {
            //Debug.Log("SADJASJDSAJKDISAKDOIKSAJDASOIUDHISOADPOISADJNASUJDXIOASDAS");
            if (trapActivated)
            {
                slider = true;
                AttackManager.trapSlider.SetActive(true);
            }
        }

        if (slider)
        {
            //sliderUI.GetComponent<Slider>().value += 0.5f * Time.deltaTime;

            if (AttackManager.trapSlider.GetComponent<Slider>().value >= 0.99f)
            {
                directionLeft = true;
            } else if (AttackManager.trapSlider.GetComponent<Slider>().value <= 0.1f)
            {
                directionLeft = false;
            }


            if (!directionLeft)
            {
                AttackManager.trapSlider.GetComponent<Slider>().value += 0.5f * Time.deltaTime;
            } else
            {
                AttackManager.trapSlider.GetComponent<Slider>().value -= 0.5f * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                blockMovementCheck(AttackManager.trapSlider.GetComponent<Slider>().value);
            }
        }

    }
}

