using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelObject", order = 1)]
public class LevelData : ScriptableObject
{
   public int CountEnemy;
   public int TypeGround;
   public GameObject Table;
   public GameObject Cup;
   public List<Weapon> weapons;
}
