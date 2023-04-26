using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeBot : AttackRange
{
    [SerializeField] private Character character;
    public float speed = 5f; // Tốc độ di chuyển của knife

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            character.IsAttack = true;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!character.IsIdle) return;
            if(!character.IsAttack) return;
            character.transform.LookAt(other.transform);
            character.ChangeAnim("Attack");
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            character.IsAttack = false;
        }
    }

    public void ThrowWeapon(Bot bot)
    {
        Weapon knife = Instantiate(LevelManager.Ins.knife, character.transform.position, Quaternion.identity);
        //knife.transform.LookAt(bot.transform);
        knife.transform.position = Vector3.MoveTowards(knife.transform.position, bot.transform.position, speed * Time.deltaTime );
    }

    public void ResetAttack()
    {
        character.IsAttack = true;
    }
}
