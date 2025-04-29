using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;

    private void LateUpdate()
    {
        Vector3 playerPosition = Player.position;
        playerPosition.y = transform.position.y;
        transform.position = playerPosition;
    }

  
}
