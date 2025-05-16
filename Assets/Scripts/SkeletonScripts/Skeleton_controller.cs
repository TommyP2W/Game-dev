
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Skeleton_controller : MonoBehaviour
{

    // Start is called before the first frame update
    public Animator anim;
    private GameObject player;
    private PostProcessVolume postProcessing;
    public LayerMask playerLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        postProcessing = GameObject.FindGameObjectWithTag("postProcessing").GetComponent<PostProcessVolume>();

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
            SoundManager.instance.playSkeletonSurprised();
            gameObject.GetComponent<Characters>().chasing = true;
        }
    }


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (EndTurn.CoroutinesActive == 0 && !EndTurn.turnEnd)
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
              
                if (gameObject.GetComponent<skeleton_CrossBow>() != null)
                {
                    if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 10f)
                    {
                        //Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);
                        int layerMask = 1 << LayerMask.NameToLayer("Player");
                        if (Physics.Raycast(gameObject.transform.position, (player.transform.position - gameObject.transform.position).normalized, out RaycastHit hit, 20f, layerMask))
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
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}




