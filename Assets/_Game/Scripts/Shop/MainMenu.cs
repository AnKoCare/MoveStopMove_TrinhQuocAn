using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.SetupMainMenu();
        LevelManager.Ins.player.ChangeState(new IdleState());
    }

    public override void SetDeActive()
    {
        base.SetDeActive();
    }

    public void ButtonWeapon()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.WeaponShop);
    }

    public void ButtonSkin()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.HatShop);
    }

    public void ButtonPlay()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}
