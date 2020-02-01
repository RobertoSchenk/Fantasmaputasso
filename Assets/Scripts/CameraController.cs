using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int distance;
    public GameObject target;
    Vector3 targetpos;
    public float minDist, speed;

    float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        offsetY =  transform.position.y - target.transform.position.y;
        targetpos = target.transform.position + new Vector3(0, offsetY, -distance);
        transform.position = targetpos;
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = target.transform.position + new Vector3(0, offsetY, -distance);

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
