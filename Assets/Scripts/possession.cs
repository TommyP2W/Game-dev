using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Possession : MonoBehaviour
{


    public Camera camera;

    public static bool isPossessed;
    public static GameObject playerClass;
    public int possessionTime;
    
    public static Dictionary<GameObject, List<GridCell>> possessionPath;
    //public void Possess()
    //{
    //    Debug.Log("Possessed");
    //    Debug.Log("Requested : " + GameObject.Find("Player").GetComponent<PlayerClass>().RequestedEnemy.name);
    //    GameObject enemy = GameObject.Find("Player").GetComponent<PlayerClass>().RequestedEnemy;
    //    Smoothcamera.Target = enemy.transform;
    //    //camera.transform.position = enemy.transform.position;
    //}

    public void Start()
    {
        possessionPath = new Dictionary<GameObject, List<GridCell>>();
        playerClass = GameObject.FindGameObjectWithTag("Player") ;
    }

    public void Possess()
    {
        if (!isPossessed)
        {
            possessionTime = 5;
            Debug.Log("Possessed");
            Debug.Log("Requested : " + playerClass.GetComponent<PlayerClass>().RequestedEnemy);
            GameObject enemy = playerClass.GetComponent<PlayerClass>().RequestedEnemy;
            playerClass.GetComponent<PlayerClass>().possessedEnemy = enemy;
            Smoothcamera.cam.RenderWithShader(Shader.Find("Fog"), "fog");
            Smoothcamera.Target = enemy.transform;
            isPossessed = true;
        }
        //camera.transform.position = enemy.transform.position;
    }

    public void attackPossessed()
    {
        GameObject enemy = playerClass.GetComponent<PlayerClass>().RequestedEnemy;
        playerClass.GetComponent<PlayerClass>().current_sanity -= 5;
        foreach (GridCell cell in GridTest.getNeighbours(GridManager.gridLayout[GridManager.grid.WorldToCell(playerClass.GetComponent<PlayerClass>().possessedEnemy.transform.position)]))
        {
            if (cell.occupiedBy == enemy)
            {
                playerClass.GetComponent<PlayerClass>().possessedEnemy.GetComponent<Characters>().requestedEnemy = enemy;
                playerClass.GetComponent<PlayerClass>().possessedEnemy.GetComponent<Characters>().attack();

            }
        }
    }

    public void MovePossessed()
    {
        possessionTime--;
        StartCoroutine(MovePossessed(possessionPath));

        if (possessionTime == 0)
        {
            quitPossession();   
        }
    }

    public void quitPossession()
    {
        playerClass.GetComponent<PlayerClass>().possessedEnemy = null;

        Smoothcamera.Target = playerClass.transform;

        isPossessed = false;
    }

    IEnumerator MovePossessed(Dictionary<GameObject, List<GridCell>> path)
    {
        int i = 0;
        // While there is a path
        foreach (GameObject person in path.Keys)
        {
            Smoothcamera.Target = person.transform;
            path[person][0].occupied = false;
            path[person][0].occupiedBy = null;
            path[person].Last().occupied = true;
            path[person].Last().occupiedBy = person;

        
            while (path[person].Count > 0)

            {
                // Get the first node of the path
                GridCell currentNode = path[person][0];

                // Transform the current position of the character to a cell, and the cell to a world position
                Vector3Int playerGridPosition = GridManager.grid.WorldToCell(person.transform.position);
                Vector3 nodeToWorld = GridManager.grid.CellToWorld(currentNode.position);
                nodeToWorld.x += 0.5f;
                nodeToWorld.z += 0.5f;
                // If there is no difference on the x axis
                if (person.transform.position.x - nodeToWorld.x == 0)
                {
                    // If the player is moving forwards
                    if (person.transform.position.z - nodeToWorld.z < 0)
                    {
                        if (person.transform.rotation.y != 0)
                        {
                            person.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    else
                    {
                        // Else, rotate the player backwards
                        if (person.transform.rotation.y != -180)
                        {
                            person.transform.rotation = Quaternion.Euler(0, -180, 0);
                        }
                    }
                }
                else
                {
                    // If the player is moving right
                    if (person.transform.position.x - nodeToWorld.x < 0)
                    {
                        if (person.transform.rotation.y != 90)
                        {
                            person.transform.rotation = Quaternion.Euler(0, 90, 0);
                        }

                    }

                    // If the player is moving left
                    else
                    {
                        if (person.transform.rotation.y != -90)
                        {
                            person.transform.rotation = Quaternion.Euler(0, -90, 0);
                        }
                    }
                }

                // Calculating the difference from the node at the world position to the player
                while (Vector3.Distance(person.transform.position, nodeToWorld) > 0.1f)

                {
                    // Make the player move towards the node at the world position
                    person.transform.position = Vector3.MoveTowards(person.transform.position, nodeToWorld, 5f * Time.deltaTime);
                    yield return null;
                }
                // Ensures no edge cases such as the player not being completely at the node
                person.transform.position = nodeToWorld;

                // Remove the path and draw
             

                path[person].Remove(currentNode);



            }
          
            i++;
            Debug.Log(i);
        }
        possessionPath.Remove(playerClass.GetComponent<PlayerClass>().possessedEnemy);
        Smoothcamera.Target = playerClass.GetComponent<PlayerClass>().RequestedEnemy.transform;
        // Coroutine has finished so set the turn ending to false
     
        yield return null;
    }

}