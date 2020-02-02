using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MovementComponent
{
    public float Speed;

    public CharacterController controller;
    public GameObject charactermesh;

    // Update is called once per frame

    void Update()
    {
        Vector3 input = ConsumeMovementInput().normalized;

        controller.SimpleMove(input * Speed);

        if(input.magnitude > 0.1f)
        {
            charactermesh.transform.LookAt(charactermesh.transform.position - input, Vector3.up);
        }

    }

}
