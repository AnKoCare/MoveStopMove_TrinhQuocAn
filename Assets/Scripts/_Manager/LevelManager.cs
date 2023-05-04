using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Weapon knife;
    public Bot BotPrefabs;
    private int maxBot = 20;

    private void Start() 
    {
        for(int i = 0; i < maxBot; i++)
        {
            Bot bots = Instantiate(BotPrefabs);
            bots.name = "Bot " + i;
            float x = Random.Range(-47f,47f);
            float y = Random.Range(-47f,47f);
            bots.transform.position = Vector3.up * 1.58f + Vector3.forward * y + Vector3.right * x;
        }
    }
}
