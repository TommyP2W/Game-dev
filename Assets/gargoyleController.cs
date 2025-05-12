using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gargoyleController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void OnTriggerEnter(Collider other)

    {
     
            if (other.tag == "Player")
            {
                gameObject.tag = "Enemy";
                gameObject.GetComponent<Characters>().chasing = true;
                if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 2f) 
                {
                    anim.SetBool("isFlying", true);
                    gameObject.GetComponent<GargoyleClass>().isFlying = true;

                }
            }
        
    }


    public void OnTriggerExit(Collider other)

    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<Characters>().attackAction = false;
            gameObject.GetComponent<Characters>().chasing = false;

            anim.SetBool("isAttacking", false);
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 2f)
            {
                anim.SetBool("isFlying", true);
                gameObject.GetComponent<GargoyleClass>().isFlying = true;

            }
            else
            {
                anim.SetBool("isFlying", false);
                gameObject.GetComponent<GargoyleClass>().isFlying = false;

            }
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (!EndTurn.turnEnd && EndTurn.CoroutinesActive == 0)
        {
            if (other.tag == "Player")
            {

                if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 2f)
                {
                    anim.SetBool("isFlying", false);
                    gameObject.GetComponent<GargoyleClass>().isFlying = false;
                }

                foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]))
                {
                    if (cell.occupiedBy == player)
                    {
                        gameObject.GetComponent<Characters>().isWalking = false;
                        gameObject.GetComponent<GargoyleClass>().isFlying = false;

                        gameObject.GetComponent<Characters>().attackAction = true;
                    }

                }
            }
            else
            {

                anim.SetBool("isAttacking", false);

            }

        }
    }
      

        // Update is called once per frame
        void Update()
        {
            if (gameObject.GetComponent<GargoyleClass>().isFlying)
        {
            anim.SetBool("isFlying", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);

        } else if (gameObject.GetComponent<GargoyleClass>().isWalking)
        {
            anim.SetBool("isFlying", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        } else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
