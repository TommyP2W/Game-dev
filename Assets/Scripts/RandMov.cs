using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandMov : MonoBehaviour
{
    private int rangex = 2;
    private int rangez = 2;

    [SerializeField] private LayerMask groundLayer;
    public bool chasePlayer = false;
    private GameObject player;
    private GridTest grid;
    private GridCell EnemyPos;
    private GridCell EnemyDestination;
    public  bool finding_path = false;
    private bool coroutineActive = false;
    private LineRenderer lineRenderer;
    public bool isWalking = false;
    // Start is called before the first frame update
    void Awake()
    {
        chasePlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyPos = new GridCell();
        EnemyDestination = new GridCell();
        grid = gameObject.AddComponent<GridTest>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void SetDestination()
    {
        // Get a random position from the desired movement range
        int range_x = Random.Range(-rangex, rangex);
        int range_z = Random.Range(-rangez, rangez);

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 1000f)
        {
            chasePlayer = false;
        }
        // Setting the destination, if chasing, set destination to player point on map, else set it to random walking point with offset
        Vector3 walkPoint = new Vector3(transform.position.x + range_x, transform.position.y,transform.position.z + range_z);
        if (chasePlayer)
        {
            Debug.Log("chasing player");
            EnemyDestination.position = Placement.Grid.WorldToCell(player.transform.position);
        }
        else
        {
            EnemyDestination.position = Placement.Grid.WorldToCell(walkPoint);
        }
        EnemyPos.position = Placement.Grid.WorldToCell(transform.position);
        grid.findPath(EnemyPos, EnemyDestination);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        // If collided with player
        if (other.CompareTag("Player"))
        {
            chasePlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!EndTurn.turnEnd) { 
            chasePlayer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (grid == null)
        {
            Debug.Log("grid is null");
        }
        if (grid != null)
        {
            // If there is not a path, not finding a path and the turn as not ended
            if (grid.FinalPath.Count < 1 && !finding_path && !EndTurn.turnEnd)
            {
                finding_path = true;
                SetDestination();
            }
            else
            {
                // If the corotuine is currently not active
                if (EndTurn.turnEnd && !coroutineActive)
                {
                    coroutineActive = true;
                    StartCoroutine("MovePlayer", grid.FinalPath);
                    // Update total number of coroutines active at the end of the turn
                    EndTurn.CoroutinesActive++;
                }
            }
        }

    }

    IEnumerator MovePlayer(List<GridCell> FinalPath)
    {
        // If the path count is not zero
        while (FinalPath.Count > 0)
        {
            // Character is walking
            isWalking = true;
            // Get the point to walk to and add offset so its centre of the cell
            GridCell currentNode = FinalPath[0];
            EnemyPos.position = Placement.Grid.WorldToCell(transform.position);
            Vector3 nodeToWorld = Placement.Grid.CellToWorld(currentNode.position);
            nodeToWorld.x += 0.5f;
            nodeToWorld.z += 0.5f;
            // If there is no difference in x axis
            if (transform.position.x - nodeToWorld.x == 0)
            {
                // If enemy moving forwards
                if (transform.position.z - nodeToWorld.z < 0)
                {
                    if (transform.rotation.y != 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    // Else, rotate enemy backwards
                    if (transform.rotation.y != -180)
                    {
                        transform.rotation = Quaternion.Euler(0, -180, 0);
                    }
                }
            }
            else
            {
                // If enemy moving right
                if (transform.position.x - nodeToWorld.x < 0)
                {
                    if (transform.rotation.y != 90)
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    }

                }
                else
                {
                    // Else rotate enemy left
                    if (transform.rotation.y != -90)
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                }
            }



            // Calculate the distance between the enemy and the node to walk to
            while (Vector3.Distance(transform.position, nodeToWorld) > 0.1f)

            {
                transform.position = Vector3.MoveTowards(transform.position, nodeToWorld, 1f * Time.deltaTime);
                yield return null;
            }
            transform.position = nodeToWorld;

            // Draw the path and remove the current target node once reached
            GridTest.drawPath(FinalPath, lineRenderer);
            FinalPath.Remove(currentNode);
            
        }
        // If couroutine has finished, finding_path is false, the coroutine is not active and walking is therefore also false.
        finding_path = false;
        coroutineActive = false;
        isWalking = false;
        // Decrement the number of coroutines active at this moment
        EndTurn.CoroutinesActive--;
        yield return null;
    }


}
