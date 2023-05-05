using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{ 
    [SerializeField] private Character character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character") && !character.IsDead)
        {
            Character chars = other.GetComponent<Character>();
            if (chars == character) return;
            if(chars.IsDead) return;
            character.IsAttack = true;
            character.characterList.Add(chars);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Character") && character.AttackEnd && !character.IsDead)
        {
            Character chars = other.GetComponent<Character>();
            if(chars == character) return;
            if(chars.IsDead)
            {
                character.characterList.Remove(chars);
                return;
            }
            character.IsAttack = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Character") && !character.IsDead)
        {
            Character chars = other.GetComponent<Character>();
            if(chars == character) return;
            character.IsAttack = false;
            character.characterList.Remove(chars);
        }
    }

    public void ChangeRange()
    {
        gameObject.transform.localScale *= 1.2f;
    }
}
