﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementComponent : MovementComponent
{
    Rigidbody rb;
    public float speed = 700;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void StopMovement()
    {
        MovementInput = Vector3.zero;
        rb.AddForce(new Vector3(0,-1,0) * speed, ForceMode.Force);
    }

    void Update()
    {
        var input = ConsumeMovementInput();
        rb.AddForce(input * speed * Time.deltaTime, ForceMode.Force);
        if(input.magnitude > 0.1f && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }
}
