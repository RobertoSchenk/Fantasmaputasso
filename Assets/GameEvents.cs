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

    public float EndGameTimer;
    
    public static bool gameRunning;

    public UnityEvent onGameFinished;
    
    private void Awake()
    {
        current = this;
       
    }

    void Start()
    {
        gameRunning = false;
        CurrentPoints = 0;
        StartGame();
    }

    public static void StartGame()
    {
        gameRunning = true;
    }


    public static void EndGame()
    {
        current.StartCoroutine(current.WaitEndGame(current.EndGameTimer));
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
        items--;
        
        if(current.onPointsChanged != null)
        {
            current.onPointsChanged.Invoke(CurrentPoints);
        }

        if(items <= 0)
        {
            EndGame();
        }
        
    }

    IEnumerator WaitEndGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);


        gameRunning = false;
        if(onGameFinished != null)
        {
            onGameFinished.Invoke();
        }
    }

    static int items = 0;
    public static void RegisterItem()
    {
        items++;
    }


}
