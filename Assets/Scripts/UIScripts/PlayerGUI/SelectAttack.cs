using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public static Grid Grid;
    private GridCell cell;
    private GameObject[] cameras;
    private GameObject[] textures;
    [SerializeField]
    private LayerMask enemy;


    public void selectTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3Int playerPositionOnGrid = Grid.WorldToCell(player.transform.position);
        cell.position = playerPositionOnGrid;
        List<GridCell> neighbours = GridTest.getNeighbours(cell);
        Debug.Log("hello");
        Debug.Log(cameras[0].name);
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].occupied)
            {
                cameras[i].GetComponent<Camera>().enabled = true;
                cameras[i].transform.position = neighbours[i].position + Vector3.up * 1;
                textures[i].GetComponent<RawImage>().enabled = true;
            } else 
            {
                cameras[i].GetComponent<Camera>().enabled = false;
                textures[i].GetComponent<RawImage>().enabled = false;

            }

        }
    }


    void Awake()
    {
         Grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
         cameras = GameObject.FindGameObjectsWithTag("UI_cameras");
         textures = GameObject.FindGameObjectsWithTag("rendered_textures");
         cell = new GridCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
