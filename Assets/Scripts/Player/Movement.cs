using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float maxSpeed = 2f, acceleration = 50f, deacceleration = 100f;
    [SerializeField] private float currentSpeed = 0f;

    private Vector2 _oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            _oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        _rb.velocity = _oldMovementInput * currentSpeed;
    }
}
