using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool turnEnd = false;
    public static bool isMovementBlocked = true;
    public static Vector3 playerRequestedMove;
    private NavMeshAgent playerNav;
    private GameObject playerPosition;
    private LineRenderer lineRenderer;
    private GridTest gridTest;


    public void Start()
    {
        lineRenderer = GameObject.FindGameObjectWithTag("Line renderer").GetComponent<LineRenderer>();
        gridTest = GameObject.FindGameObjectWithTag("Player").GetComponent<GridTest>();
    }
    public void endTurn()
    {
        turnEnd = true;
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        //playerNav = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        //playerNav.SetDestination(playerRequestedMove);

        Debug.Log("Ended turn");

        if (gridTest.FinalPath.Count > 1)
        {
            StartCoroutine("MovePlayer", gridTest.FinalPath);
        }
    }

    IEnumerator MovePlayer(List<GridCell> FinalPath)
    {
        //Debug.Log(Placement.Grid.WorldToCell(playerPosition.transform.position));
        while (FinalPath.Count > 0)
        {
            GridCell currentNode = FinalPath[0];
            Vector3Int playerGridPosition = Placement.Grid.WorldToCell(playerPosition.transform.position);
            Vector3 nodeToWorld = Placement.Grid.CellToWorld(currentNode.position);
            if (playerPosition.transform.position.x - currentNode.position.x == 0)
            {
                if (playerPosition.transform.position.z - currentNode.position.z < 0)
                {
                    if (playerPosition.transform.rotation.y != -180)
                    {
                        playerPosition.transform.Rotate(Vector3.down, -180 * 100f * Time.deltaTime);
                    }
                }
                else
                {
                    if (playerPosition.transform.rotation.y != 0)
                    {
                        playerPosition.transform.Rotate(Vector3.up, 0 * 100f * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (playerPosition.transform.position.x - playerGridPosition.x < 0)
                {
                    if (playerPosition.transform.rotation.y != -90)
                    {
                        playerPosition.transform.Rotate(Vector3.down, -90 * 100f * Time.deltaTime);
                    }
                        
                }
                else
                {
                    if (playerPosition.transform.rotation.y != 90)
                    {
                        playerPosition.transform.Rotate(Vector3.down, 90 * 100f * Time.deltaTime);
                    }
                }
            }
            while (Vector3.Distance(playerPosition.transform.position, nodeToWorld) > 0.05f)

            {
                playerPosition.transform.position = Vector3.MoveTowards(playerPosition.transform.position, nodeToWorld, 0.05f * Time.deltaTime);
                yield return null;
            }
            playerPosition.transform.position = currentNode.position;
            //Debug.Log("Hello");
            //Debug.Log(FinalPath.Count);
            FinalPath.Remove(currentNode);
            GridTest.drawPath(FinalPath, lineRenderer);


        }
        turnEnd = false;
        yield return null;
    }
    void Update()
    {
        if (turnEnd)
        {
            StartCoroutine("MovePlayer", gridTest.FinalPath);
        }
    }
}
