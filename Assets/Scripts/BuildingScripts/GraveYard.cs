using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveYard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[(GridManager.grid.WorldToCell(transform.position))]);


            StartCoroutine(spawnGraveSkeletons(neighbours));
        }
    }
    public IEnumerator spawnGraveSkeletons(List<GridCell> neighbours)
    {

        while (neighbours.Count > 0)
        {
            GameObject Skeleton = ObjectPool.SharedInstance.GetPooledObject("grave");
            if (Skeleton != null)
            {
                Skeleton.transform.position = neighbours[0].position;
                Skeleton.SetActive(true);
                Skeleton.GetComponent<Characters>().chasing = true;
                Debug.Log(Skeleton.GetComponent<Characters>().chasing);
                List<GridCell> new_neighbours = GridTest.getNeighbours(neighbours[0]);
                for (int i = 0; i < new_neighbours.Count; i++)
                {
                    neighbours.Add(new_neighbours[i]);
                }
                neighbours.Remove(neighbours[0]);
            } else
            {
                for (int i = 0; i < neighbours.Count; i++)
                {
                    neighbours.RemoveAt(i);
                }
                yield return null ;
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
