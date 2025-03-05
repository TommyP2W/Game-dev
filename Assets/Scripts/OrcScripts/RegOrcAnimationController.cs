using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RegOrcAnimationController : MonoBehaviour
{

    // Start is called before the first frame update
    private Animator anim;
    private GameObject player;
    private PostProcessVolume postProcessing;
    private Vignette vin;
    private RandMov randMov;
    float fadeSpeed = 0.1f;
    void Start()
    {
        randMov = GetComponent<RandMov>();
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
    


// Update is called once per frame
    private void OnTriggerStay(Collider other)
        {
        if (!EndTurn.turnEnd && EndTurn.CoroutinesActive == 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {

                anim.SetBool("isAttacking", true);
                if (postProcessing.profile.TryGetSettings(out vin) && vin.intensity.value < 0.485f)
                {
                    vin.intensity.value = Mathf.Clamp(vin.intensity.value + (fadeSpeed * Time.deltaTime), 0.0f, 0.485f);
                }

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

    public void Update()
    {
        if (randMov.isWalking)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        } else
        {
            anim.SetBool("isWalking", false);
        }
    }
}


