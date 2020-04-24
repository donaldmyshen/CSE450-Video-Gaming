﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public class PlayerController : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    public Transform[] attackZones;

    // State Tracking
    public Direction facingDirection;

    // Configurations
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public float moveSpeed;

    // Methods
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(keyUp))
        {
            _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyDown))
        {
            _rigidbody.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyLeft))
        {
            _rigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyRight))
        {
            _rigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("attack");

            // Convert enumeration to an index
            int facingDirectionIndex = (int)facingDirection;

            // Get ar=ttack zone from index
            Transform attackZone = attackZones[facingDirectionIndex];

            // What objects are within a circle at that attack zone?
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 0.1f);

            // Handle each hit target
            foreach (Collider2D hit in hits)
            {
                Breakable breakableObject = hit.GetComponent<Breakable>();
                if (breakableObject)
                {
                    breakableObject.Break();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = _rigidbody.velocity.magnitude;
        _animator.SetFloat("speed", movementSpeed);
        if (movementSpeed > 0.1f)
        {
            _animator.SetFloat("movementX", _rigidbody.velocity.x);
            _animator.SetFloat("movementY", _rigidbody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("attack");
        }
    }

    void LateUpdate()
    {
        if (String.Equals(_spriteRenderer.sprite.name, "zelda1_8"))
        {
            facingDirection = Direction.Up;
        }
        else if (String.Equals(_spriteRenderer.sprite.name, "zelda1_4"))
        {
            facingDirection = Direction.Down;
        }
        else if (String.Equals(_spriteRenderer.sprite.name, "zelda1_10"))
        {
            facingDirection = Direction.Left;
        }
        else if (String.Equals(_spriteRenderer.sprite.name, "zelda1_6"))
        {
            facingDirection = Direction.Right;
        }
    }
}