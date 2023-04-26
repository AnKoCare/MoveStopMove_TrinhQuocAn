using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Animator anim;
    public bool IsIdle;
    public bool IsAttack;
    protected string currentAnimName = "Idle";

    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("Dead");
    }

    public virtual void OnIdleEnter()
    {

    }

    public virtual void OnIdleExecute()
    {

    }

    public virtual void OnIdleExit()
    {
        
    }

    public virtual void OnPatrolEnter()
    {
        
    }

    public virtual void OnPatrolExecute()
    {

    }

    public virtual void OnPatrolExit()
    {

    }

    public virtual void OnAttackEnter()
    {
        
    }

    public virtual void OnAttackExecute()
    {

    }

    public virtual void OnAttackExit()
    {

    }

    public virtual void OnDeadEnter()
    {
        
    }

    public virtual void OnDeadExecute()
    {

    }

    public virtual void OnDeadExit()
    {

    }

    public virtual void OnDance_WinEnter()
    {
        
    }

    public virtual void OnDance_WinExecute()
    {

    }

    public virtual void OnDance_WinExit()
    {

    }
}
