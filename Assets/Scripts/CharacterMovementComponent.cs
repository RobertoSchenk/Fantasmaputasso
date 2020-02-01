using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MovementComponent
{
    public float Speed;

    public CharacterController controller;

    // Update is called once per frame

    void Update()
    {


        controller.Move(MovementInput.normalized * Speed * Time.deltaTime);

    }

}
