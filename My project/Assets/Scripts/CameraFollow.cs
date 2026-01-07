using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Vector2 playerPos;
    void Start()
    {
        
    }

    void Update()
    {
        playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
    }
}
