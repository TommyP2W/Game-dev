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
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {

                gameObject.GetComponent<Characters>().attackAction = true;

            }
            else
            {
                anim.SetBool("isAttacking", false);
                if (postProcessing.profile.TryGetSettings(out vin) && vin.intensity.value > 0f)
                {
                    //Debug.Log("we have got here");
                    vin.intensity.value = Mathf.Clamp(vin.intensity.value - (fadeSpeed * Time.deltaTime), 0.0f, 5f);
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


