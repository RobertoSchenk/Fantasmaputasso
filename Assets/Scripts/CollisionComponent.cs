using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionComponent : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if(onTriggerEnter != null && other.tag == "Player")
        {
            onTriggerEnter.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(onTriggerExit != null && other.tag == "Player")
        {
            onTriggerExit.Invoke();
        }
    }

}
