using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    public override void OnIdleEnter()
    {
        animator.SetBool("IsIdle", true);
    }

    public override void OnIdleExecute()
    {
        animator.SetBool("IsAttack", true);
    }

    public override void OnIdleExit()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsAttack", false);
    }

    public override void OnPatrolEnter()
    {
        
    }

    public override void OnPatrolExecute()
    {

    }

    public override void OnPatrolExit()
    {

    }
}
