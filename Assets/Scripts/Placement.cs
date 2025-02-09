using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager manager;
    [SerializeField]
    private Grid Grid;
    private NavMeshAgent playerobj;
    [SerializeField]
    private LineRenderer lineRenderer_1;
  
    private Transform points;
 
    private void Start()
    {
        playerobj = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 mousePosition = manager.GetSelectedMapPostion();
        Vector3Int gridPosition = Grid.WorldToCell(mousePosition); 
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = Grid.CellToWorld(gridPosition);

        lineRenderer_1 = GetComponent<LineRenderer>();

        Vector3[] line_pos = {
            (cellIndicator.transform.position + (new Vector3(4f,0f,0f))),
            (cellIndicator.transform.position + (new Vector3(0f, 0f, 0f))),
            (cellIndicator.transform.position + (new Vector3(0f, 0f, 4f))),
            (cellIndicator.transform.position + (new Vector3(4f, 0f, 4f))),
            (cellIndicator.transform.position + (new Vector3(4f, 0f,0f)))
        };
        lineRenderer_1.positionCount = line_pos.Length;
        for (int i = 0; i < line_pos.Length; i++)
        {
            lineRenderer_1.SetPosition(i, line_pos[i]);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _offset = new Vector3 (2.5f, 0f, 2.5f);
            playerobj.SetDestination(cellIndicator.transform.position + _offset);
        }
    }
}
