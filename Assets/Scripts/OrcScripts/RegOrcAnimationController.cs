using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RegOrcAnimationController : MonoBehaviour
{

    // Start is called before the first frame update
    public Animator anim;
    private GameObject player;
    private PostProcessVolume postProcessing;
    private Vignette vin;
    public LayerMask playerLayer;
    float fadeSpeed = 0.1f;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        postProcessing = GameObject.FindGameObjectWithTag("postProcessing").GetComponent<PostProcessVolume>();
        vin = new Vignette();
        
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
            if (other.tag == "Player")
            {
               
          
                SoundManager.instance.playOrcSurprised();
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
                if (gameObject.GetComponent<Characters>().attackAction == false)
                {
                    if (gameObject.name == "OrcShaman")
                    {
                        gameObject.GetComponent<OrcShaman>().healAction = true;
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
                                Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);
                                gameObject.GetComponent<OrcArcher>().attackAction = true;
                            }
                        }

                    }
                }
            }
        }
        else
        {

            anim.SetBool("isAttacking", false);

            if (EndTurn.turnEnd)
            {
                if (Physics.Raycast(gameObject.transform.position, (player.transform.position - gameObject.transform.position).normalized, out RaycastHit hit, 20f))
                {
                    if (hit.collider.gameObject.layer == 14)
                    {

                        Debug.Log("orc mode activated");
                        Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);
                        gameObject.GetComponent<Characters>().chasing = true;
                    }

                }
            }
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
        } else
        {
            anim.SetBool("isWalking", false);
        }
    }
}


