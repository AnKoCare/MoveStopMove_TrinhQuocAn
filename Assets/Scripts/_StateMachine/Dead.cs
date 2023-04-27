using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.OnDeadEnter();
    }

    public void OnExecute(Character t)
    {
        t.OnDeadExecute();
    }

    public void OnExit(Character t)
    {
        t.OnDeadExit();
    }
}
