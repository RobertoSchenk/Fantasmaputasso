using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public Vector3 MovementInput;

    public Vector3 ConsumeMovementInput()
    {
        Vector3 movementInput = MovementInput.normalized;
        MovementInput = Vector3.zero;
        return movementInput;
    }
}
