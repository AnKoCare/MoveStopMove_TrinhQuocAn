using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CvGameplay : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CameraFollow.Ins.SetupGamePlay();
    }
}
