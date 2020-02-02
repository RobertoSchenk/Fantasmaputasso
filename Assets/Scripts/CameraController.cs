using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 XZOffset;
    public GameObject target;
    Vector3 targetpos;
    public float minDist, speed;

    float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        offsetY =  transform.position.y - target.transform.position.y;
        XZOffset = new Vector3(transform.position.x - target.transform.position.x, 0, transform.position.z - target.transform.position.z);
        targetpos = target.transform.position + new Vector3(0, offsetY, 0) + XZOffset;
        transform.position = targetpos;
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = target.transform.position + new Vector3(0, offsetY, 0) + XZOffset;

        if(Mathf.Abs(targetpos.x - transform.position.x) >= minDist)
        {
            targetpos += new Vector3(-minDist,0,0) * Mathf.Sign(targetpos.x  - transform.position.x);
            transform.position = targetpos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * speed);
        }
  
    }
}
