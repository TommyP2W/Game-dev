using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class Placement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager manager;
    public static Grid Grid;
    private NavMeshAgent playerobj;
    private GridTest findPath;

 
    private void Start()
    {
        Grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        findPath = GameObject.FindGameObjectWithTag("Player").GetComponent<GridTest>();
       playerobj = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 mousePosition = manager.GetSelectedMapPostion();
        Vector3Int gridPosition = Grid.WorldToCell(mousePosition);
        Vector3 pointOnGrid = Grid.CellToWorld(gridPosition);
     
        

        pointOnGrid.y += 0.1f;
        mouseIndicator.transform.position = gridPosition;
        cellIndicator.transform.position = pointOnGrid;


        Vector3Int playerPositionOnGrid = Grid.WorldToCell(playerobj.transform.position);

      

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            GridCell player = new GridCell();
            GridCell MouseCell = new GridCell();
            player.position = playerPositionOnGrid;
            MouseCell.position = gridPosition;
            findPath.findPath(player, MouseCell);
          
           
        }
    }

}
