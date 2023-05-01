using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed; // Tốc độ di chuyển của knife

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Character"))
        {
            Destroy(gameObject);
            //Destroy(other.gameObject);
        }
    }
}
