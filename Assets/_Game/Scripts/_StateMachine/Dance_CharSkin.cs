using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance_CharSkin : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.OnDance_CharSkinEnter();
    }

    public void OnExecute(Character t)
    {
        t.OnDance_CharSkinExecute();
    }

    public void OnExit(Character t)
    {
        t.OnDance_CharSkinExit();
    }
}
