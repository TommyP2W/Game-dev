using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class Skeleton_controller : MonoBehaviour
{
    public Animator anim;
    private GameObject player;
    private PostProcessVolume postProcessing;
    private Vignette vin;
    float fadeSpeed = 0.1f;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        postProcessing = GameObject.FindGameObjectWithTag("postProcessing").GetComponent<PostProcessVolume>();

        if (anim == null)
        {
            Debug.Log("ANIM NULL");
        }
        else
        {
            Debug.Log("ANIM NOT NULL");
        }

    }
    public void OnTriggerExit(Collider other)

    {
        if (other.tag == "Player")
        {
            Debug.Log("sdadajsdasdasdsad");
            gameObject.GetComponent<Characters>().attackAction = false;
            gameObject.GetComponent<Characters>().chasing = false;

            anim.SetBool("isAttacking", false);

        }
    }
    public void OnTriggerEnter(Collider other)

    {
        
            if (other.tag == "Player")
            {

                gameObject.GetComponent<Characters>().chasing = true;
            }
        
    }


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (EndTurn.CoroutinesActive == 0)
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
            }

        }
        else
        {
            gameObject.GetComponent<Characters>().attackAction = false;

            anim.SetBool("isAttacking", false);

        }
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
    public void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
