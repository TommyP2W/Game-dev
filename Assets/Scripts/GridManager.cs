using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static Dictionary<Vector3Int, GridCell> gridLayout; // change this to 2d array tomorrow
    private Vector3Int cellGridPos;
    public static int width = 50, height = 50;
    private GridCell cell;
    [SerializeField] private LayerMask buildingLayer;
    [SerializeField] private LayerMask casteLayer;
    [SerializeField] private LayerMask gateLayer;

    public static Grid grid;

    private void generateGrid()
    {
        // Initialise the dictionary
        // For the width of the grid
        for (int i = 0; i < width; i++)
        {
            // For the height of the grid
            for (int j = 0; j < height; j++)
            {
                // Here i and j represent x and y values, since this is a 1 by 1 grid
                //Vector3 pos = new Vector3Int(i, 0, j);
                GridCell cell = new GridCell();
                cell.position = grid.WorldToCell(new Vector3(i, 0, j));
                cellGridPos = grid.WorldToCell(new Vector3(i, 0, j));  // this will provide cell coords, if wanna check player cell, just convert player pos to cell
                Vector3 positionOnMap = grid.CellToWorld(cellGridPos);

                //Debug.Log(grid.cellSize);
                //Debug.Log(grid.CellToWorld(new Vector3Int(0, 0, 0)));
                //Debug.Log(grid.CellToWorld(new Vector3Int(1, 0, 0)));
                //Debug.Log(grid.CellToWorld(new Vector3Int(0, 0, 1)));
                //Debug.Log(grid.gameObject.name);
                if (Physics.Raycast(positionOnMap + Vector3.up * 10f, Vector3.down, 20f, buildingLayer))
                {
                    cell.walkable = false;
                }
                if (Physics.Raycast(positionOnMap + Vector3.up * 20f, Vector3.down, 20f, casteLayer))
                {
                    cell.walkable = false;
                }
                if (Physics.Raycast(positionOnMap + Vector3.up * 20f, Vector3.down, 20f, gateLayer))
                {
                    cell.walkable = false;
                }
                // So here I am adding the grid cell position, and an abstracted real position of a cell on the grid
                gridLayout.Add(cellGridPos, cell);
            }
        }
      

    }
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        gridLayout = new Dictionary<Vector3Int, GridCell>();
        generateGrid();
        Vector3 posOnMap = grid.CellToWorld(new Vector3Int(0, 0, 9));
      
    }

    // Update is called once per frame
   
}
