

using UnityEngine;

public class GridCell
{
    public Vector3Int position;
    public int gCost = 0;
    public int hCost = 0;
    public bool walkable = true;
    public bool occupied = false;
    public GameObject occupiedBy;
    public GridCell parent;
    public int HeapIndex { get; set; }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    // Get rid of this if the heap does not work
    public int compareTo(GridCell other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}