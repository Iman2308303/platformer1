using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float JumpForce = 50f;

    public Transform GroundCheck;
    public float GroundCheckRadius = 1f;
    public float MaxSlopeAngle = 45f;

    public LayerMask GroundLayerMask;

    bool _IsGrounded = false;
    bool _IsJumping = false;

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

    void FixedUpdate()
    {
        CheckGround();

        HandleMovement();
    }

    void HandleInput()
    {
        _InputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetButton("Jump"))
        {
            _IsJumping = true;
        }
        else
        {
            _IsJumping = false;
        }
    }
    void DoJump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, JumpForce);
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

        if ( _IsGrounded )
        {
            if ( _IsJumping )
            DoJump();
        }
    }
}
