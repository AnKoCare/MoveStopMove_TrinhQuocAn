using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public void ChangeRange()
    {
        gameObject.transform.localScale *= 1.2f;
    }
}
