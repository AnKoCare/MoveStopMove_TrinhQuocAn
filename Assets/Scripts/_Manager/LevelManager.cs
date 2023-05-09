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
    public float xMin;
    public float zMin;
    public float xMax;
    public float zMax;

    public void Start() 
    {
        // levelIndex = PlayerPrefs.GetInt("Level", 0);
        // LoadLevel(levelIndex);
        // OnInit();

        
    }

    private void Update() 
    {
        if(maxBot == MaxBotUI - 5 && maxBot >= 7)
        {
            SpawnBot();
            MaxBotUI = maxBot;
        }
    }

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

        for(int i = 0; i < 10; i++)
        {
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);

            while(Vector3.Distance(player.transform.position, Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x) < 15f)
            {
                x = Random.Range(xMin, xMax);
                z = Random.Range(zMin, zMax);
            }

            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            bots.name = "Bot " + i;
            
            bots.transform.position = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
        }

        //update navmesh data
        // NavMesh.RemoveAllNavMeshData();
        // NavMesh.AddNavMeshData(currentLevel.navMeshData);

        //Set vi tri player

    }

    public void SpawnBot()
    {
        for(int i = 0; i < 5; i++)
        {
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);

            while(Vector3.Distance(player.transform.position, Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x) < 15f)
            {
                x = Random.Range(xMin, xMax);
                z = Random.Range(zMin, zMax);
            }

            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            bots.name = "Bot " + i;
            
            bots.transform.position = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
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
