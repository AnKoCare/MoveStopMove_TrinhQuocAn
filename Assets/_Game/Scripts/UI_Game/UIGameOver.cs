using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameOver : UICanvas
{
    public TextMeshProUGUI rankPlayer;
    public TextMeshProUGUI nameKiller;
    public TextMeshProUGUI earnCoin;

    public override void Setup()
    {
        nameKiller.text = LevelManager.Ins.player.nameKiller;
        rankPlayer.text = "#" + LevelManager.Ins.player.rankPlayer; 
        earnCoin.text = "" + LevelManager.Ins.player.numberKillBot * LevelManager.Ins.player.rateUpGold;
    }

    public void ButtonContinue()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
        LevelManager.Ins.ReloadGame();
    }
}
