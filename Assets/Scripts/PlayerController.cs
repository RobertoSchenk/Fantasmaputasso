using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementComponent moveComponent;
    void Start()
    {
        moveComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        moveComponent.MovementInput = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W))
        {
            moveComponent.MovementInput = new Vector3(0,1,0);
        }
    }
}
