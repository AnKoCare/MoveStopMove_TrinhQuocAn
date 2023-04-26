using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
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
