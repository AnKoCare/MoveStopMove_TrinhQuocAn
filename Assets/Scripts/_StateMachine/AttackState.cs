using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour
{
    public void OnEnter(Character t)
    {
        t.OnAttackEnter();
    }

    public void OnExecute(Character t)
    {
        t.OnAttackExecute();
    }

    public void OnExit(Character t)
    {
        t.OnAttackExit();
    }
}
