using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody rb;
    public int distance;
    public GameObject target;
    Vector3 targetpos;
    public float minDist, speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        targetpos = target.transform.position + new Vector3(0, 0, -distance);
        if (Vector3.Distance(targetpos, transform.position) > 2)
        {
            Vector3 direction = (targetpos - transform.position).normalized;
            rb.MovePosition(transform.position + direction * 10 * Time.deltaTime);
        }
       

    }
}
