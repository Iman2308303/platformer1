using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorhandleEnemy : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;

    private Vector3 _initialScale = Vector3.one;
    private Vector3 _flipScale = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = transform.parent.GetComponent<Movement>();

        _initialScale = transform.localScale;
        _flipScale = new Vector3(_initialScale.x, _initialScale.y, _initialScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        HandleFlip();
       
    }
    void HandleFlip()
    {
        if (_movement == null)
            return;

        if (_movement.IsRunning)
        {
            if (_movement._inputDirection.x > 0)
                transform.localScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
            else if (_movement._inputDirection.x < 0)
                transform.localScale = _initialScale;
        }

        //if (_movement.flipAnim == false)
        //transform.localScale = _initialScale;
        //else
        //transform.localScale = _flipScale;

    }
}
