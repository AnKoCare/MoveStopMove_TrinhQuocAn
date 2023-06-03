using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;
    public bool IsDestroyOnClose = false;

    public UIID ID;
    protected RectTransform m_RectTransform;
    private Animator m_Animator;
    private float m_OffsetY = 0;
    
    private void Start()
    {
        OnInit();
    }

    //Init default Canvas
    //khoi tao gia tri canvas
    protected void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();

        // xu ly tai tho
        float ratio = (float)Screen.height / (float)Screen.width;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = m_RectTransform.offsetMin;
            Vector2 rightTop = m_RectTransform.offsetMax;
            rightTop.y = -100f;
            m_RectTransform.offsetMax = rightTop;
            leftBottom.y = 0f;
            m_RectTransform.offsetMin = leftBottom;
            m_OffsetY = 100f;
        }
    }

    private void OnEnable() 
    {
        Setup();
    }

    private void OnDisable() 
    {
        SetDeActive();
    }
    //Setup canvas to avoid flash UI
    //set up mac dinh cho UI de tranh truong hop bi nhay' hinh
    public virtual void Setup()
    {
        if(LevelManager.Ins.player.suitType == SuitType.EmptySuit)
        {
            LevelManager.Ins.player.SetHair(LevelManager.Ins.player.hairsType);
            LevelManager.Ins.player.SetWeapon(LevelManager.Ins.player.weaponType);
            LevelManager.Ins.player.SetPant(LevelManager.Ins.player.pantsType);
            LevelManager.Ins.player.SetSupportItem(LevelManager.Ins.player.supportsType);
        }
        else
        {
            LevelManager.Ins.player.RemovePant();
            LevelManager.Ins.player.SetCharacterColor(LevelManager.Ins.player.suitType);
            LevelManager.Ins.player.SetHairSuit(LevelManager.Ins.player.suitType);
            LevelManager.Ins.player.SetSupportItemSuit(LevelManager.Ins.player.suitType);
            LevelManager.Ins.player.SetTailSuit(LevelManager.Ins.player.suitType);
            LevelManager.Ins.player.SetWingSuit(LevelManager.Ins.player.suitType);
            LevelManager.Ins.player.SetWeapon(LevelManager.Ins.player.weaponType);
        }
        
    }

    public virtual void SetDeActive()
    {
        LevelManager.Ins.player.RemoveHair();
        LevelManager.Ins.player.RemoveWeapon();
        LevelManager.Ins.player.RemovePant();
        LevelManager.Ins.player.RemoveSupportItem();
        LevelManager.Ins.player.RemoveCharacterColor();
        LevelManager.Ins.player.RemoveTailItem();
        LevelManager.Ins.player.RemoveWingItem();
    }


    //back key in android device
    //back key danh cho android
    public virtual void BackKey()
    {

    }

    //Open canvas
    //mo canvas
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //close canvas directly
    //dong truc tiep, ngay lap tuc
    public virtual void CloseDirectly()
    {
        //UIManager.Ins.RemoveBackUI(this);
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }

    }

    //close canvas with delay time, used to anim UI action
    //dong canvas sau mot khoang thoi gian delay
    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

}

public enum UIID
{
    MainMenu = 0,
    WeaponShop = 1,
    HatShop = 2,
    Joystick = 3,
    Gameplay = 4,
    PantShop = 5,
    SupportItemShop = 6,
    SuitShop = 7,
    UIRevive = 8,
    UIGameOver = 9,
    UIPauseGame = 10,
    UINotice = 11
}
