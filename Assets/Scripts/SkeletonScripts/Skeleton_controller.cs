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

    }
    public void OnTriggerExit(Collider other)

    {
        if (!EndTurn.turnEnd)
        {
            anim.SetBool("isAttacking", false);
        }
    }
    public void OnTriggerEnter(Collider other)

    {
        if (!EndTurn.turnEnd)
        {
            gameObject.GetComponent<Characters>().chasing = true;
        }
    }


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!EndTurn.turnEnd && EndTurn.CoroutinesActive == 0)
        {
            foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(gameObject.transform.position)]))
            {
                if (cell.occupiedBy == player)
                {

                    gameObject.GetComponent<Characters>().attackAction = true;
                }
            }
        }
        else
        {
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
