
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GridTest : MonoBehaviour
{
    public List<GridCell> FinalPath;



    void Start()
    {
        FinalPath = new List<GridCell>();

    }
    //atten distance of neighbours
    int calcShortestPath(Vector3Int firstPos, Vector3Int secondPos)


    {

        int Manhatten = (Mathf.Abs(firstPos.x - secondPos.x) + Mathf.Abs(firstPos.z - secondPos.z));
        return Manhatten;
    }

    public static List<GridCell> getNeighbours(GridCell Cell)
    {
        //Debug.Log("CELL POSITION" + Cell.position);
        List<GridCell> neighbours = new List<GridCell>();


        for (int x = -1; x <= 1; x++)
        {
            // Ignoring the player's current position
            if (x == 0) continue;
            int checkX = Cell.position.x + x;
            if (checkX >= 0 && checkX < GridManager.height)
            {
                neighbours.Add(GridManager.gridLayout[new Vector3Int(checkX, 0, Cell.position.z)]);
            }
        }
        for (int z = -1; z <= 1; z++)
        {
            // Ignoring player's current cell
            if (z == 0) continue;
            int checkZ = Cell.position.z + z;
            // If its inside of the grid
            if (checkZ >= 0 && checkZ < GridManager.width)
            {
                neighbours.Add(GridManager.gridLayout[new Vector3Int(Cell.position.x, 0, checkZ)]);
            }
        }


        return neighbours;
    }

    public void findPath(GridCell startPos, GridCell endPos)
    {

        if (!EndTurn.turnEnd)
        {
            // Revert back to lists if necessary
            List<GridCell> openList = new List<GridCell>();
            List<GridCell> closedList = new List<GridCell>();
            //  GridCell startingCell = gridLayout[startPos];
            openList.Add(startPos);

            //Debug.Log("helo");

            // openList.count
            while (openList.Count > 0)
            {

                GridCell cellToSearch = openList[0];
                //cellToSearch.fCost = calcShortestPath(cellToSearch.position, endPos);
                //cellToSearch.hCost = calcShortestPath(cellToSearch.position, endPos);




                for (int i = 1; i < openList.Count; i++)
                {
                    {
                        //print(i);
                        GridCell cells = openList[i];
                        //Debug.Log(cells.fCost + " " + cells.hCost);

                        if (cells.fCost < cellToSearch.fCost || cells.fCost == cellToSearch.fCost && cells.hCost == cellToSearch.hCost)
                        {

                            cellToSearch = cells;
                        }

                    }
                }
                if ((cellToSearch.position == endPos.position))
                {
                    constructPath(startPos, cellToSearch);
                    return;

                }
                openList.Remove(cellToSearch);
                closedList.Add(cellToSearch);

                foreach (GridCell neighbour in getNeighbours(cellToSearch))
                {
                    if (closedList.Contains(neighbour) || !neighbour.walkable)
                    {
                        //Debug.Log(closedList.Contains(neighbour));
                        //Debug.Log(!neighbour.walkable);
                        //Debug.Log("neighbour occupied " + neighbour.occupied);
                        continue;
                    }
                    int newMovementToNeighbour = cellToSearch.gCost + calcShortestPath(cellToSearch.position, neighbour.position);
                    // This works correctly according to manhatten distance so thats good. Check up with this tomorrow!
                    //Debug.Log(newMovementToNeighbour);
                    if (newMovementToNeighbour < neighbour.gCost || !openList.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementToNeighbour;
                        neighbour.hCost = calcShortestPath(neighbour.position, endPos.position);
                        neighbour.parent = cellToSearch;

                        if (!openList.Contains(neighbour) && neighbour.walkable && !neighbour.occupied)
                        { 
                     
                            openList.Add(neighbour);
                        }
                    }

                }
              
            }
        }

    }



    void constructPath(GridCell start, GridCell end)
    {
        //List<GridCell> cells = new List<GridCell>();
        FinalPath = new List<GridCell>();
        GridCell currentNode = end;
        while (currentNode != start)
        {
            //Debug.Log("jidasj");
            FinalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        FinalPath.Add(start);
        FinalPath.Reverse();    
        //drawPath(FinalPath);
        return;
    }

    public static void drawPath(List<GridCell> path, LineRenderer lineRenderer)
    {
        //GameObject lineRendererObject = GameObject.FindGameObjectWithTag("Line renderer");
        //LineRenderer lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i].position + Vector3.up * 0.1f);
        }
    }
}

