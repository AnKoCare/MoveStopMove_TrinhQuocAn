using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_Win : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.OnDance_WinEnter();
    }

    public void OnExecute(Character t)
    {
        t.OnDance_WinExecute();
    }

    public void OnExit(Character t)
    {
        t.OnDance_WinExit();
    }
}
