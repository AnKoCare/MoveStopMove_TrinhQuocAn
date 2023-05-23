using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NumberBot;

    void Update()
    {
        NumberBot.text = "Alive: " +  (LevelManager.Ins.maxBot + 1);
    }
}
