using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Weapon knife;
    public Bot BotPrefabs;
    public int maxBot = 20;
    private Level currentLevel;
    private int levelIndex;
    public Level[] levelPrefabs;
    public PlayerController player;


    public void Start() 
    {
        // levelIndex = PlayerPrefs.GetInt("Level", 0);
        // LoadLevel(levelIndex);
        // OnInit();

        for(int i = 0; i < maxBot; i++)
        {
            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            bots.name = "Bot " + i;
            float x = Random.Range(-47f,47f);
            float y = Random.Range(-47f,47f);
            bots.transform.position = Vector3.up * 1.58f + Vector3.forward * y + Vector3.right * x;
        }
    }

    // public override void OnInit()
    // {
    //     //update navmesh data
    //     NavMesh.RemoveAllNavMeshData();
    //     NavMesh.AddNavMeshData(currentLevel.navMeshData);

    //     //Set vi tri player

    // }

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
