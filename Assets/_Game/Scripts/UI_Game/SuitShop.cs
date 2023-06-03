using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SuitShop : UICanvas
{
    public SuitData suitData;
    public SuitType suitType;
    private int currentIndex = 0;
    public TextMeshProUGUI description;
    public TextMeshProUGUI priceSuit;
    public TextMeshProUGUI select;
    public Canvas buttonBuySuit;
    public Canvas buttonSelect;
    public List<Image> imageLock;

    void Update()
    {
        Item();
    }

    public void Item()
    {
        description.text = suitData.GetSuit((SuitType)currentIndex).Description;    
        if(!suitData.GetSuit((SuitType)currentIndex).IsUnlocked)
        {
            buttonSelect.gameObject.SetActive(false);
            buttonBuySuit.gameObject.SetActive(true);
            priceSuit.text = "" + suitData.GetSuit((SuitType)currentIndex).Price;
        }
        else
        {
            buttonSelect.gameObject.SetActive(true);
            buttonBuySuit.gameObject.SetActive(false);
            if(suitData.GetSuit((SuitType)currentIndex).IsEquipped)
            {
                select.text = "UnEquip";
            }
            else
            {
                select.text = "Select";
            }
        }
    }

    public override void Setup()
    {
        currentIndex = 3;
        base.Setup();
        CameraFollow.Ins.SetupSuitShop();
        LevelManager.Ins.player.ChangeState(new Dance_CharSkin());
        for(int i = 0; i < 3; i ++)
        {
            if(suitData.GetSuit((SuitType)i).IsUnlocked)
            {
                imageLock[i].gameObject.SetActive(false);
            }
            else
            {
                imageLock[i].gameObject.SetActive(true);
            }
        }
    }

    public override void SetDeActive()
    {
        RemoveItemSuit();
        
    }

    public void ButtonSelectDevil()
    {
        RemoveItemSuit();
        currentIndex = 0;
        SetUpItemSuit((SuitType)currentIndex);
    }

    public void ButtonSelectAngle()
    {
        RemoveItemSuit();
        currentIndex = 1;
        SetUpItemSuit((SuitType)currentIndex);
    }

    public void ButtonSelectWitch()
    {
        RemoveItemSuit();
        currentIndex = 2;
        SetUpItemSuit((SuitType)currentIndex);
    }

    public void ButtonExitShop()
    {   
        RemoveItemSuit();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonSelection()
    {  
        if(!suitData.GetSuit((SuitType)currentIndex).IsEquipped)
        {
            UnEquipItem();
            RemoveItemSuit();
            LevelManager.Ins.player.suitType = (SuitType)currentIndex;
            SetUpItemSuit(LevelManager.Ins.player.suitType);
            suitData.GetSuit(LevelManager.Ins.player.suitType).IsEquipped = true;
            for(int i = 0; i < 3; i ++)
            {
                if(suitData.GetSuit((SuitType)i).IsEquipped == true && suitData.GetSuit((SuitType)i) != suitData.GetSuit(LevelManager.Ins.player.suitType))
                {
                    suitData.GetSuit((SuitType)i).IsEquipped = false;
                    break;
                }
            }
        }
        else
        {
            suitData.GetSuit((SuitType)currentIndex).IsEquipped = false;
            RemoveItemSuit();
            LevelManager.Ins.player.SetWeapon(LevelManager.Ins.player.weaponType);
            LevelManager.Ins.player.suitType = SuitType.EmptySuit;
        }
    }

    public void ButtonBuySuit()
    {
        if(LevelManager.Ins.player.coin >= suitData.GetSuit((SuitType)currentIndex).Price)
        {
            LevelManager.Ins.player.BuyItem((int)suitData.GetSuit((SuitType)currentIndex).Price);
            suitData.GetSuit((SuitType)currentIndex).IsUnlocked = true;
            imageLock[currentIndex].gameObject.SetActive(false);
        }
    }

    public void ButtonPantShop()
    {
        RemoveItemSuit();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.PantShop);
    }

    public void ButtonShieldShop()
    {
        RemoveItemSuit();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.SupportItemShop);
    }

    public void ButtonHatShop()
    {
        RemoveItemSuit();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.HatShop);
    }

    public void ButtonSuitShop()
    {

    }

    public void RemoveItemSuit()
    {
        LevelManager.Ins.player.RemoveHair();
        LevelManager.Ins.player.RemoveCharacterColor();
        LevelManager.Ins.player.RemovePant();
        LevelManager.Ins.player.RemoveWeapon();
        LevelManager.Ins.player.RemoveSupportItem();
        LevelManager.Ins.player.RemoveTailItem();
        LevelManager.Ins.player.RemoveWingItem();
    }

    public void SetUpItemSuit(SuitType type)
    {
        LevelManager.Ins.player.SetHairSuit(type);
        LevelManager.Ins.player.SetCharacterColor(type);
        LevelManager.Ins.player.SetSupportItemSuit(type);
        LevelManager.Ins.player.SetWeapon(LevelManager.Ins.player.weaponType);
        LevelManager.Ins.player.SetTailSuit(type);
        LevelManager.Ins.player.SetWingSuit(type);
    }

    public void UnEquipItem()
    {
        LevelManager.Ins.player.hairsType = HairsType.EmptyHair;
        LevelManager.Ins.player.pantsType = PantsType.EmptyPant;
        LevelManager.Ins.player.supportsType = SupportsType.EmptyShield;
        for(int i = 0; i < 2; i ++)
        {
            if(LevelManager.Ins.player.supportItemData.GetSupportItem((SupportsType)i).IsEquipped == true)
            {
                LevelManager.Ins.player.supportItemData.GetSupportItem((SupportsType)i).IsEquipped = false;
                break;
            }
        }
        for(int i = 0; i < 10; i ++)
        {
            if(LevelManager.Ins.player.hairData.GetHair((HairsType)i).IsEquipped == true)
            {
                LevelManager.Ins.player.hairData.GetHair((HairsType)i).IsEquipped = false;
                break;
            }
        }
        for(int i = 0; i < 9; i ++)
        {
            if(LevelManager.Ins.player.pantsData.GetPants((PantsType)i).IsEquipped == true)
            {
                LevelManager.Ins.player.pantsData.GetPants((PantsType)i).IsEquipped = false;
                break;
            }
        }
    }

}
