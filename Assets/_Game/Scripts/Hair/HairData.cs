using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HairsObject", order = 4)]
public class HairData : ScriptableObject
{
    [SerializeField] HairsItem[] hairItems;


    public HairsItem GetHair(HairsType type)
    {
        return hairItems[(int)type];
    }
}

[System.Serializable]
public class HairsItem
{
    public Hair HairItem;
    public string NameHair;
    public float Range;
    public float Price;
    public string Description;
    public bool IsUnlocked;
    public bool IsEquipped;
}

public enum HairsType
{
    Arrow = 0,
    Cowboy = 1,
    Crown = 2,
    Ear = 3,
    Hat = 4,
    Hat_Cap = 5,
    Hat_Yellow = 6,
    HearPhone = 7,
    Horn = 8,
    Rau = 9,
    EmptyHair = 10
}
