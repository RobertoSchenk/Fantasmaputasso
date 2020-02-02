using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomId;
    public Renderer InvisWall;
    public GameObject player;
    public GameObject camera;
    void Start()
    {
       InvisWall.enabled = false;

       player = GameObject.FindGameObjectWithTag("Player");
       camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector= (player.transform.position - camera.transform.position);
        float dist = vector.magnitude;
        RaycastHit outHit;
        if(Physics.Raycast(camera.transform.position, vector.normalized, out outHit, dist))
        {
            if(outHit.collider.gameObject == InvisWall)
            {
                InvisWall.enabled = false;
            }
        }
        else
        {
                InvisWall.enabled = false;
        }
    }
}
