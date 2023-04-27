using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Animator anim;
    public List<Character> characterList;
    public bool AttackEnd = true; // bien neu chay het anim attack thi se tra ve true, neu chua chay het se tra ve false
    public bool IsIdle = false;
    public bool IsPatrol = false;
    public bool IsAttack = false;
    public bool IsDead = false;
    public bool IsDance = false;
    private int pos;
    protected string currentAnimName = "Idle";

    public IState<Character> currentState;

    public virtual void Start() 
    {
        OnInit();   
    }

    public virtual void OnInit()
    {

    }

    public void ChangeState(IState<Character> newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }


    //IDLE
    public virtual void OnIdleEnter()
    {
        IsIdle = true;
    }

    public virtual void OnIdleExecute()
    {
        ChangeAnim("Idle");
    }

    public virtual void OnIdleExit()
    {
        IsIdle = false;
    }


    //PATROL
    public virtual void OnPatrolEnter()
    {
        IsPatrol = true;
    }

    public virtual void OnPatrolExecute()
    {
        ChangeAnim("Patrol");
    }

    public virtual void OnPatrolExit()
    {
        IsPatrol = false;
    }


    //ATTACK
    public virtual void OnAttackEnter()
    {
        IsAttack = true;
        if(characterList.Count == 0)
        {
            return;
        }
        float disMin;
        for(int i = 0; i < characterList.Count - 1; i++)
        {
            disMin = DisChar(characterList[i]) < DisChar(characterList[i+1]) ? DisChar(characterList[i]) : DisChar(characterList[i+1]);
            if(disMin == DisChar(characterList[i]))
            {
                pos = i;
            }
            else if(disMin == DisChar(characterList[i+1]))
            {
                pos = i + 1;
            }
        }
        gameObject.transform.LookAt(characterList[pos].transform);
    }

    public virtual void OnAttackExecute()
    {
        
    }

    public virtual void OnAttackExit()
    {
        IsAttack = false;
    }


    //DEAD
    public virtual void OnDeadEnter()
    {
        IsDead = true;
    }

    public virtual void OnDeadExecute()
    {
        ChangeAnim("Dead");
    }   

    public virtual void OnDeadExit()
    {
        IsDead = false;
    }


    //DANCE_WIN_ENTER
    public virtual void OnDance_WinEnter()
    {
        IsDance = true;
    }

    public virtual void OnDance_WinExecute()
    {
        ChangeAnim("Dance");
    }

    public virtual void OnDance_WinExit()
    {
        IsDance = false;
    }

    public float DisChar(Character chars)
    {
       return Vector3.Distance(gameObject.transform.position ,chars.transform.position); 
    }

    
}
