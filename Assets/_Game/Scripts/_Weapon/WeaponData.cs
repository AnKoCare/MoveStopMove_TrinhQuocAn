using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponObject", order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField] WeaponItem[] weapons;


    public WeaponItem GetWeapon(WeaponType type)
    {
        return weapons[(int)type];
    }
}

[System.Serializable]
public class WeaponItem
{
    public WeaponModel WeaponModel;
    public Weapon Weapons;
    public bool IsUnlocked;
    public Sprite WeaponImage;
    public string WeaponName;
    public string description;
    public float Speed;
    public float Range;
    public float attackSpeed;
    public float Prices;
    public bool goBack;
    public bool rotate;
    public bool IsEquipped;
}

public enum WeaponType
{
    Axe_0 = 0,
    Axe_1 = 1,
    Bommerang = 2,
    Knife = 3,
    Candy_0 = 4,
    Candy_1 = 5, 
    Candy_2 = 6,
    Candy_4 = 7,
    Hammer = 8
}