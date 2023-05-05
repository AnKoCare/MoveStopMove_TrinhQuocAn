using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{

    public override void OnInit()
    {

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
