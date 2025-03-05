using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class audio_play : MonoBehaviour
{
    // Start is called before the first frame update
    //AudioSource audio;
    private AudioSource audio;
    public AudioClip footstep;
    public AudioClip drum;
    public Transform camera;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void play_drum()
    {
        audio.clip = drum;
        checkDistance();
        audio.Play();
    }
    public void play_footstep()
    {
        audio.clip = footstep;
        checkDistance();
        audio.Play();
    }
    
    private void checkDistance()
    {
        if (Vector3.Distance(transform.position, camera.transform.position) > 20)
        {
            audio.volume = 0f;
        }
        else
        {
            audio.volume = 0.2f;
        }
    }
    
    public void Update()
    {
       
    }
}
