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

    bool canPickupNewTarget = true;
    public GameObject[] waypoints;

    public float waitingTime = 3;
    float _currentWait = 0;

    public bool combo = true;
    void Start()
    {
        ghostObject = gameObject;
        moveComponent = ghostObject.GetComponent<MovementComponent>();
        col = ghostObject.GetComponent<Collider>();
        renderer = ghostObject.GetComponent<Renderer>();
    }

    void AssignNewTarget()
    {
        GameObject[] possessables = FindPossessables();
        if(possessables.Length > 0)
        {
            targetObject = possessables[Random.Range(0, possessables.Length)];
            ghostObject.layer = LayerMask.NameToLayer("GhostNoCol");
        }
        else
        {
            canPickupNewTarget = false;
        }
    }

    GameObject[] FindPossessables()
    {
        return GameObject.FindGameObjectsWithTag(possessItemTag);
    }

    GameObject[] FindWaypoints()
    {
        return GameObject.FindGameObjectsWithTag("Waypoint");
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
        canPickupNewTarget = combo;

        EnableCollisionAndRender(true);
    }

    void PossessedUpdate()
    {
        moveComponent.MovementInput = possessedItem.startingForward;
    }

    void ChasingPossessUpdate()
    {
        if(targetObject == null)
        {
            AssignNewTarget();
            return;
        }

        if(Vector3.Distance(targetObject.transform.position, ghostObject.transform.position) >= 0.01f)
        {
            moveComponent.MovementInput = targetObject.transform.position - transform.position;
        }


        if(targetObject.transform.position.sqrMagnitude - transform.position.sqrMagnitude <= 3f * 3f)
        {
            ghostObject.layer = LayerMask.NameToLayer("Ghost");
        }
    }

    Vector3 DirectionToTargetObject()
    {
        if(targetObject == null)
        {
            return Vector3.zero;
        }

        return (targetObject.transform.position - ghostObject.transform.position).normalized;
    }

    void Update()
    {
        if(_currentWait > 0)
        {
            _currentWait = Mathf.Clamp(_currentWait - Time.deltaTime, 0.0f, 99999.0f);
            return;
        }

       if(possessedItem == null && !canPickupNewTarget)
       {
           if(targetObject == null)
           {
               GameObject[] waypoints =FindWaypoints();
               if(waypoints.Length > 0)
               {
                   targetObject = waypoints[Random.Range(0, waypoints.Length)];
               }
           }

            if(Vector3.Distance(targetObject.transform.position, ghostObject.transform.position) >= 1)
            {
                moveComponent.MovementInput = DirectionToTargetObject();
            }
            else if(_currentWait == 0)
            {
                _currentWait = waitingTime;
                targetObject = null;
                canPickupNewTarget = true;
            }
           return;
       }

       if(possessedItem == null )
       {
           ChasingPossessUpdate();
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

