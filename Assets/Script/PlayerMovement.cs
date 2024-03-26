using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {   
            DoJump();
            _IsJumping = true;
        }
        else
        {
            _IsJumping = false;
        }
    }
}