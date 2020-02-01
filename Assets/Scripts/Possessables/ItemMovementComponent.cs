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
        rb.AddForce(new Vector3(0,-1,0) * speed, ForceMode.Force);
    }

    void Update()
    {
        rb.AddForce(ConsumeMovementInput() * speed * Time.deltaTime, ForceMode.Force);
    }
}
