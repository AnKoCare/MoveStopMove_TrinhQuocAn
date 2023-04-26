using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    public void OnEnter(Character t)
    {
        t.IsIdle = true;
    }

    public void OnExecute(Character t)
    {
        t.OnIdleExecute();
    }

    public void OnExit(Character t)
    {
        t.OnIdleExit();
    }
}
