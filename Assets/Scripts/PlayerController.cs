using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementComponent moveComponent;
    // Start is called before the first frame update
    void Start()
    {
        moveComponent = GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moveComponent.MovementInput = new Vector3(0,0,0);

        moveComponent.MovementInput = new Vector3(x, 0f, z);
        //if(Input.GetKey(KeyCode.W))
        //{
        //    moveComponent.MovementInput = new Vector3(0,1,0);
        //}
    }
}
