using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Weapon knife;
    public Bot BotPrefabs;
    public int maxBot;
    public int MaxBotUI;
    private Level currentLevel;
    private int levelIndex;
    private LevelData currentData;
    public Level[] levelPrefabs;
    public PlayerController player;
    public List<LevelData> DataManager;
    public Bounds Map;
    public GameObject Ground;
    private float xMin;
    private float zMin;
    private float xMax;
    private float zMax;

    public override void OnInit()
    {
        currentData = DataManager[levelIndex];

        maxBot = currentData.CountEnemy;
        currentLevel = Instantiate(Resources.Load<Level>("Level/Ground_" + levelIndex));

        MaxBotUI = maxBot;

        Map =  currentLevel._renderer.bounds;

        xMin = Map.min.x;
        zMin = Map.min.z;

        xMax = Map.max.x;
        zMax = Map.max.z;

        for(int i = 0; i < 30; i++)
        {
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);

            while(Vector3.Distance(player.TF.position, Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x) < 15f)
            {
                x = Random.Range(xMin, xMax);
                z = Random.Range(zMin, zMax);
            }

            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            bots.name = "Bot " + i;
            
            bots.TF.position = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
        }

    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
        }
        else
        {
            //TODO: level vuot qua limit
        }
    }
}
