using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject door;
    public int id;

    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
        id = Random.Range(0, 10000);
        GetComponentInChildren<TriggerArea>().id = id;
    }

    private void OnDoorwayClose(int id)
    {
        if (id != this.id)
        {
            return;
        }
        door.SetActive(true); //Trocar isso por fazer girar de volta e colocar a colisão
    }

    private void OnDoorwayOpen(int id)
    {
        if (id != this.id)
        {
            return;
        }
        door.SetActive(false);//trocar isso por tirar a colisao e fazer girar
    }


}
