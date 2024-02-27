using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Vector2 EnemyDirection = new Vector2 (1, 0);
    public Transform Target;
    protected override void HandleInput()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (Target == null)
            return;

        Vector2 targetDirection = Target.position - transform.position;
        targetDirection = targetDirection.normalized;

        _InputDirection = targetDirection;
       
    }
}
