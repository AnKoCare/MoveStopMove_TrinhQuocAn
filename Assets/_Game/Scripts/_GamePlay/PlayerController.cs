using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Character character;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rigibody;
    [SerializeField] private bool EnableMove = true;
    [SerializeField] private bool AttackEnd = true;
    public int coin = 0;

    public int startingCoin; // Số vàng khởi đầu

    public const string goldKey = "Gold"; // Khóa lưu trữ vàng

    private Vector3 moveVector;

    private void Awake() 
    {
        for(int i = 0; i < 9; i++)
        {
            if(weaponData.GetWeapon((WeaponType)i).IsEquipped)
            {
                LevelManager.Ins.player.weaponType = (WeaponType)i;
                break;
            }
        }
        for(int i = 0; i < 11; i ++)
        {
            if(hairData.GetHair((HairsType)i).IsEquipped)
            {
                LevelManager.Ins.player.hairsType = (HairsType)i;
                break;
            }
        }
        for(int i = 0; i < 10; i ++)
        {
            if(pantsData.GetPants((PantsType)i).IsEquipped)
            {
                LevelManager.Ins.player.pantsType = (PantsType)i;
                break;
            }
        }
        for(int i = 0; i < 2; i ++)
        {
            if(supportItemData.GetSupportItem((SupportsType)i).IsEquipped)
            {
                LevelManager.Ins.player.supportsType = (SupportsType)i;
                break;
            }
        }
        for(int i = 0; i < 3; i ++)
        {
            if(suitData.GetSuit((SuitType)i).IsEquipped)
            {
                LevelManager.Ins.player.suitType = (SuitType)i;
                break;
            }
        }
    }
    
    public override void Start() 
    {
        base.Start();
        // Kiểm tra xem vàng đã được lưu trữ trước đó chưa
        if (PlayerPrefs.HasKey(goldKey))
        {
            // Nếu có, lấy giá trị vàng đã lưu trữ
            coin = PlayerPrefs.GetInt(goldKey);
        }
        else
        {
            // Nếu chưa, sử dụng giá trị vàng khởi đầu
            coin = startingCoin;
        }
    }

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
        if(isDead)
        {
            rigibody.velocity = Vector3.zero;
            return;
        } 
        
        Move();
    }

    private void Move()
    {
        if(!GameManager.Ins.IsState(GameState.Gameplay)) return;
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed * Time.fixedDeltaTime;
        moveVector.z = joystick.Vertical * moveSpeed * Time.fixedDeltaTime;

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            rigibody.velocity = transform.forward * moveSpeed;
            ChangeState(new PatrolState());
        }
        else if(joystick.Horizontal == 0 && joystick.Vertical == 0 && AttackEnd)
        {
            rigibody.velocity = Vector3.zero;
            ChangeState(new IdleState());
        }
        
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    //IDLE
    public override void OnIdleEnter()
    {
        base.OnIdleEnter();
    }

    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
        if (characterList.Count > 0)
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
        WeaponModel.gameObject.SetActive(false);
        AttackEnd = false;
        CountThrow = 0;
    }

    public override void OnAttackExecute()
    {
        if (CountThrow == 0)
        {
            base.OnAttackExecute();
            ChangeAnim("Attack");
            CountThrow++;
        }

        if(timerAttack >= (0.3f - (float)LevelManager.Ins.player.weaponData.GetWeapon(LevelManager.Ins.player.weaponType).attackSpeed / 10) * duration && CountThrow == 1)
        {
            isThrow = true;
            ThrowWeapon();
            isThrow = false;
            CountThrow++;
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
        isThrow = false;
        AttackEnd = true;
        timerAttack = 0f;
        WeaponModel.gameObject.SetActive(true);
    }


    public void UpCoin(int number)
    {
        coin += number;
        PlayerPrefs.SetInt(goldKey, coin);
    }

    public void BuyItem(int price)
    {
        coin -= price;
        PlayerPrefs.SetInt(goldKey, coin);
    }
}