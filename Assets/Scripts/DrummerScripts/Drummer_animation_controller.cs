
using Unity.VisualScripting;
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
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!EndTurn.turnEnd)
        {
            anim.SetBool("Drum_playing", true);
            anim.SetBool("isAttacking", false);
        }
    }
    public void OnTriggerExit(Collider other)
    {
      
            anim.SetBool("Drum_playing", false);
            anim.SetBool("isAttacking", false);
        
    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!EndTurn.turnEnd)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {

                anim.SetBool("Drum_playing", false);
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

                GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Enemy");
                if (gameobjects == null)
                {
                   // Debug.Log("null");
                }
                if (gameobjects != null)
                {
                    foreach (GameObject enemy in gameobjects)
                    {
                        if (Vector3.Distance(transform.position, enemy.transform.position) < 1000f)
                        {
                            if (enemy == null)
                            {
                                Debug.Log("null");
                            }
                            enemy.GetComponent<Characters>().chasing = true;
                          
                        }
                    }
                }
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








