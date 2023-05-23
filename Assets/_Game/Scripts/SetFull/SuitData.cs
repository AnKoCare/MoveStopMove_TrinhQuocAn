using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SuitObject", order = 5)]
public class SuitData : ScriptableObject
{
    [SerializeField] SuitItem[] suitItems;


    public SuitItem GetSuit(SuitType type)
    {
        return suitItems[(int)type];
    }
}

[System.Serializable]
public class SuitItem
{
    public Material CharColor;
    public Hair HairItem;
    public Tail TailItem;
    public Wing WingItem;
    public SupportItem supportItem;
    public string NameSuit;
    public float Speed;
    public string Description;
    public float Price;
    public bool IsUnlocked;
    public bool IsEquipped;
}

public enum SuitType
{
    Set1 = 0,
    Set2 = 1,
    Set3 = 2,
    EmptySuit = 3
}
