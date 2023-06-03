using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : UICanvas
{
    public TMP_InputField InputField;
    public TextMeshProUGUI InputName;
    public string PlayerName;

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.SetupMainMenu();
        LevelManager.Ins.player.ChangeState(new IdleState());
        InputField.onEndEdit.AddListener(SetName);
    }

    public void SetName(string text)
    {
        PlayerName = InputName.text;
        LevelManager.Ins.player.nameChar = PlayerName;
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
        LevelManager.Ins.player.nameChar = PlayerName;
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}
