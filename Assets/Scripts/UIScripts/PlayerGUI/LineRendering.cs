using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineRendering : MonoBehaviour
{
    // Start is called before the first frame update
   private LineRenderer lineRenderer;
   private Transform[] points;
   private NavMeshAgent agent;


    /// <summary>
    /// forget this class for now.
    /// </summary>
    private void Awake()
    {
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void LineSetup(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }

    IEnumerator positionUpdate()
    {
        this.points[0] = agent.transform;


        yield return null;
    }
    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
