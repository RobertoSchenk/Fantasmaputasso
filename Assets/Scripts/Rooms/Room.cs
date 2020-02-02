using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WallStruct
{
    public bool isDoor;
    public GameObject parentObj;
}

public class Room : MonoBehaviour
{
    public int roomId;
    public Renderer InvisWall;

    public WallStruct wallS;
    public WallStruct wallW;
    public WallStruct wallE;
    public WallStruct wallN;

    public GameObject player;
    public GameObject camera;
    void Start()
    {
        UpdateWallStatus(wallS);
       UpdateWallStatus(wallE);
       UpdateWallStatus(wallW);
       UpdateWallStatus(wallN);

    }
    
    void UpdateWallStatus(WallStruct wallStruct)
    {
        if(wallStruct.parentObj != null)
        {
            Destroy(wallStruct.parentObj.transform.GetChild(wallStruct.isDoor ? 0 : 1).gameObject);
        }
    }
   
}
