using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovementComponent : MovementComponent
{
    public Rigidbody rb;
    public float speed;
    void Update()
    {
        rb.MovePosition(transform.position + ConsumeMovementInput() * speed * Time.deltaTime);
    }
}
