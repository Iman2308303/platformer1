using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnYPosition = -5f;
    public Health playerHealth;

    void Update()
    {
        if (transform.position.y < respawnYPosition)
        {
            Debug.Log("Respawning player...");
            Respawn();
        }


    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerHealth.ResetHealthToMax();
        transform.position = respawnPoint.position;

        GameManager.Instance.ResetUIText();
        Debug.Log("Respawn Manager Reset UI Text");
    }

}
