using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{ 
    [SerializeField] private Character character;
    public float speed = 5f; // Tốc độ di chuyển của knife

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character") && gameObject.transform.root != other.transform.root)
        {
            Character chars = other.GetComponent<Character>();
            character.IsAttack = true;
            character.characterList.Add(chars);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Character") && gameObject.transform.root != other.transform.root && character.AttackEnd)
        {
            character.IsAttack = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Character") && gameObject.transform.root != other.transform.root)
        {
            Character chars = other.GetComponent<Character>();
            character.IsAttack = false;
            character.characterList.Remove(chars);
        }
    }

    public void ThrowWeapon(Bot bot)
    {
        // Weapon knife = Instantiate(LevelManager.Ins.knife, character.transform.position, Quaternion.identity);
        // //knife.transform.LookAt(bot.transform);
        // knife.transform.position = Vector3.MoveTowards(knife.transform.position, bot.transform.position, speed * Time.deltaTime );
    }

    public void ChangeRange()
    {
        gameObject.transform.localScale *= 1.2f;
    }
}
