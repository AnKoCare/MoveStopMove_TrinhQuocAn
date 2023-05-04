using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Character character;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;


    private void Update() 
    {
        if(character.currentState != null)
        {
            character.currentState.OnExecute(this);
        }  
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _characterController.Move(new Vector3(_joystick.Horizontal * _moveSpeed * Time.fixedDeltaTime, _gravity, _joystick.Vertical * _moveSpeed * Time.fixedDeltaTime));

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical));
            ChangeState(new PatrolState());
        }
        else if(_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !IsDead && AttackEnd)
        {
            ChangeState(new IdleState());
        }
    }


    //IDLE
    public override void OnIdleEnter()
    {
        base.OnIdleEnter();
    }

    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
        if(IsAttack)
        {
            ChangeState(new AttackState());
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
    }

    public override void OnPatrolExecute()
    {
        base.OnPatrolExecute();
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
        AttackEnd = true;
    }

}