using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public static bool turnEnd = false;
    public static bool playerSelectedPath = false;
    public bool findingPath = false;
    private GameObject player;
    private LineRenderer lineRenderer;
    private GridTest gridTest;
    private GameObject endButton;
    private GameObject HealthSlider;
    private GameObject StaminaSlider;
    public static int CoroutinesActive = 0; // Number of coroutines running at this moment
    public Dictionary<GameObject, List<GridCell>> requestedMovements;
    public GameObject[] enemiesArr;
    public GridCell EnemyDestination = new GridCell();
    public GridCell EnemyPos = new GridCell();
    public GridCell playerPos = new GridCell();

    public void Awake()
    {
        requestedMovements = new Dictionary<GameObject, List<GridCell>>();
    }
    public void Start()
    {
        endButton = GameObject.Find("Button");
        HealthSlider = GameObject.Find("HealthSlider");
        StaminaSlider = GameObject.Find("StaminaSlider");
        lineRenderer = GameObject.FindGameObjectWithTag("Line renderer").GetComponent<LineRenderer>();
        gridTest = GameObject.FindGameObjectWithTag("Player").GetComponent<GridTest>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemiesArr = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void SetEnemyDestination(GameObject[] enemies)
    {
        requestedMovements = new Dictionary<GameObject, List<GridCell>>();

        if (enemies == null)
        {
            return;
        }


        Debug.Log("AMOUNT OF ENEMIES + : " + enemies.Length);
        foreach (GameObject enemy in enemies)

        {
            if (enemy.gameObject.activeInHierarchy)
            {
                GridCell enemycell = GridManager.gridLayout[GridManager.grid.WorldToCell(enemy.transform.position)];
                GridCell EnemyDestination;
                Debug.Log("enemy cell : " + enemycell.position);

                int range_x = UnityEngine.Random.Range(-2, 2);
                int range_z = UnityEngine.Random.Range(-2, 2);

                if (Vector3.Distance(enemy.transform.position, player.transform.position) > 15f)
                {
                    enemy.GetComponent<Characters>().chasing = false;
                }
                // Setting the destination, if chasing, set destination to player point on map, else set it to random walking point with offset
                Vector3 walkPoint = new Vector3(enemy.transform.position.x + range_x, 0, enemy.transform.position.z + range_z);
                //Debug.Log("WALKPOINT BEFORE LOOKUP " + GridManager.gridLayout[GridManager.grid.WorldToCell(walkPoint)].position);
                if (enemy.GetComponent<Characters>().chasing)
                {
                    // Reference to the player requested movement script.
                    List<GridCell> playerReqMovements = player.GetComponent<PlayerClass>().ReqPlayerMovement;
                    //Debug.Log("chasing player");
                    if (playerReqMovements.Count > 0)
                    {
                        if (!GridManager.gridLayout.ContainsValue(playerReqMovements.Last()))
                        {
                            Debug.Log("Skipping");
                            continue;
                        }
                        EnemyDestination = playerReqMovements.Last();

                        gridTest.findPath(enemycell, EnemyDestination);
                    }
                    else
                    {
                        if (!GridManager.gridLayout.ContainsKey(GridManager.grid.WorldToCell(player.transform.position)))
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        EnemyDestination = GridManager.gridLayout[GridManager.grid.WorldToCell(player.transform.position)];
                        gridTest.findPath(enemycell, EnemyDestination);
                    }
                    if (gridTest.FinalPath != null && EnemyDestination != null && gridTest.FinalPath.Count > 0)
                    {
                        Debug.Log("Final path is not null, enemy destination not null, more than one element");
                    }

                }
                else
                {
                    if (!GridManager.gridLayout.ContainsKey(GridManager.grid.WorldToCell(walkPoint)))
                    {
                        Debug.Log("Skipping");
                        continue;
                    }
                    EnemyDestination = GridManager.gridLayout[GridManager.grid.WorldToCell(walkPoint)];

                    gridTest.findPath(enemycell, EnemyDestination);
                }
                //Debug.Log("HELLOS");
                //Debug.Log("Final path " + gridTest.FinalPath);
                // Get a random position from the desired movement range
                if (gridTest.FinalPath != null && gridTest.FinalPath.Count > 0)
                {
                    if (gridTest.FinalPath.Last().occupied)
                    {
                        Debug.Log("requested : " + gridTest.FinalPath.Last().position);
                        Debug.Log("requested_occupied? : " + gridTest.FinalPath.Last().occupied);

                        List<GridCell> neighbours = GridTest.getNeighbours(EnemyDestination);
                        
                        if (neighbours.Count > 0)
                        {
                           
                                resolveNeighbours(enemy, enemycell, neighbours);
                        }
                        else
                        {
                            Debug.Log("neighbours empty");
                        }

                    }
                    else
                    {
                        //     Debug.Log("THRER");
                        //Debug.Log("sjaidjaidjajaimcacijaicjaca");
                        gridTest.FinalPath.Last().occupied = true;
                        gridTest.FinalPath.Last().occupiedBy = enemy;
                        requestedMovements.Add(enemy, gridTest.FinalPath);
                    }
                }
                else
                {
                    Debug.Log("idjfoisajdoisajfdoiasjfa");
                }
            }

        }
        Debug.Log("finished");
        findingPath = true;
        turnEnd = true;

        Debug.Log("starting corotuine");
        Debug.Log(requestedMovements.Count);
        StartCoroutine(MovePlayer(requestedMovements, false));
    }

    public void resolveNeighbours(GameObject enemy, GridCell enemyPos, List<GridCell> neighbours)
    {
        if (neighbours.Count > 0 && neighbours != null)
        {
            foreach (GridCell neighbour in neighbours)
            {
                if (!neighbour.occupied)
                {
                    gridTest.findPath(enemyPos, neighbour);
                    if (!gridTest.FinalPath.Last().occupied)
                    {
                        gridTest.FinalPath.Last().occupied = true;
                        gridTest.FinalPath.Last().occupiedBy = enemy;
                        requestedMovements.Add(enemy, gridTest.FinalPath);
                        //Debug.Log("found neighbour");
                        return;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (neighbours.Count > 0 && neighbours != null)
                {
                    List<GridCell> new_neighbours = GridTest.getNeighbours(neighbours[0]);
                    if (new_neighbours.Count > 0)
                    {
                        foreach (GridCell new_neighbour in new_neighbours)
                        {
                            //
                            if (!new_neighbour.occupied)
                            {
                                gridTest.findPath(enemyPos, new_neighbour);
                                if (!gridTest.FinalPath.Last().occupied)
                                {
                                    gridTest.FinalPath.Last().occupied = true;
                                    gridTest.FinalPath.Last().occupiedBy = enemy;
                                    requestedMovements.Add(enemy, gridTest.FinalPath);
                                    //Debug.Log("found neighbour");
                                    return;
                                }

                            }
                            else
                            {
                                neighbours.Add(new_neighbour);
                                //Debug.Log("still searching");

                            }
                        }
                        neighbours.Remove(neighbours[0]);

                    }
                    else
                    {
                        Debug.Log("No new neighbours");
                        return;
                    }
                }
                else
                {
                    Debug.Log("No Remaining neighbours");
                    return;
                }
            }
        }
        else
        {
            Debug.Log("searching neighbor list null");
            return;
        }
        Debug.Log("ran out of neighbours");
        return;

    }



    public void endTurn()
    {
        // Creating a new list full of the player requested movements, previously was a reference so coroutine removed elements before they could be accessed by enemies.
        List<GridCell> playerReqMovement = new List<GridCell>(player.GetComponent<PlayerClass>().ReqPlayerMovement);
        requestedMovements = new Dictionary<GameObject, List<GridCell>>();
      
        Debug.Log("Ended turn");
        Debug.Log("PLAYER REQ MOVEMENT COUNT " + playerReqMovement.Count);
        // If there is a path, the turn has ended, and no other corotuines are running
        // This stops the player being able to move while other enemies are also still moving previously

        if (playerReqMovement.Count > 0)
        {
            Debug.Log("Has the turn ended? : " + turnEnd);
            Debug.Log("COROUTINES ACTIVE : " + CoroutinesActive);
            if (playerReqMovement.Count > 0 && !turnEnd && CoroutinesActive == 0)
            {
                // The turn has ended
                Debug.Log("helo");
                playerReqMovement[0].occupied = true;
                playerReqMovement.Last().occupied = true;
                requestedMovements.Add(player, playerReqMovement);
                StartCoroutine(MovePlayer(requestedMovements, true));

               

            }
        }
        // If the player wants to skip their turn without moving
        Debug.Log(playerReqMovement.Count);
        if  (playerReqMovement.Count == 0)
        {
            Debug.Log("Skipping");
            SetEnemyDestination(enemiesArr);

        }
    }
    

    IEnumerator MovePlayer(Dictionary<GameObject, List<GridCell>> path, bool playerMove)
    {
        CoroutinesActive++;
        int i = 0;
        Debug.Log("e");
        // While there is a path
        Debug.Log(path.Count);
        foreach (GameObject person in path.Keys)
        {
            Smoothcamera.Target = person.transform;
            Debug.Log("AMOUNT OF REQUESTS: " + path.Count);
            path[person][0].occupied = false;
            path[person][0].occupiedBy = null;
            path[person].Last().occupied = true;
            path[person].Last().occupiedBy = person;
            if (person.tag == "Player")
            {
                person.GetComponent<PlayerClass>().isWalking = true;
            } else
            {
                person.GetComponent<Characters>().isWalking = true;
            }
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
                GridTest.drawPath(path[person], lineRenderer);

                path[person].Remove(currentNode);
               
                

            }
            if (person.tag == "Player")
            {
                person.GetComponent<PlayerClass>().isWalking = false;
            }
            else
            {
                person.GetComponent<Characters>().isWalking = false;
            }
            i++;
            Debug.Log(i);
        }
        Smoothcamera.Target = player.transform;
        // Coroutine has finished so set the turn ending to false
        turnEnd = false;
        findingPath = false;
        playerSelectedPath = false;
        CoroutinesActive--;
        if (playerMove)
        {
            SetEnemyDestination(enemiesArr);

        } else
        {
            player.GetComponent<PlayerClass>().ReqPlayerMovement.Clear();
            StartCoroutine(EnemyActions(enemiesArr));
        }

        yield return null;
    }
    IEnumerator EnemyActions(GameObject[] enemies)
    {
        if (enemies != null && enemies.Length > 0)
        {
            Debug.Log("hello");

            foreach (GameObject enemy in enemies)

            {
                Debug.Log("hello");
                enemy.GetComponent<Characters>().actionSelector();
                yield return null;
            }
        }
    }

    public void Update()
    {
        HealthSlider.GetComponentInChildren<Slider>().value = (float)player.GetComponent<PlayerClass>().currentHealth / 100;
        StaminaSlider.GetComponentInChildren<Slider>().value = (float)player.GetComponent<PlayerClass>().currentStamina / 10;
        if (CoroutinesActive > 0)
        {
            endButton.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
        }
        else
        {
            if (playerSelectedPath)
            {
                endButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";

            } else
            {
                endButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn without movement";

            }
        }
    }


}
