using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnYPosition = -5f; 

    void Update()
    {
        if (transform.position.y < respawnYPosition)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        
        transform.position = respawnPoint.position;
    }
}
