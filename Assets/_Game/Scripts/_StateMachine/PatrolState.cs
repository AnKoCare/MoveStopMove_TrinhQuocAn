using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.OnPatrolEnter();
    }

    public void OnExecute(Character t)
    { 
        t.OnPatrolExecute();
    }

    public void OnExit(Character t)
    {
        t.OnPatrolExit();
    }

}
