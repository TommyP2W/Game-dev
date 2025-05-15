using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class Sanity : MonoBehaviour
{
    // Start is called before the first frame update
    public static int risk = 0;
    public static bool checkingSanity = false;
    public Volume vim;
    public FilmGrain grain;

    private void Start()
    {
        vim = GameObject.Find("pp").GetComponent<Volume>();
    }

    public void checkSanity(int sanity)
    {
    
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().currentHealth <= 75)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().current_sanity -= UnityEngine.Random.Range(0, 5);
        }
        if (sanity >= 75 && sanity < 100)
        {
            risk = 1;
            if (vim.profile.TryGet<FilmGrain>(out grain))
            {
                grain.intensity.Override(0.25f);               
            }
          
            
        } else if (sanity >= 50 && sanity < 75)
        {
            risk = 2;
            if (vim.profile.TryGet<FilmGrain>(out grain))
            {
                grain.intensity.Override(0.50f);
            }

        }
        else if (sanity < 50 )
        {
            risk = 3;
            if (vim.profile.TryGet<FilmGrain>(out grain))
            {
                grain.intensity.Override(0.75f);
            }

        }
        if (risk > 0)
        {
            sanity_consequence();
        }
        checkingSanity = false;
    }
    public void sanity_consequence()
    {
        if (Random.Range(risk, 15) == risk)
        {
            List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[(GridManager.grid.WorldToCell(transform.position))]);
            StartCoroutine(spawnGhosts(neighbours));
        }

    }



    public IEnumerator spawnGhosts(List<GridCell> neighbours)
    {
        while (neighbours.Count > 0)
        {
            GameObject ghosts = ObjectPool.SharedInstance.GetPooledObject("ghosts");
            if (ghosts != null)
            {
                ghosts.transform.position = neighbours[0].position;
                ghosts.SetActive(true);
                ghosts.GetComponent<Characters>().chasing = true;
                Debug.Log(ghosts.GetComponent<Characters>().chasing);
                neighbours.Remove(neighbours[0]);
            }
            yield return new WaitForSeconds(2);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
       if (checkingSanity)
        {
            checkSanity(transform.GetComponent<PlayerClass>().current_sanity);
            Debug.Log(transform.GetComponent<PlayerClass>().current_sanity);
        }
    }
}
