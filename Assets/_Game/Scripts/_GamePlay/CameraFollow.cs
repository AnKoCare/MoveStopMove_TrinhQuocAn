using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private GameObject player;       

    readonly Vector3 orriginOffset = new Vector3(0,1,-1);

    [SerializeField] private Vector3 offset;          
    
    void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
    }

    public void SetupMainMenu()
    {
        offset = new Vector3(0,4,8);
        transform.localRotation = Quaternion.Euler(30f,-180f,0);
    }

    public void SetupGamePlay()
    {
        offset = new Vector3(0,16,-16);
        transform.localRotation = Quaternion.Euler(40f,0,0);
    }

    public void SetupSuitShop()
    {
        offset = new Vector3(0,2,10);
        transform.localRotation = Quaternion.Euler(30f,-180f,0);
    }

    public void SetUpWhenKill(float dis)
    {
        offset += new Vector3(0,dis,-dis);
    }
}
