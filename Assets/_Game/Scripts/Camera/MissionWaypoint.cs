using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionWaypoint : GameUnit
{
    public Character owner;
    public Image imgDir;
    public Transform target;
    public Vector3 offset;
    public Vector3 pos;
    public Vector2 originPoint;   // Điểm gốc toạ độ O

    // Update is called once per frame

    private void Start() 
    {
        originPoint = Vector2.zero;
    }

    void Update()
    {
        if(GameManager.Ins.IsState(GameState.MainMenu)) return;
        float minX = imgDir.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = imgDir.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        pos = Camera.main.WorldToScreenPoint(target.position + offset);
        


        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;
        imgDir.transform.position = Vector3.Lerp(pos, imgDir.transform.position, 0.5f);
        if(pos.x == 50 || pos.x == 1030 || pos.y == 50 || pos.y == 1870)
        {
            imgDir.enabled = true;
        }
        else
        {
            imgDir.enabled = false;
        }

        Vector2 anglePos = Vector2.up * pos.y + Vector2.right * pos.x;

        float angle = CalculateAngle(anglePos);

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        TF.localRotation =  Quaternion.Lerp(rotation, TF.localRotation, 0.9f);

    }

    private float CalculateAngle(Vector2 point)
    {
        float x = (float)(point.x - 540)/490;
        float y = (float)(point.y - 960)/910;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return angle;
    }

    public override void OnDespawn()
    {

    }

    public override void OnInit()
    {

    }

    public void OnInit(Character character)
    {
        owner = character;
    }

    public void Setoffset(float size)
    {
        offset += Vector3.up * size;
    }
}
