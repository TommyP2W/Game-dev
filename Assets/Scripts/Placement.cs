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
        //if (!GridTest.gridLayout.ContainsValue(new Vector3Int(gridPosition.x, gridPosition.y ,gridPosition.z))) {
        //    //Debug.Log(pointOnGrid);  
        //}
        

        pointOnGrid.y += 0.1f;
        mouseIndicator.transform.position = gridPosition;
        cellIndicator.transform.position = pointOnGrid;


        Vector3Int playerPositionOnGrid = Grid.WorldToCell(playerobj.transform.position);

        //if (gridPosition.x - playerPositionOnGrid.x > 7)
        //{
        //    int difference = gridPosition.x - playerPositionOnGrid.x + 7;
        //    cellIndicator.x -= difference;
        //}
        //else if (gridPosition.x - playerPositionOnGrid.x < -7)
        //{
        //    int difference = gridPosition.x + 7;
        //    cellIndicator.transform.position.x += (difference - -7);
        //}
        //Debug.Log(cellIndicator.transform.position);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            GridCell player = new GridCell();
            GridCell MouseCell = new GridCell();
            


            player.position = playerPositionOnGrid;
            MouseCell.position = gridPosition;
            findPath.findPath(player, MouseCell);
            //Debug.Log("Hello");
            float offset = 0.5f;
            Vector3 vector3 = (Grid.CellToWorld(gridPosition));
            vector3.x += offset;
            vector3.z += offset;
            EndTurn.playerRequestedMove = vector3;
            
        }
    }

}
