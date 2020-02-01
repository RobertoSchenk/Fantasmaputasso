using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MovementComponent
{
    public float Speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += MovementInput.normalized * Speed * Time.deltaTime;
    }
}
