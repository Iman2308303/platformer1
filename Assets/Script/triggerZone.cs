using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject Enemy;

    private bool hasAppeared = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (!hasAppeared)
            {
                
                Enemy.SetActive(true);
                hasAppeared = true; 
            }
        }
    }
}