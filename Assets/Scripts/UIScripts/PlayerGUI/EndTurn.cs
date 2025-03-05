using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public static bool turnEnd = false;
    public static bool isMovementBlocked = true;
    public static Vector3 playerRequestedMove;
    private GameObject playerPosition;
    private LineRenderer lineRenderer;
    private GridTest gridTest;
    private GameObject endButton;
    public static int CoroutinesActive = 0; // Number of coroutines running at this moment


    public void Start()
    {
        endButton = GameObject.Find("Button");
        lineRenderer = GameObject.FindGameObjectWithTag("Line renderer").GetComponent<LineRenderer>();
        gridTest = GameObject.FindGameObjectWithTag("Player").GetComponent<GridTest>();
    }
    public void endTurn()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
       

        Debug.Log("Ended turn");

        // If there is a path, the turn has ended, and no other corotuines are running
        // This stops the player being able to move while other enemies are also still moving previously
        if (gridTest.FinalPath.Count > 0 && !turnEnd && CoroutinesActive == 0)

        {
            // The turn has ended
            turnEnd = true;
            StartCoroutine("MovePlayer", gridTest.FinalPath);
        }
    }

    IEnumerator MovePlayer(List<GridCell> FinalPath)
    {
        // While there is a path
        while (FinalPath.Count > 0)

        {
            // Get the first node of the path
            GridCell currentNode = FinalPath[0];
            // Transform the current position of the character to a cell, and the cell to a world position
            Vector3Int playerGridPosition = Placement.Grid.WorldToCell(playerPosition.transform.position);
            Vector3 nodeToWorld = Placement.Grid.CellToWorld(currentNode.position);
            nodeToWorld.x += 0.5f;
            nodeToWorld.z += 0.5f;
            // If there is no difference on the x axis
            if (playerPosition.transform.position.x - nodeToWorld.x == 0)
            {
                // If the player is moving forwards
                if (playerPosition.transform.position.z - nodeToWorld.z < 0)
                {
                    if (playerPosition.transform.rotation.y != 0)
                    {
                        playerPosition.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    // Else, rotate the player backwards
                    if (playerPosition.transform.rotation.y != -180)
                    {
                        playerPosition.transform.rotation = Quaternion.Euler(0, -180, 0);
                    }
                }
            }
            else
            {
                // If the player is moving right
                if (playerPosition.transform.position.x - nodeToWorld.x < 0)
                {
                    if (playerPosition.transform.rotation.y != 90)
                    {
                        playerPosition.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }

                }

                // If the player is moving left
                else
                {
                    if (playerPosition.transform.rotation.y != -90)
                    {
                        playerPosition.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                }
            }
            
            // Calculating the difference from the node at the world position to the player
            while (Vector3.Distance(playerPosition.transform.position, nodeToWorld) > 0.1f)

            {
                // Make the player move towards the node at the world position
                playerPosition.transform.position = Vector3.MoveTowards(playerPosition.transform.position, nodeToWorld, 1f * Time.deltaTime);
                yield return null;
            }
            // Ensures no edge cases such as the player not being completely at the node
            playerPosition.transform.position = nodeToWorld;

            // Remove the path and draw
            GridTest.drawPath(FinalPath, lineRenderer);

            FinalPath.Remove(currentNode);


        }
        // Coroutine has finished so set the turn ending to false
        turnEnd = false;
        yield return null;
    }

    public void Update()
    {
        if (CoroutinesActive > 0)
        {
            endButton.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
        }
        else
        {
            endButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
        }
    }


}
