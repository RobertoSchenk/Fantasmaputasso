using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    GameObject ghostObject;
    MovementComponent moveComponent;
    new Renderer renderer;
    Collider col;
    GameObject targetObject;
    BreakableItem possessedItem;

    public string possessItemTag = "PossessItem";

    void Start()
    {
        ghostObject = gameObject;
        moveComponent = ghostObject.GetComponent<MovementComponent>();
        col = ghostObject.GetComponent<Collider>();
        renderer = ghostObject.GetComponent<Renderer>();
    }

    void AssignNewTarget()
    {
        GameObject[] possessables = GameObject.FindGameObjectsWithTag(possessItemTag);
        if(possessables.Length > 0)
        {
            targetObject = possessables[Random.Range(0, possessables.Length)];
        }
    }

    void Possess(GameObject otherObj)
    {
        targetObject = otherObj;
        possessedItem = otherObj.GetComponent<BreakableItem>();
        moveComponent = otherObj.GetComponent<MovementComponent>();
        possessedItem.onBreak.AddListener(UnPossess);
        
        EnableCollisionAndRender(false);
    }

    void EnableCollisionAndRender(bool enabled)
    {
        col.enabled = enabled;
        renderer.enabled = enabled;
    }

    void UnPossess()
    {
        possessedItem.onBreak.RemoveListener(UnPossess);
        ghostObject.transform.position = possessedItem.transform.position;
        possessedItem = null;
        targetObject = null;
        moveComponent = ghostObject.GetComponent<MovementComponent>();

        EnableCollisionAndRender(true);
    }

    void PossessedUpdate()
    {
        moveComponent.MovementInput = possessedItem.startingForward;
    }

    void RegularUpdate()
    {
        if(targetObject == null)
        {
            AssignNewTarget();
            return;
        }

        moveComponent.MovementInput = targetObject.transform.position - transform.position;
    }

    void Update()
    {
       if(possessedItem == null)
       {
           RegularUpdate();
       }
       else
       {
           PossessedUpdate();
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == targetObject)
        {
            Possess(other.gameObject);
        }
    }

}

