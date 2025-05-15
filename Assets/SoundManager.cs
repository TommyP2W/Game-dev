using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;
    public AudioClip swordSlash;
    public AudioClip enemySwordSlash;
    public AudioClip arrowFiring;
    public AudioClip orcDeath;
    public AudioClip orcDrum;
    public AudioClip orcSurprised;
    public AudioClip skeletonDeath;
    public AudioClip skeletonWarriorDeath;


    private AudioSource AudioSource;
    public void playSwordSlash()
    {
        AudioSource.PlayOneShot(swordSlash);
    }
    public void playEnemySwordSlash()
    {
        AudioSource.PlayOneShot(enemySwordSlash);
    }
    public void playEnemyArrowFire()
    {
        AudioSource.PlayOneShot(arrowFiring);
    }
    public void playOrcDeath()
    {
        AudioSource.PlayOneShot(orcDeath);
    }
    public void playOrcDrum()
    {
        AudioSource.PlayOneShot(orcDrum);
    }
    public void playOrcSurprised()
    {
        AudioSource.PlayOneShot(orcSurprised);
    }
    public void playSkeletonDeath()
    {
        AudioSource.PlayOneShot(skeletonDeath);
    }
    public void playSkeletonWarriorDeath()
    {
        AudioSource.PlayOneShot(skeletonWarriorDeath);
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
