using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public Character Owner;

    public override void OnInit()
    {

    }

    public void OnInit(Character character) 
    {
        Owner = character;
    }

    public override void OnDespawn()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            Owner.ScaleUp(0.5f);
            character.ChangeState(new Dead());
            OnDespawn();
            SimplePool.Despawn(this);
        }
        if(other.CompareTag("Obstacle"))
        {
            OnDespawn();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Ring"))
        {
            OnDespawn();
            SimplePool.Despawn(this);
        }
    }
}
