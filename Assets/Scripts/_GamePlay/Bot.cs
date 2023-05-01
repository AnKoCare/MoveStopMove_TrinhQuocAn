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
    private float timerAttack = 0f; // biến đếm thời gian chạy animation tấn công
    private float timerPatrol = 0f; // biến đếm thời gian delay tấn công
    private float randomTimeAttack; // biến random thời gian delay tấn công
    private float duration; // biến lưu thời gian chạy của animation Attack

    public override void Start()
    {
        base.Start();
        duration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Attack")?.length ?? 0;
    }

    private void Update() 
    {
        if(character.currentState != null && !character.IsDead)
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
            // Tìm vị trí ngẫu nhiên trên NavMesh
            targetPosition = RandomNavSphere(transform.position, searchRadius, navMeshAgent.areaMask);

            // Đặt đích cho AI di chuyển đến vị trí đó
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
        IsThrow = true;
    }

    public override void OnAttackExecute()
    {
        base.OnAttackExecute();
        ChangeAnim("Attack");
        timerAttack += Time.deltaTime; // cộng thêm thời gian đã trôi qua

        if (timerAttack >= duration) 
        {
            // thời gian đếm đã đủ, trả về true
            AttackEnd = true;
            timerAttack = 0f;
            ChangeState(new IdleState()); 
        }
        else
        {
            AttackEnd = false;
            return;
        }
    }

    public override void OnAttackExit()
    {
        base.OnAttackExit();
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
