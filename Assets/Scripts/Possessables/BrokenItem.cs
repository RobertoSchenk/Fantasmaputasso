using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class BrokenItem : MonoBehaviour
{
    public List<GameObject> internalObjects;
    public List<FTransform> startingPositions;
    public List<FTransform> endedPositions;
    public float force = 25;
    public float dist = 1;
    public float mod = 1.1f;
    public float rewindSpeed = 1;
    public float unrewindableDelay = 3;
    bool disabled = false;
    bool rewinding;
    float _currentRewind;
    float _currentUnRewindable;
    bool rewinded;


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
        }
    }

    void StartRewind()
    {
        if(!disabled && !IsUnrewindable())
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

    bool IsUnrewindable()
    {
        return _currentUnRewindable > unrewindableDelay;
    }

    void Update()
    {
        if(!rewinded && !rewinded)
        {
            _currentUnRewindable += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartRewind();
        }

        if(rewinding && _currentRewind < 1.0f)
        {
            _currentRewind = Mathf.Clamp(_currentRewind + Time.deltaTime * rewindSpeed, 0, 1);
            Rewind();
        }
        else if(rewinding)
        {
            rewinding = false;
            rewinded = true;  
        }
    }


    void OnTriggerExit(Collision other)
    {

        rewinding = false;
    }

    void OnTriggerEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            rewinding  = true;
        }
    }
}


