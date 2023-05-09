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
    public float searchRadius = 10f;
    private float timerPatrol = 0f; // biến đếm thời gian delay tấn công
    private float randomTimeAttack; // biến random thời gian delay tấn công

    public override void Start()
    {
        base.Start();
        randomTimeAttack = UnityEngine.Random.Range(0.5f, 1.5f);
    }

    private void Update() 
    {
        if(character.currentState != null)
        {
            character.currentState.OnExecute(this);
        }  
    }

    public override void OnInit()
    {
        base.OnInit();
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
        if(character.IsAttack)
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
        if(character.IsAttack)
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
        AttackEnd = false;
        CountThrow = 0;
        WeaponModel.gameObject.SetActive(false);
    }

    public override void OnAttackExecute()
    {
        base.OnAttackExecute();
        ChangeAnim("Attack");
        timerAttack += Time.deltaTime; // cộng thêm thời gian đã trôi qua

        if (timerAttack >= duration) 
        {
            ChangeState(new IdleState()); 
        }
        else if(timerAttack >= 0.3f * duration && CountThrow == 0)
        {
            IsThrow = true;
            CountThrow++;
        }
    }

    public override void OnAttackExit()
    {
        base.OnAttackExit();
        timerAttack = 0f;
        AttackEnd = true;
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

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
