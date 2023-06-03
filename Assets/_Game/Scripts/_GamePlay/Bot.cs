using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Character character;
    private float searchRadius = 10f;
    private float timerPatrol = 0f; // biến đếm thời gian delay tấn công
    private float randomTimeAttack; // biến random thời gian delay tấn công

    public override void Start()
    {
        base.Start();
        randomTimeAttack = UnityEngine.Random.Range(0.5f, 1.5f);
        
    }

    private void Update() 
    {
        if(GameManager.Ins.IsState(GameState.Gameplay) &&  character.currentState != null)
        {
            character.currentState.OnExecute(this);
        }  
    }

    public override void OnInit()
    {
        base.OnInit();

        WeaponType[] weaponTypesValues =  (WeaponType[])Enum.GetValues(typeof(WeaponType));
        WeaponType weaponTypeBot = weaponTypesValues[UnityEngine.Random.Range(0,weaponTypesValues.Length)];
        weaponType = weaponTypeBot;
        SetWeapon(weaponTypeBot);

        PantsType[] pantsTypesValues = (PantsType[])Enum.GetValues(typeof(PantsType));
        PantsType pantsTypeBot = pantsTypesValues[UnityEngine.Random.Range(0,pantsTypesValues.Length)];
        pantsType = pantsTypeBot;
        SetPant(pantsTypeBot);

        HairsType[] hairsTypesValues = (HairsType[])Enum.GetValues(typeof(HairsType));
        HairsType hairsTypeBot = hairsTypesValues[UnityEngine.Random.Range(0,hairsTypesValues.Length)];
        hairsType = hairsTypeBot;
        SetHair(hairsTypeBot);

        SupportsType[] supportsTypesValues = (SupportsType[])Enum.GetValues(typeof(SupportsType));
        SupportsType supportsTypeBot = supportsTypesValues[UnityEngine.Random.Range(0,supportsTypesValues.Length)];
        supportsType = supportsTypeBot;
        SetSupportItem(supportsTypeBot);

        ColorsType[] colorsTypesValues = (ColorsType[])Enum.GetValues(typeof(ColorsType));
        ColorsType colorsTypeBot = colorsTypesValues[UnityEngine.Random.Range(0,colorsTypesValues.Length)];
        colorsTypeChar = colorsTypeBot;
        SetColorBot(colorsTypeChar);

        nameChar = GetRandomBotName();

        navMeshAgent.speed = moveSpeed;

        ScaleUp();

        ChangeState(new PatrolState());
    }


    //IDLE
    public override void OnIdleEnter()
    {
        base.OnIdleEnter();
    }

    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
        if(UnityEngine.Random.Range(0f, 1f) < 0.7f && characterList.Count > 0)
        {
            randomTimeAttack = UnityEngine.Random.Range(0.5f, 1.5f);
            ChangeState(new AttackState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        
    }

    public override void OnIdleExit()
    {
        base.OnIdleExit();
    }


    //PATROL
    public override void OnPatrolEnter()
    {
        base.OnPatrolEnter();
        targetPosition = transform.position;
    }

    public override void OnPatrolExecute()
    {
        base.OnPatrolExecute();
        if(characterList.Count > 0)
        {
            timerPatrol += Time.deltaTime; // cộng thêm thời gian đã trôi qua
            if (timerPatrol >= randomTimeAttack) 
            {
                navMeshAgent.SetDestination(transform.position);
                timerPatrol = 0;
                ChangeState(new IdleState()); 
            }
        }

        if (Vector3.Distance(targetPosition, transform.position)< 1.2f)
        {
            targetPosition = RandomNavSphere(transform.position, searchRadius, navMeshAgent.areaMask);

            while(targetPosition.Equals(Vector3.positiveInfinity))
            {
                targetPosition = RandomNavSphere(transform.position, searchRadius, navMeshAgent.areaMask);
            }

            navMeshAgent.SetDestination(targetPosition);
        }
    }

    public override void OnPatrolExit()
    {
        base.OnPatrolExit();
        
    }


    //ATTACK
    public override void OnAttackEnter()
    {
        base.OnAttackEnter();
        timerAttack = 0;
        WeaponModel.gameObject.SetActive(false);
    }

    public override void OnAttackExecute()
    {
        if (CountThrow == 0)
        {
            base.OnAttackExecute();
            ChangeAnim("Attack");
            CountThrow ++;
        }

        if(timerAttack >= (0.3f - (float)LevelManager.Ins.player.weaponData.GetWeapon(LevelManager.Ins.player.weaponType).attackSpeed / 10) * duration && CountThrow == 1)
        {
            isThrow = true;
            ThrowWeapon();
            isThrow = false;
            CountThrow ++;
        }

        if (timerAttack >= duration) 
        {
            ChangeState(new IdleState()); 
        }

        timerAttack += Time.deltaTime; // cộng thêm thời gian đã trôi qua
    }

    public override void OnAttackExit()
    {
        base.OnAttackExit();
        timerAttack = 0f;
        CountThrow = 0;
        WeaponModel.gameObject.SetActive(true);
    }


    //DEAD
    public override void OnDeadEnter()
    {
        base.OnDeadEnter();
        navMeshAgent.SetDestination(gameObject.transform.position);
        
    }

    public override void OnDeadExecute()
    {
        base.OnDeadExecute();
    }

    public override void OnDeadExit()
    {
        base.OnDeadExit();
    }

    public void SetMoveSpeedBot()
    {
        navMeshAgent.speed = moveSpeed;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
