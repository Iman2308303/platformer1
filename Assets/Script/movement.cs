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
    public Cooldown BufferJump;



    public LayerMask GroundLayerMask;

    protected int JumpRemaining = 2;

    protected bool _IsGrounded = false;
    protected bool _IsRunning = false;
    protected bool _IsFalling = false;
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
    protected void DoubleJump()
    {

        if (!_IsGrounded && JumpRemaining > 0)
        {
            _canJump = true;
            JumpRemaining--;
            if (!_IsGrounded && JumpRemaining > 0)
            {
                _canJump = false;
            }
        }

    }

    protected void TryBufferJump()
    {
        BufferJump.StartCooldown();
    }
    protected virtual void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_IsGrounded || JumpRemaining > 0)
            {
                DoJump();
            }
        }
    }
    protected virtual void DoJump()
    {
        TryBufferJump();
        

        if (!_canJump)
            return;

        if (CoyoteTime.CurrentProgress == Cooldown.Progress.Finished)
            return;

        if (!_IsJumping)
        {
            _IsJumping = true;
            _canJump = false;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, JumpForce);

            if (!_IsGrounded)
            {
                JumpRemaining--;
            }
        }
        else if (!_IsGrounded && JumpRemaining > 0)
        {
            JumpRemaining--;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, JumpForce);
        }

        CoyoteTime.StopCooldown();
    }

    void HandleMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (_IsGrounded && !_IsJumping)
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

        if (_IsGrounded && !_IsJumping)
        {
            _canJump = true;
            JumpRemaining = 2;



            if (CoyoteTime.CurrentProgress != Cooldown.Progress.Ready)
                CoyoteTime.StopCooldown();

            if (BufferJump.CurrentProgress is Cooldown.Progress.Started or Cooldown.Progress.InProgress)
            {
                DoJump();
            }

            if (_IsJumping)
                DoJump();

            
        }
        if (!_IsGrounded && _IsJumping && CoyoteTime.CurrentProgress == Cooldown.Progress.Ready)
            CoyoteTime.StopCooldown();
        DoubleJump();

    }
}