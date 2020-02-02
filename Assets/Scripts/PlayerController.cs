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

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moveComponent.MovementInput = new Vector3(0,0,0);

        moveComponent.MovementInput = new Vector3(-z, 0f, x);
        //if(Input.GetKey(KeyCode.W))
        //{
        //    moveComponent.MovementInput = new Vector3(0,1,0);
        //}

        Shader.SetGlobalVector("_PlayerPos", transform.position);
        Shader.SetGlobalVector("PlayerPos", transform.position);
    }
}
