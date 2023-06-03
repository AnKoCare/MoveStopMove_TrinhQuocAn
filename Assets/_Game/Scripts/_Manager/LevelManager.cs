using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class LevelManager : Singleton<LevelManager>
{
    public Bot BotPrefabs;
    public int maxBot;
    public int currentBot;
    public ColorData colorDataManager;
    [SerializeField] private Level currentLevel;
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
    private NavMeshHit navHit;
    public List<string> botNames; // Danh sách tên của bot
    public List<string> usedNames = new List<string>(); // Danh sách các tên đã được sử dụng
    public GameObject BotHold;
    public GameObject MissionWayPoint1Hold;
    public GameObject MissionWayPoint2Hold;
    



    private void Update() 
    {
        if(currentBot <= 10 && maxBot >= 11)
        {
            SpawnBot(10, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
            currentBot = 20;
        }
    }

    public override void OnInit()
    {
        currentData = DataManager[levelIndex];

        maxBot = currentData.CountEnemy;
        currentLevel = Instantiate(Resources.Load<Level>("Level/Ground_" + levelIndex));

        Map =  currentLevel._renderer.bounds;

        xMin = Map.min.x;
        zMin = Map.min.z;

        xMax = Map.max.x;
        zMax = Map.max.z;
        currentBot = 20;
        
    }

    private void Start() 
    {
        SpawnBot(currentBot, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
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

    private void SpawnBot(int numberBot, float sizeCharacter, float sizeRing, int levelCharacter, float speed)
    {
        for(int i = 0; i < numberBot; i ++)
        {
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);

            Vector3 posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
            Vector3 navPos = Vector3.zero;
            bool checkPos = false; 

            while(!checkPos)
            {
                if(NavMesh.SamplePosition(posBot, out navHit ,5f, NavMesh.AllAreas))
                {
                    navPos = navHit.position;
                    if(Vector3.Distance(player.TF.position, navPos) > 15f)
                    {
                        checkPos = true;
                    }
                    else
                    {
                        x = Random.Range(xMin, xMax);
                        z = Random.Range(zMin, zMax);
                        posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
                    }
                }
                else
                {
                    x = Random.Range(xMin, xMax);
                    z = Random.Range(zMin, zMax);
                    posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
                }   
            }

            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            bots.sizeCharacter = sizeCharacter;
            bots.sizeRing = sizeRing;
            bots.LevelCharacter = levelCharacter;
            bots.moveSpeed = speed;
            bots.isDead = false;
            bots.ChangeState(new IdleState());
            bots.boxCollider.enabled = true;
            
            if(navPos != Vector3.zero)
            {
                bots.transform.position = navPos;
            }
            
        }
    }

    public void SpawnNotice(string killer, string victim)
    {
        UIManager.Ins.GetUI<CvGameplay>(UIID.Gameplay).SpawnNotice(killer,victim);
    }

    public void ReloadGame()
    {
        usedNames.Clear();

        Bot[] bots = BotHold.GetComponentsInChildren<Bot>();
        if(bots.Length > 0)
        {
            for(int i = 0; i < bots.Length; i ++)
            {
                Debug.Log("despawnBot");
                bots[i].characterList.Clear();
                SimplePool.Despawn(bots[i]);
            }
        }

        MissionWaypoint[] missionWaypoints = MissionWayPoint1Hold.GetComponentsInChildren<MissionWaypoint>();
        if(missionWaypoints.Length > 0)
        {
            for(int i = 0; i < missionWaypoints.Length; i++)
            {
                Debug.Log("despawnMisswaypoint1");
                SimplePool.Despawn(missionWaypoints[i]);
            }
        }

        MissionWayPoint2[] missionWaypoints2 = MissionWayPoint2Hold.GetComponentsInChildren<MissionWayPoint2>();
        if(missionWaypoints2.Length > 0)
        {
            for(int i = 0; i < missionWaypoints2.Length; i++)
            {
                Debug.Log("despawnMisswaypoint2");
                SimplePool.Despawn(missionWaypoints2[i]);
            }
        }
        
        
        player.isAttack = false;
        player.isDance = false;
        player.isDead = false;
        player.isIdle = true;
        player.isPatrol = false;
        player.isThrow = false;
        player.timerAttack = 0f;
        player.boxCollider.enabled = true;
        player.AttackEnd = true;
        player.characterList.Clear();
        
        player.ChangeState(new IdleState());

        player.TF.position = Vector3.up * 1.5f;
        player.LevelCharacter = 1;
        player.sizeCharacter = 1;
        player.SetSizeChar(player.sizeCharacter);
        player.SetSizeRing(player.sizeRing);

        player.SetUpPantIndicator();
        player.SetUpSupportItemIndicator();
        player.SetUpWeaponAndHairIndicator();
        player.numberKillBot = 0;

        player.gameObject.SetActive(true);

        Destroy(currentLevel);
        OnInit();
        SpawnBot(currentBot, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
    }

}
