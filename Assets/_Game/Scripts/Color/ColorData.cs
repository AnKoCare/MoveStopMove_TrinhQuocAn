using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorsObject", order = 7)]
public class ColorData : ScriptableObject
{
    [SerializeField] ColorsItem[] colorsItems;


    public ColorsItem GetColor(ColorsType type)
    {
        return colorsItems[(int)type];
    }
}

[System.Serializable]
public class ColorsItem
{
    public Material Color;
}

public enum ColorsType
{
    Red = 0, 
    Violet = 1,
    Orange = 2,
    Dark = 3,
    Blue = 4,
    Green = 5,
    Yellow = 6,
    White = 7,
    Pink = 8,
    Transparent = 9
}
