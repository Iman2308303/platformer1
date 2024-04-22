using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : Movement
{
    protected bool FlipDirection = false;
    protected override void HandleInput()
    {
        if (FlipDirection)
        {
           
            _inputDirection = Vector2.left;
        }
        else
        {
            
            _inputDirection = Vector2.right;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null && playerHealth.CanDamage)
            {
                // Damage the player
                playerHealth.Damage(1, gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Boundary"))
            return;


        FlipDirection = !FlipDirection;
    }

}
