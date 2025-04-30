
using JetBrains.Annotations;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class Placement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager manager;
    public static Grid Grid;
    private GameObject playerobj;
    private GridTest findPath;

 
    private void Start()
    {
        Grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        findPath = GameObject.FindGameObjectWithTag("Player").GetComponent<GridTest>();
        playerobj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 mousePosition = manager.GetSelectedMapPostion();
        Vector3Int gridPosition = GridManager.grid.WorldToCell(mousePosition);
        Vector3 pointOnGrid = GridManager.grid.CellToWorld(gridPosition);
     
        

        pointOnGrid.y += 0.1f;
        mouseIndicator.transform.position = gridPosition;
        cellIndicator.transform.position = pointOnGrid;


        Vector3Int playerPositionOnGrid = GridManager.grid.WorldToCell(playerobj.transform.position);

      

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            GridCell player = GridManager.gridLayout[playerPositionOnGrid];
            GridCell MouseCell = GridManager.gridLayout[gridPosition];
            Debug.Log(MouseCell.position);

            if (MouseCell.occupied)
            {
                if (MouseCell.occupiedBy != null)
                {
                    playerobj.GetComponent<PlayerClass>().RequestedEnemy = MouseCell.occupiedBy;
                    GameObject.Find("Attack").GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

                } else
                {
                    playerobj.GetComponent<PlayerClass>().RequestedEnemy = null;
                }

            } else
            {
                GameObject.Find("Attack").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            if (!MouseCell.occupied)
            {
                if (!Possession.isPossessed)
                {
                    findPath.findPath(player, MouseCell);
                    if (findPath.FinalPath != null)
                    {
                        Debug.Log("added");
                        if (findPath.FinalPath.Count > 0)
                        {
                            Debug.Log("more than 0");
                        }
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>().ReqPlayerMovement = findPath.FinalPath;
                        EndTurn.playerSelectedPath = true;

                    }
                }
                else
                {
                    findPath.findPath(GridManager.gridLayout[GridManager.grid.WorldToCell(playerobj.GetComponent<PlayerClass>().possessedEnemy.GetComponent<Transform>().position)], MouseCell);
                    if (findPath.FinalPath != null)
                    {
                        Possession.possessionPath.Add(playerobj.GetComponent<PlayerClass>().possessedEnemy, findPath.FinalPath);
                    }
                }
            }
           
        }
    }

}
