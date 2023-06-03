using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CvVictory : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Pause);
    }
}
