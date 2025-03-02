
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Drummer_animation_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private GameObject player;
    private PostProcessVolume postProcessing;
    private Vignette vin;
    float fadeSpeed = 0.1f;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        postProcessing = GameObject.FindGameObjectWithTag("postProcessing").GetComponent<PostProcessVolume>();
        anim.SetBool("isWalking", true);
    }
    public void OnTriggerEnter(Collider other)
    {
        anim.SetBool("Drum_playing", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        //print("should be working");
    }
    public void OnTriggerExit(Collider other)
    {
        anim.SetBool("Drum_playing", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", true);
     
    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {

            anim.SetBool("Drum_playing", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            if (postProcessing.profile.TryGetSettings(out vin) && vin.intensity.value < 0.485f)
            {
                vin.intensity.value = Mathf.Clamp(vin.intensity.value + (fadeSpeed * Time.deltaTime), 0.0f, 0.485f);
            }

        }
        else
        {
            anim.SetBool("Drum_playing", true);
            anim.SetBool("isAttacking", false);
            Debug.Log("this is an issue");
            if (postProcessing.profile.TryGetSettings(out vin) && vin.intensity.value > 0f)
            {
                //Debug.Log("we have got here");
                vin.intensity.value = Mathf.Clamp(vin.intensity.value - (fadeSpeed * Time.deltaTime), 0.0f, 5f);
            }
        }
    }

    void Update()
    {
        //if (postProcessing.profile.TryGetSettings(out vin))

        //{
        //    if (attacking && vin.intensity.value < 0.485f)
        //    {
        //        vin.intensity.value += Time.deltaTime * fadeSpeed;
        //    }
        //    Debug.Log(attacking);
        //    //Debug.Log("hellosasdads");
        //    vin.intensity.value = Mathf.Clamp(vin.intensity.value - (fadeSpeed * Time.deltaTime), 0.0f, 5f);
               
                
            

        //}
       
    }
}
        
    



