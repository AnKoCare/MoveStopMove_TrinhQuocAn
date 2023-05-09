using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotNumber : MonoBehaviour
{
    public TextMeshProUGUI NumberBot;

    void Update()
    {
        NumberBot = GetComponent<TextMeshProUGUI>();
        NumberBot.text = "Alive: " +  (LevelManager.Ins.maxBot + 1);
    }
}
