using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;
    public AudioClip swordSlash;
    public AudioClip enemySwordSlash;
    public AudioClip arrowFiring;
    public AudioClip unarmedOrcAttack;
    public AudioClip orcDeath;
    public AudioClip orcDrum;
    public AudioClip orcSurprised;
    public AudioClip skeletonDeath;
    public AudioClip skeletonSurprised;
    public AudioClip skeletonWarriorDeath;
    public AudioClip skeletonFiring;
    public AudioClip dwarfSurprised;
    public AudioClip dwarfMeleeAttack;
    public AudioClip bearSurprised;
    public AudioClip bearAttack;
    public AudioClip bearTrap;


    private AudioSource AudioSource;
    
    public void playBearTrap()
    {
        AudioSource.PlayOneShot(bearTrap);
    }
    public void playUnarmedOrcAttack()
    {
        AudioSource.PlayOneShot(unarmedOrcAttack);
    }
    public void playBearSurprised()
    {
        AudioSource.PlayOneShot(bearSurprised);
    }
    public void playBearAttack()
    {
        AudioSource.PlayOneShot(bearAttack);
    }
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
    public void playSkeletonSurprised()
    {
        AudioSource.PlayOneShot(skeletonSurprised);
    }


    public void playSkeletonDeath()
    {
        AudioSource.PlayOneShot(skeletonDeath);
    }
    public void playSkeletonWarriorDeath()
    {
        AudioSource.PlayOneShot(skeletonWarriorDeath);
    }
    public void playSkeletonFiring()
    {
        AudioSource.PlayOneShot(skeletonFiring);
    }
    public void playDwarfSurprised()
    {
        AudioSource.PlayOneShot(dwarfSurprised);
    }
    public void playDwarfMelee()
    {
        AudioSource.PlayOneShot(dwarfMeleeAttack);
    }

    void Start()
    {
    

     AudioSource = GetComponent<AudioSource>();
    }
    public void Awake()

    {
        //if (SceneManager.GetActiveScene().name == "menu")
        //{
        //    Destroy(gameObject);
        //}
        //if (instance != null && this != instance)
        //{
        //    Destroy(gameObject);
        //}

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
