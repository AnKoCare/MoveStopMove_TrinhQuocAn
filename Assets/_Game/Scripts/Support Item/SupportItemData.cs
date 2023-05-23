using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SupportItemObject", order = 4)]
public class SupportItemData : ScriptableObject
{
    [SerializeField] SupportsItem[] supportsItems;


    public SupportsItem GetSupportItem(SupportsType type)
    {
        return supportsItems[(int)type];
    }
}

[System.Serializable]
public class SupportsItem
{
    public SupportItem SupportItem;
    public string NameSupportItem;
    public float Gold;
    public string Description;
    public float Price;
    public bool IsUnlocked;
    public bool IsEquipped;
}

public enum SupportsType
{
    NormalShield = 0,
    CaptainShield = 1,
    EmptyShield = 2
}
