using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Character>
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
