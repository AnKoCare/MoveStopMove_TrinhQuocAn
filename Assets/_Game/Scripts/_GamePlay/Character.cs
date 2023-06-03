using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Character : GameUnit
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject ModelCharacter;

    public float moveSpeed;

    readonly Vector3 originSize = new Vector3(1,0,1);

    [SerializeField] private Vector3 originRingSize; 
    
    public BoxCollider boxCollider;
    
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererPant;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererCharacter;

    [SerializeField] private Transform Hair;

    public string nameKiller;

    public string nameChar;

    public MissionWaypoint missionWaypoint;

    public MissionWayPoint2 missionWayPoint2;

    public int LevelCharacter = 1;

    public ColorData colorData;

    public ColorsType colorsTypeChar;

    public HairData hairData;

    public HairsType hairsType;
    
    public float sizeCharacter;

    public float sizeRing;

    public float rateUpGold;

    private GameObject Pant;

    public PantsData pantsData;

    public PantsType pantsType;

    public WeaponType weaponType;

    public WeaponData weaponData;
    
    [SerializeField] private Transform WeaponHold;

    public SupportItemData supportItemData;

    public SupportsType supportsType;

    public SuitData suitData;

    public SuitType suitType;

    [SerializeField] private Transform Shield;

    [SerializeField] private Transform WingHold;

    [SerializeField] private Transform TailHold;

    public List<Character> characterList;
    public bool isIdle = false; // biến kiểm tra Idle
    public bool isPatrol = false; // biến kiểm tra Patrol
    public bool isAttack = false; // biến kiểm tra Attack

    public bool isDead = false; // biến kiểm tra Dead
    public bool isDance = false; // biến kiểm tra Dance
    public bool isThrow = false; // biến kiểm tra ném Weapon
    public float timerAttack = 0f; // biến đếm thời gian chạy animation tấn công
    public float duration; // biến lưu thời gian chạy của animation Attack
    public GameObject WeaponModel;
    public GameObject ThrowPoint;
    private int pos;
    public int CountThrow = 0;

    protected string currentAnimName = "Idle";

    public IState<Character> currentState;
    public UnityAction onDespawnEvent;

    public virtual void Start() 
    {
        missionWaypoint = SimplePool.Spawn<MissionWaypoint>(PoolType.MissionWaypoint);
        missionWaypoint.OnInit(this);
        
        missionWaypoint.target = this.transform;

        missionWayPoint2 = SimplePool.Spawn<MissionWayPoint2>(PoolType.MissionWayPoint2);
        missionWayPoint2.OnInit(this);
        
        missionWayPoint2.target = this.transform;

        originRingSize = Vector3.forward * attackRange.transform.localScale.z + Vector3.right * attackRange.transform.localScale.x;
        OnInit();   
        missionWaypoint.imgDir.color = colorData.GetColor(colorsTypeChar).Color.color;
        missionWayPoint2.imgPoint.color = colorData.GetColor(colorsTypeChar).Color.color;
        duration = anim.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Attack")?.length ?? 0;
    }

    public virtual void FixedUpdate()
    {
        
    }

    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {

    }

    
    public void ScaleUp()
    {
        this.LevelCharacter ++;
        this.sizeCharacter += 0.2f;
        this.sizeRing += 0.3f;
        this.moveSpeed += 0.4f;
        SetSizeChar(sizeCharacter);
        SetSizeRing(sizeRing);
    }

    public void OnKillUp()
    {

    }

    public void OnHit()
    {
        ChangeState(new Dead());
    }
    
    //TODO: set chi so thay vi scale up
    public void SetSizeChar(float size)
    {
        
        //TODO: cache transform
        ModelCharacter.transform.localScale = Vector3.one * (size);
    }

    public void SetSizeRing(float size)
    {
        attackRange.transform.localScale = originSize * (originRingSize.x * (100+size*20)/100) + Vector3.up * 0.1f;
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

    public void ThrowWeapon()
    {
        Weapon weapon = SimplePool.Spawn<Weapon>((PoolType)((int)weaponType));
        weapon.OnInit(this);
        weapon.AddForce();
    }


    //IDLE
    public virtual void OnIdleEnter()
    {
        isIdle = true;
    }

    public virtual void OnIdleExecute()
    {
        ChangeAnim("Idle");
    }

    public virtual void OnIdleExit()
    {
        isIdle = false;
    }


    //PATROL
    public virtual void OnPatrolEnter()
    {
        isPatrol = true;
    }

    public virtual void OnPatrolExecute()
    {
        ChangeAnim("Patrol");
    }

    public virtual void OnPatrolExit()
    {
        isPatrol = false;
    }


    //ATTACK
    public virtual void OnAttackEnter()
    {
        
    }    

    public virtual void OnAttackExecute()
    {
        if(characterList.Count == 0)
        {
            ChangeState(new IdleState());
            return;
        }
        float disMin = DisChar(characterList[0]);
        pos = 0;
        for(int i = 0; i < characterList.Count - 1; i++)
        {
            float disChar = DisChar(characterList[i]);
            if(disMin > disChar)
            {
                disMin = disChar;
                pos = i;
            }
        }
        if(pos < characterList.Count)
        {
            Vector3 direction = characterList[pos].TF.position - TF.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            gameObject.transform.rotation = rotation;
        }
    }

    public virtual void OnAttackExit()
    {
        isAttack = false;
    }


    //DEAD
    public virtual void OnDeadEnter()
    {
        isDead = true;
        LevelManager.Ins.SpawnNotice(nameKiller,nameChar);
        LevelManager.Ins.maxBot --;
        LevelManager.Ins.currentBot --;
        boxCollider.enabled = false;
        SimplePool.Despawn(missionWaypoint);
        SimplePool.Despawn(missionWayPoint2);
        onDespawnEvent?.Invoke();
        ChangeAnim("Dead");
        Invoke("DespawnObj", 2f);
    }

    public virtual void OnDeadExecute()
    {

    }   

    public virtual void OnDeadExit()
    {
        isDead = false;
    }


    //DANCE_WIN_ENTER
    public virtual void OnDance_WinEnter()
    {
        isDance = true;
    }

    public virtual void OnDance_WinExecute()
    {
        ChangeAnim("Dance");
    }

    public virtual void OnDance_WinExit()
    {
        isDance = false;
    }


    //DANCE_ChARSKIN
    public virtual void OnDance_CharSkinEnter()
    {
        isDance = true;
    }

    public virtual void OnDance_CharSkinExecute()
    {
        ChangeAnim("Dance_CharSkin");
    }

    public virtual void OnDance_CharSkinExit()
    {
        isDance = false;
    }

    public float DisChar(Character chars)
    {
       return Vector3.Distance(gameObject.transform.position ,chars.transform.position); 
    }

    public void DespawnObj()
    {
        SimplePool.Despawn(this);
    }

    public WeaponModel ChangeWeapon(WeaponType weaponType)
    {
        return Instantiate(weaponData.GetWeapon(weaponType).WeaponModel);
    }

    public Hair ChangeHair(HairsType hairsType)
    {
        return Instantiate(hairData.GetHair(hairsType).HairItem);
    }

    public Hair ChangeHairSuit(SuitType suitType)
    {
        return Instantiate(suitData.GetSuit(suitType).HairItem);
    }

    public SupportItem ChangeSupportItem(SupportsType supportsType)
    {
        return Instantiate(supportItemData.GetSupportItem(supportsType).SupportItem);
    }

    public SupportItem ChangeSupportItemSuit(SuitType supportsType)
    {
        return Instantiate(suitData.GetSuit(supportsType).supportItem);
    }

    public Tail ChangeTailSuit(SuitType tailType)
    {
        return Instantiate(suitData.GetSuit(tailType).TailItem);
    }

    public Wing ChangeWingSuit(SuitType wingType)
    {
        return Instantiate(suitData.GetSuit(wingType).WingItem);
    }

    public Material ChangeColor(ColorsType colorsType)
    {
        return Instantiate(colorData.GetColor(colorsType).Color);
    }

    public void SetColorBot(ColorsType type)
    {
        Material color = ChangeColor(type);
        skinnedMeshRendererCharacter.material = color;
    }

    public void SetWeapon(WeaponType type)
    {
        WeaponModel weapon = ChangeWeapon(type);
        weapon.transform.SetParent(WeaponHold);
        weapon.transform.localPosition = weaponData.GetWeapon(type).WeaponModel.transform.localPosition;
        weapon.transform.localRotation = weaponData.GetWeapon(type).WeaponModel.transform.localRotation;
    }

    public void RemoveWeapon()
    {
        WeaponModel childCollider = WeaponHold.GetComponentInChildren<WeaponModel>();
        if (childCollider != null)
        {
            Destroy(childCollider.gameObject);
        }
    }

    public void SetPant(PantsType type)
    {
        skinnedMeshRendererPant.material = pantsData.GetPants(type).Pant;

    }

    public void RemovePant()
    {
        skinnedMeshRendererPant.material = pantsData.GetPants(PantsType.EmptyPant).Pant;
    }

//TODO: nen co dau vao cu the
    public void SetHair(HairsType type)
    {
        Hair hair = ChangeHair(type);
        hair.transform.SetParent(Hair);
        hair.transform.localPosition = hairData.GetHair(type).HairItem.transform.localPosition;
        hair.transform.localRotation = hairData.GetHair(type).HairItem.transform.localRotation;
    }

    public void SetHairSuit(SuitType type)
    {
        Hair hair = ChangeHairSuit(type);
        hair.transform.SetParent(Hair);
        hair.transform.localPosition = suitData.GetSuit(type).HairItem.transform.localPosition;
        hair.transform.localRotation = suitData.GetSuit(type).HairItem.transform.localRotation;
    }

    public void RemoveHair()
    {
        Hair childCollider = Hair.GetComponentInChildren<Hair>();
        if (childCollider != null)
        {
            Destroy(childCollider.gameObject);
        }
    }

    public void SetSupportItem(SupportsType type)
    {
        SupportItem supportItem = ChangeSupportItem(type);
        supportItem.transform.SetParent(Shield);
        supportItem.transform.localPosition = supportItemData.GetSupportItem(type).SupportItem.transform.localPosition;
        supportItem.transform.localRotation = supportItemData.GetSupportItem(type).SupportItem.transform.localRotation;
    }

    public void SetSupportItemSuit(SuitType type)
    {
        SupportItem supportItem = ChangeSupportItemSuit(type);
        supportItem.transform.SetParent(Shield);
        supportItem.transform.localPosition = suitData.GetSuit(type).supportItem.transform.localPosition;
        supportItem.transform.localRotation = suitData.GetSuit(type).supportItem.transform.localRotation;
    }

    public void RemoveSupportItem()
    {
        SupportItem childCollider = Shield.GetComponentInChildren<SupportItem>();
        if (childCollider != null)
        {
            Destroy(childCollider.gameObject);
        }
    }

    public void SetTailSuit(SuitType type)
    {
        Tail tailItem = ChangeTailSuit(type);
        tailItem.transform.SetParent(TailHold);
        tailItem.transform.localPosition = suitData.GetSuit(type).TailItem.transform.localPosition;
        tailItem.transform.localRotation = suitData.GetSuit(type).TailItem.transform.localRotation;
    }
        
    public void RemoveTailItem()
    {
        Tail childCollider = TailHold.GetComponentInChildren<Tail>();
        if (childCollider != null)
        {
            Destroy(childCollider.gameObject);
        }
    }

    public void SetWingSuit(SuitType type)
    {
        Wing wingItem = ChangeWingSuit(type);
        wingItem.transform.SetParent(WingHold);
        wingItem.transform.localPosition = suitData.GetSuit(type).WingItem.transform.localPosition;
        wingItem.transform.localRotation = suitData.GetSuit(type).WingItem.transform.localRotation;
    }

    public void RemoveWingItem()
    {
        Wing childCollider = WingHold.GetComponentInChildren<Wing>();
        if (childCollider != null)
        {
            Destroy(childCollider.gameObject);
        }
    }

    public void SetCharacterColor(SuitType type)
    {
        skinnedMeshRendererCharacter.material = suitData.GetSuit(type).CharColor;
    }

    public void RemoveCharacterColor()
    {
        skinnedMeshRendererCharacter.material = suitData.GetSuit((SuitType)3).CharColor;
    }

    public void SetUpPantIndicator()
    {
        moveSpeed = 5f + pantsData.GetPants(pantsType).Speed;
    }

    public void SetUpSupportItemIndicator()
    {
        rateUpGold = 5f + supportItemData.GetSupportItem(supportsType).Gold;
    }

    public void SetUpWeaponAndHairIndicator()
    {
        sizeRing = 1f + weaponData.GetWeapon(LevelManager.Ins.player.weaponType).Range + hairData.GetHair(hairsType).Range;
    }

    public string GetRandomBotName()
    {
        if (LevelManager.Ins.usedNames.Count == LevelManager.Ins.botNames.Count)
        {
            // Đã sử dụng hết tất cả các tên có sẵn
            return "No more names available";
        }

        string randomName = "";

        do
        {
            int randomIndex = UnityEngine.Random.Range(0, LevelManager.Ins.botNames.Count); // Chọn một chỉ số ngẫu nhiên từ danh sách tên
            randomName = LevelManager.Ins.botNames[randomIndex]; // Lấy tên tại chỉ số ngẫu nhiên
        }
        while (LevelManager.Ins.usedNames.Contains(randomName)); // Lặp lại cho đến khi tìm được tên chưa được sử dụng

        LevelManager.Ins.usedNames.Add(randomName); // Đánh dấu tên đã được sử dụng

        return randomName;
    }

}
