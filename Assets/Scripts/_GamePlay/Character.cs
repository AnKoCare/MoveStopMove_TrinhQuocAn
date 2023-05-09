using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Character : GameUnit
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject ModelCharacter;

    public List<Character> characterList;
    public bool AttackEnd = true; // bien neu chay het anim attack thi se tra ve true, neu chua chay het se tra ve false
    public bool IsIdle = false; // biến kiểm tra Idle
    public bool IsPatrol = false; // biến kiểm tra Patrol
    public bool IsAttack = false; // biến kiểm tra Attack

    public bool IsDead = false; // biến kiểm tra Dead
    public bool IsDance = false; // biến kiểm tra Dance
    public bool IsThrow = false; // biến kiểm tra ném Weapon
    public float timerAttack = 0f; // biến đếm thời gian chạy animation tấn công
    public float duration; // biến lưu thời gian chạy của animation Attack
    public MeshRenderer WeaponModel;
    public GameObject ThrowPoint;
    private float throwForce = 8f;
    private int pos;
    public int CountThrow;

    protected string currentAnimName = "Idle";

    public IState<Character> currentState;

    public virtual void Start() 
    {
        OnInit();   
        duration = anim.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Attack")?.length ?? 0;
    }

    public virtual void FixedUpdate()
    {
        if(IsThrow)
        {
            ThrowWeapon();
            IsThrow = false;
        }
    }

    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }

    internal void ScaleUp(float size)
    {
        attackRange.transform.localScale += new Vector3(size, 0f, size);
        ModelCharacter.transform.localScale += new Vector3(size * 0.5f,size * 0.5f,size * 0.5f);
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

    private void ThrowWeapon()
    {
        //Weapon knife = Instantiate(LevelManager.Ins.knife, ThrowPoint.transform.position, ThrowPoint.transform.rotation);
        Weapon knife = SimplePool.Spawn<Weapon>(PoolType.Bullet_Knife);
        knife.transform.position = ThrowPoint.transform.position;
        knife.transform.rotation = ThrowPoint.transform.rotation;
        knife.OnInit(this);
        knife.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
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
        pos = 0;
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
        if(pos < characterList.Count)
        {
            gameObject.transform.LookAt(characterList[pos].transform);
        }
        else 
        {
            Debug.Log(name + ", pos = " + pos);
        }
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
        LevelManager.Ins.maxBot --;
    }

    public virtual void OnDeadExecute()
    {
        ChangeAnim("Dead");
        Invoke("DespawnObj", 2f);
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

    public void DespawnObj()
    {
        SimplePool.Despawn(this);
        
    }
}
