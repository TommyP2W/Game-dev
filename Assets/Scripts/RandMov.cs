using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandMov : MonoBehaviour
{
    private int rangex = 3;
    private int rangez = 3;
   // private NavMeshAgent agent;
    [SerializeField] private bool walkable;
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private LayerMask groundLayer;
    private bool chase;
    private GameObject player;
    private Vector3 Direction;
    private GridTest grid;
    private GridCell drummerPos;
    private GridCell drummerDestination;
    private bool finding_path = false;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.speed = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player");
        drummerPos =  new GridCell();
        drummerDestination = new GridCell();
        grid = gameObject.AddComponent<GridTest>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    public void SetDestination()
    {
       
        int range_x = Random.Range(-rangex, rangex);
        int range_z = Random.Range(-rangez, rangez);
        walkPoint = new Vector3(transform.position.x + range_x, transform.position.y,transform.position.z + range_z);
        if (chase)
        {
            drummerDestination.position = Placement.Grid.WorldToCell(player.transform.position);
        }
        else
        {
            drummerDestination.position = Placement.Grid.WorldToCell(walkPoint);
        }
        drummerPos.position = Placement.Grid.WorldToCell(transform.position);
        grid.findPath(drummerPos, drummerDestination);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chase = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        chase = false;
    }
    // Update is called once per frame
    void Update()
    {
      

        if (grid.FinalPath.Count < 1 && !finding_path && !EndTurn.turnEnd)
        {
            finding_path = true;
            SetDestination();

        }
        else
        {
            if (EndTurn.turnEnd)
            {
                GridTest.drawPath(grid.FinalPath, lineRenderer);

                StartCoroutine("MovePlayer", grid.FinalPath);
            }
        }
        
    }

    IEnumerator MovePlayer(List<GridCell> FinalPath)
    {
        while (FinalPath.Count > 0)
        {
            GridCell currentNode = FinalPath[0];
            drummerPos.position = Placement.Grid.WorldToCell(transform.position);
            Vector3 nodeToWorld = Placement.Grid.CellToWorld(currentNode.position);

            while (Vector3.Distance(transform.position, nodeToWorld) > 0.1f)

            {
                transform.position = Vector3.MoveTowards(transform.position, nodeToWorld, 0.01f * Time.deltaTime);
                yield return null;
            }
            transform.position = nodeToWorld;
          
            FinalPath.Remove(currentNode);
            GridTest.drawPath(FinalPath, lineRenderer);



        }
        finding_path = false;
        yield return null;
    }


}
