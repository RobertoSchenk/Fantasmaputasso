using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public DoorController door;
    public int id;
    
   
    private void OnTriggerEnter(Collider other)
    {
        
        GameEvents.current.DoorwayTriggerEnter(id);
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.DoorwayTriggerExit(id);
    }
}
