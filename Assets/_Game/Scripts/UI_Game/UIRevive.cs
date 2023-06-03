using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRevive : UICanvas
{
    public void ButtonReviveAsCoin()
    {

    }

    public void ButtonExit()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.UIGameOver);
    }
}
