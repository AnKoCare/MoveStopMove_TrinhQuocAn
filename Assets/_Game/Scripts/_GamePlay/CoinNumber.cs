using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinNumber : MonoBehaviour
{
    public TextMeshProUGUI NumberCoin;

    void Update()
    {
        NumberCoin.text = "" + LevelManager.Ins.player.coin;
    }
}
