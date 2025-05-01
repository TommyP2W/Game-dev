using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

   // public List<GridCell> neighbours;
    public static bool checkingTraps = false;
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
            gameObject.SetActive(false);
        }
    }

    public void checkForPlayer()
    {
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]);
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
            checkForPlayer();

        }
    }
}

