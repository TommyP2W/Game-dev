using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.VFX;

public class SkeletonMage : MonoBehaviour
{
    [SerializeField] private GameObject SkeletonPrefab;
    private GridTest gridTest;
    // Start is called before the first frame update
    void Start()
    {
        //gridTest = gameObject.AddComponent<GridTest>();
    }


    public void SpawnSkeletons()
    {
        //Vector3Int currentPosition = GridManager.grid.WorldToCell(transform.position);
        List<GridCell> neighbours = GridTest.getNeighbours(GridManager.gridLayout[(GridManager.grid.WorldToCell(transform.position))]);



        StartCoroutine(spawnSkeletons(neighbours));
    }

    public void MistEffect()
    {
        VisualEffect effect = gameObject.GetComponent<VisualEffect>();
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
                neighbours.Remove(neighbours[0]);
            }
            yield return null;
        }
        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        SpawnSkeletons();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
