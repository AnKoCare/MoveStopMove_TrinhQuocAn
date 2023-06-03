using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{ 
    [SerializeField] private Character character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_CHARACTER) && !character.isDead)
        {
            Character chars = Cache.GetCharacter(other);
            if (chars == character) return;
            if(chars.isDead) return;
            character.characterList.Add(chars);
            chars.onDespawnEvent += () => {character.characterList.Remove(chars);};
        }

        if(other.CompareTag(Constant.TAG_OBSTACLE) && character == LevelManager.Ins.player)
        {
            Obstacle obstacle = Cache.GetObstacle(other);
            obstacle.meshRendererObstacle.material = LevelManager.Ins.colorDataManager.GetColor(ColorsType.Transparent).Color;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag(Constant.TAG_CHARACTER) && !character.isDead)
        {
            Character chars = Cache.GetCharacter(other);
            if(chars == character) return;
            character.isAttack = false;
            character.characterList.Remove(chars);
        }

        if(other.CompareTag(Constant.TAG_OBSTACLE) && character == LevelManager.Ins.player)
        {
            Obstacle obstacle = Cache.GetObstacle(other);
            obstacle.meshRendererObstacle.material = obstacle.materialOrigin;
        }
    }

}
