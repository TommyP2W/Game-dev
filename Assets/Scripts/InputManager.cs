using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public Camera SceneCamera;
    
    private Vector3 lastPosition;
    [SerializeField]
    private LayerMask placementLayerMask;

    public Vector3 GetSelectedMapPostion()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCamera.nearClipPlane;
        Ray ray = SceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
