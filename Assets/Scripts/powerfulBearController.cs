
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class powerfulBearController : MonoBehaviour
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
            SoundManager.instance.playBearSurprised();
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
            else
            {

                anim.SetBool("isAttacking", false);

                if (EndTurn.turnEnd)
                {
                    if (Physics.Raycast(gameObject.transform.position, (player.transform.position - gameObject.transform.position).normalized, out RaycastHit hit, 20f))
                    {
                        if (hit.collider.gameObject.layer == 14)
                        {
                            Debug.DrawRay(gameObject.transform.position, player.transform.position - gameObject.transform.position, Color.red);
                            gameObject.GetComponent<Characters>().chasing = true;
                        }

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


