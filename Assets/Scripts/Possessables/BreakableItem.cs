﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableItem : MonoBehaviour
{
    public UnityEvent onBreak;
    public Vector3 startingForward;
    public Collider possessCollider;
    public GameObject brokenVersion;
    public void Start()
    {
        startingForward = transform.forward;
    }
    
    public void Break()
    {
        gameObject.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("BrokenItem");
        possessCollider.enabled = false;


        if(onBreak == null)
        {
            return;
        }

        onBreak.Invoke();

        var go = GameObject.Instantiate(brokenVersion, transform.position, Quaternion.identity);
        go.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        print("TEST");
        if(other.gameObject.tag == "Floor")
        {
            Break();
        }
    }
}
