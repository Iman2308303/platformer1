 using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float JumpForce = 50f;

    //ground check
    public Transform GroundCheck;
    public float GroundCheckRadius = 1f;
    public float MaxSlopeAngle = 45f;

    public Cooldown CoyoteTime;
    public Cooldown BufferTime;

    

    public LayerMask GroundLayerMask;

    protected bool _IsGrounded = false;
    protected bool _IsJumping = false;
    protected bool _canJump = true;


    protected Vector3 _InputDirection;

    protected Rigidbody2D _rigidbody2d;
    protected Collider2D _collider2d;
    // Start is called before the first frame update


    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
    }

    protected virtual void FixedUpdate()
    {
        CheckGround();

        HandleMovement();
    }

    protected void TryBufferJump()
    {
        
    }
    protected virtual void HandleInput()
    {
        
    }
    protected virtual void DoJump()
    {
        if (!_canJump)
            return;

        if (CoyoteTime.CurrentProgress == Cooldown.Progress.Finished)
            return;

        _canJump = false;
        _IsJumping = true;

        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, JumpForce);

        CoyoteTime.StopCooldown();
    }

    void HandleMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (_IsGrounded && !_IsJumping )
        {
            targetVelocity = new Vector2(_InputDirection.x * (Acceleration), 0f);
        }
        else
        {
            targetVelocity = new Vector2(_InputDirection.x * (Acceleration), _rigidbody2d.velocity.y);
        }
        
        _rigidbody2d.velocity = targetVelocity;
    }
    void CheckGround()
    {
        _IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayerMask);
        Debug.Log(_IsGrounded);

        if (_rigidbody2d.velocity.y <= 0)
        {
            _IsJumping = false;
        }

        if ( _IsGrounded && !_IsJumping )
        {
            _canJump = true;

            if (CoyoteTime.CurrentProgress != Cooldown.Progress.Ready)
                CoyoteTime.StopCooldown();

          
           
        }
        if (!_IsGrounded && _IsJumping && CoyoteTime.CurrentProgress == Cooldown.Progress.Ready)
            CoyoteTime.StopCooldown();


    }
}
