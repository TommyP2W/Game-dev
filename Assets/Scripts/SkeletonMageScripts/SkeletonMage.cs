using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class SkeletonMage : MonoBehaviour, Characters
{


    public int currentHealth { get; set; }
    public int maxHealth { get; set; } = 15;
    public bool chasing { get; set; }
    public bool isWalking { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void SpawnSkeletons()
    {
        //Vector3Int currentPosition = GridManager.grid.WorldToCell(transform.position);
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[(GridManager.grid.WorldToCell(transform.position))]);


        StartCoroutine(spawnSkeletons(neighbours));
    }
    public void playerSmall()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }


    public IEnumerator spawnSkeletons(List<GridCell> neighbours)
    {
        while (neighbours.Count > 0)
        {
            GameObject Skeleton = ObjectPool.SharedInstance.GetPooledObject();
            if (Skeleton != null)
            {
                Skeleton.transform.position = neighbours[0].position;
                Skeleton.SetActive(true);
                Skeleton.GetComponent<Characters>().chasing = true;
                Debug.Log(Skeleton.GetComponent<Characters>().chasing);
                neighbours.Remove(neighbours[0]);
            }
            yield return null;
        }
        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        SpawnSkeletons();
        playerSmall();
    }
    // Update is called once per frame


    public void attack()
    {
        SpawnSkeletons();
    }

    public void death()
    {
        gameObject.SetActive(false);
    }


}
