using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PointEvent : UnityEvent<int, Vector3>
{

}

[Serializable]
public class IntEvent : UnityEvent<int>
{

}

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public static int CurrentPoints =0;
    
    private void Awake()
    {
        current = this;
    }


    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> onDoorwayTriggerExit;
    public void DoorwayTriggerExit(int id)
    {

        if (onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    }

    public static int GetPointsPerTime(float time)
    {
        if(time < 0.5f)
        {
            return GetMaxpoints();
        }
        if(time < 1f)
        {
            return 750;
        }
        if(time < 1.5f)
        {
            return 500;
        }
        if(time < 2.5f)
        {
            return 300;
        }
        if(time < 3f)
        {
            return 100;
        }

        return 50;
    }

    public static int GetMaxpoints()
    {
        return 1000;
    }
    public PointEvent onItemRepaired;
    public IntEvent onPointsChanged;
    public static void ItemRepaired(int points, Vector3 position)
    {
        if(current.onItemRepaired != null)
        {
            current.onItemRepaired.Invoke(points,position);
        }

        CurrentPoints += points;
        
        if(current.onPointsChanged != null)
        {
            current.onPointsChanged.Invoke(CurrentPoints);
        }
        
    }
}
