
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Drummer_animation_controller : MonoBehaviour
{
    // Start is called before the first frame update
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
    public void OnTriggerEnter(Collider other)
    {
        if (!EndTurn.turnEnd && other.gameObject.tag == "Player")
        {
            anim.SetBool("Drum_playing", true);
            gameObject.GetComponent<OrcDrummer>().playDrumAction = true;
            anim.SetBool("isAttacking", false);
        }
    }
    public void OnTriggerExit(Collider other)
    {
      
            anim.SetBool("Drum_playing", false);
            anim.SetBool("isAttacking", false);
            gameObject.GetComponent<OrcDrummer>().playDrumAction = false;
            gameObject.GetComponent<Characters>().attackAction = false;


    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!EndTurn.turnEnd)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 2f)
                {
                    gameObject.GetComponent<OrcDrummer>().playDrumAction = false;
                    gameObject.GetComponent<Characters>().attackAction = true;
                }
                else
                {
                    gameObject.GetComponent<Characters>().attackAction = false;

                    gameObject.GetComponent<OrcDrummer>().playDrumAction = true;
                    anim.SetBool("Drum_playing", true);

                }
            }
        }
    }
    public void endDrumming()
    {
        anim.SetBool("Drum_playing", false);
    }
    public void endAttack()
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








