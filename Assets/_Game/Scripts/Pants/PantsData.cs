using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PantsObject", order = 3)]
public class PantsData : ScriptableObject
{
    [SerializeField] PantsItem[] pantsItems;


    public PantsItem GetPants(PantsType type)
    {
        return pantsItems[(int)type];
    }
}

[System.Serializable]
public class PantsItem
{
    public Material Pant;
    public string NamePant;
    public float Speed;
    public string Description;
    public float Price;
    public bool IsUnlocked;
    public bool IsEquipped;
}

public enum PantsType
{
    Batman = 0,
    Chambi = 1,
    Comy = 2,
    Dabao = 3,
    Onion = 4,
    Pokemon = 5,
    Rainbow = 6,
    Skull = 7,
    Vantim = 8,
    EmptyPant = 9
}