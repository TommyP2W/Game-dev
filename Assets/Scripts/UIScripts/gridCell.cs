

using UnityEngine;

public class GridCell
{
    public Vector3Int position;
    public int gCost = 0;
    public int hCost = 0;
    public bool walkable = true;
    public GridCell parent;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}