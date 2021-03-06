﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct FTransform
{
    public Vector3 pos;
    public Quaternion rot;
    public FTransform(Transform t)
    {
        pos = t.position;
        rot = t.rotation;
    }
}

[RequireComponent(typeof(SphereCollider))]
public class BrokenItem : MonoBehaviour
{
    public List<GameObject> internalObjects;
    public List<FTransform> startingPositions;
    public List<FTransform> endedPositions;
    public float force = 250;
    public float dist = 1;
    public float mod = 0.1f;
    public float rewindSpeed = 1;
    bool disabled = false;
    bool rewinding;
    float _currentRewind;
    float _currentUnRewindable;
    bool rewinded;

    bool isPlayerInteracting;

    GameObject feedbackGo;

    void Start()
    {
        startingPositions = new List<FTransform>();
        
        foreach(var transform in GetComponentsInChildren<Transform>())
        {
            Rigidbody rb =  transform.GetComponent<Rigidbody>();
            if(!rb) continue;

            internalObjects.Add(transform.gameObject);
            startingPositions.Add(new FTransform(transform));
            rb.AddExplosionForce(force, transform.position, dist, mod);
            transform.gameObject.layer = LayerMask.NameToLayer("ItemNoCol");
        }

        feedbackGo = Instantiate(BrokenItemFeedback.staticfeedbackparticle, transform.position, Quaternion.identity);
    }

    void StartRewind()
    {
        if(!disabled)
        {
            disabled = true;
            _currentRewind = 0;
            rewinding = true;
            foreach(var go in internalObjects)
            {
                endedPositions.Add(new FTransform(go.transform));
                Destroy(go.GetComponent<Rigidbody>());
            }
        }
    }

    void Rewind()
    {
        for(int i =0 ;i <  internalObjects.Count; i++)
        {
            FTransform endedT = endedPositions[i];
            FTransform startingT = startingPositions[i];

            Vector3 pos = Vector3.Lerp(endedT.pos, startingT.pos, _currentRewind);
            Quaternion rot = Quaternion.Lerp(endedT.rot, startingT.rot, _currentRewind);
            Quaternion objRot = Quaternion.Lerp(transform.rotation, Quaternion.identity, _currentRewind);
            internalObjects[i].transform.position = pos;
            internalObjects[i].transform.rotation = rot;

        }
    }

    void Update()
    {
        if(!GameEvents.gameRunning)
        {
            return;
        }

        if(!rewinded && !isPlayerInteracting)
        {
            _currentUnRewindable += Time.deltaTime;
        }

        if(!rewinding && isPlayerInteracting)
        {
            StartRewind();
        }

        if(rewinding && _currentRewind < 1.0f)
        {
            if(isPlayerInteracting)
            {
                _currentRewind = Mathf.Clamp(_currentRewind + Time.deltaTime * rewindSpeed, 0, 1);
            }
            else
            {
                _currentRewind = Mathf.Clamp(_currentRewind - Time.deltaTime * rewindSpeed, 0, 1);
            }
            Rewind();
        }
        else if(rewinding)
        {
            rewinding = false;
            rewinded = true;
            GameEvents.ItemRepaired(GameEvents.GetPointsPerTime(_currentUnRewindable), gameObject.transform.position);
            Destroy(feedbackGo);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInteracting = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInteracting  = true;
        }
    }
}


