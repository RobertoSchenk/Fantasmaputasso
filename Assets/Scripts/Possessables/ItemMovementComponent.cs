using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementComponent : MovementComponent
{
    Rigidbody rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void StopMovement()
    {
        MovementInput = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        rb.AddForce(ConsumeMovementInput() * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
