using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class dwarfController : MonoBehaviour
{

    // Start is called before the first frame update
    public Animator anim;
    private GameObject player;
    public LayerMask playerLayer;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
       

    }
    public void OnTriggerExit(Collider other)

    {

        if (other.tag == "Player")
        {
            gameObject.GetComponent<Characters>().attackAction = false;
            gameObject.GetComponent<Characters>().chasing = false;

            anim.SetBool("isAttacking", false);

        }

    }
    public void OnTriggerEnter(Collider other)

    {
        if (!EndTurn.turnEnd)
        {
            if (other.tag == "Player")
            {

                gameObject.GetComponent<Characters>().chasing = true;
            }
        }
    }



    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!EndTurn.turnEnd && EndTurn.CoroutinesActive == 0)
        {
            if (other.tag == "Player")
            {

                foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]))
                {
                    if (cell.occupiedBy == player)
                    {

                        gameObject.GetComponent<Characters>().attackAction = true;
                    }

                }
              
                if (gameObject.GetComponent<OrcArcher>() != null)
                {
                    if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 5f)
                    {
                        // Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);

                        if (Physics.Raycast(gameObject.transform.position, (player.transform.position - gameObject.transform.position).normalized, out RaycastHit hit, 20f))
                        {

                            if (hit.collider.gameObject.layer == 14)
                            {
                                Debug.Log("Hit" + hit.collider.name + hit.collider.gameObject.layer);
                                Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);
                                gameObject.GetComponent<OrcArcher>().attackAction = true;
                            }
                            else
                            {
                                Debug.Log("HitSOmethingelse");
                            }
                        }

                    }
                }
            }
        }
        else
        {

            anim.SetBool("isAttacking", false);

        }
    }


    public void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void Update()
    {
        if (gameObject.GetComponent<Characters>().isWalking)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}


