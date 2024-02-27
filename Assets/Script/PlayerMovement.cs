using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public Cooldown Random;
    protected override void HandleInput()
    {
        _InputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump")
            && Random.CurrentProgress != Cooldown.Progress.Ready
                || Random.CurrentProgress != Cooldown.Progress.Finished)
        {
            _IsJumping = true;
        }
        else
        {
            _IsJumping = false;
         }
    }
  
}
