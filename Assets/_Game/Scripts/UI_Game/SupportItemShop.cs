using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SupportItemShop : UICanvas
{
    public SupportItemData supportItemData;
    public SupportsType supportsType;
    public TextMeshProUGUI description;
    private int currentIndex = 0;
    public TextMeshProUGUI priceSupportItem;
    public TextMeshProUGUI select;
    public Canvas buttonBuySupportItem;
    public Canvas buttonSelect;
    public List<Image> imageLock;

    private void Update() 
    {
        Item();
    }

    public override void Setup()
    {
        base.Setup();
        currentIndex = 0;
        CameraFollow.Ins.SetupSuitShop();
        LevelManager.Ins.player.ChangeState(new Dance_CharSkin());
        for(int i = 0; i < 2; i ++)
        {
            if(supportItemData.GetSupportItem((SupportsType)i).IsUnlocked)
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
        base.SetDeActive(); 
    }

    public void Item()
    {
        description.text = supportItemData.GetSupportItem((SupportsType)currentIndex).Description;    
        if(!supportItemData.GetSupportItem((SupportsType)currentIndex).IsUnlocked)
        {
            buttonSelect.gameObject.SetActive(false);
            buttonBuySupportItem.gameObject.SetActive(true);
            priceSupportItem.text = "" + supportItemData.GetSupportItem((SupportsType)currentIndex).Price;
        }
        else
        {
            buttonSelect.gameObject.SetActive(true);
            buttonBuySupportItem.gameObject.SetActive(false);
            if(supportItemData.GetSupportItem((SupportsType)currentIndex).IsEquipped)
            {
                select.text = "UnEquip";
            }
            else
            {
                select.text = "Select";
            }
        }
    }

    public void ButtonSelectNormalShield()
    {
        if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
        {
            LevelManager.Ins.player.RemoveCharacterColor();
            LevelManager.Ins.player.RemovePant();
            LevelManager.Ins.player.RemoveSupportItem();
            LevelManager.Ins.player.RemoveTailItem();
            LevelManager.Ins.player.RemoveWingItem();
            LevelManager.Ins.player.RemoveHair();
        }
        LevelManager.Ins.player.RemoveSupportItem();
        currentIndex = 0;
        LevelManager.Ins.player.SetSupportItem((SupportsType)currentIndex);
    }

    public void ButtonSelectCaptainShield()
    {
        if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
        {
            LevelManager.Ins.player.RemoveCharacterColor();
            LevelManager.Ins.player.RemovePant();
            LevelManager.Ins.player.RemoveSupportItem();
            LevelManager.Ins.player.RemoveTailItem();
            LevelManager.Ins.player.RemoveWingItem();
            LevelManager.Ins.player.RemoveHair();
        }
        LevelManager.Ins.player.RemoveSupportItem();
        currentIndex = 1;
        LevelManager.Ins.player.SetSupportItem((SupportsType)currentIndex);
    }

    public void ButtonExitShop()
    {   
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonBuySupportItem()
    {
        if(LevelManager.Ins.player.coin >= supportItemData.GetSupportItem((SupportsType)currentIndex).Price)
        {
            LevelManager.Ins.player.BuyItem((int)supportItemData.GetSupportItem((SupportsType)currentIndex).Price);
            supportItemData.GetSupportItem((SupportsType)currentIndex).IsUnlocked = true;
            imageLock[currentIndex].gameObject.SetActive(false);
        }
    }

    public void ButtonSelection()
    {  
        if(!supportItemData.GetSupportItem((SupportsType)currentIndex).IsEquipped)
        {
            LevelManager.Ins.player.RemoveSupportItem();
            if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
            {
                UnEquipItem();
            }
            LevelManager.Ins.player.supportsType = (SupportsType)currentIndex;
            LevelManager.Ins.player.SetSupportItem(LevelManager.Ins.player.supportsType);
            supportItemData.GetSupportItem(LevelManager.Ins.player.supportsType).IsEquipped = true;
            LevelManager.Ins.player.SetUpSupportItemIndicator();
            for(int i = 0; i < 2; i ++)
            {
                if(supportItemData.GetSupportItem((SupportsType)i).IsEquipped == true && supportItemData.GetSupportItem((SupportsType)i) != supportItemData.GetSupportItem(LevelManager.Ins.player.supportsType))
                {
                    supportItemData.GetSupportItem((SupportsType)i).IsEquipped = false;
                    break;
                }
            }
        }
        else
        {
            supportItemData.GetSupportItem((SupportsType)currentIndex).IsEquipped = false;
            LevelManager.Ins.player.RemoveSupportItem();
            LevelManager.Ins.player.supportsType = SupportsType.EmptyShield;
        }
    }

    public void ButtonHatShop()
    {
        //LevelManager.Ins.player.RemoveSupportItem();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.HatShop);
    }

    public void ButtonPantShop()
    {
        //LevelManager.Ins.player.RemoveSupportItem();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.PantShop);
    }

    public void ButtonSuitShop()
    {
        //LevelManager.Ins.player.RemoveSupportItem();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.SuitShop);
    }

    public void UnEquipItem()
    {
        LevelManager.Ins.player.RemoveCharacterColor();
        LevelManager.Ins.player.RemovePant();
        LevelManager.Ins.player.RemoveSupportItem();
        LevelManager.Ins.player.RemoveTailItem();
        LevelManager.Ins.player.RemoveWingItem();
        LevelManager.Ins.player.RemoveHair();
        LevelManager.Ins.player.suitType = SuitType.EmptySuit;
        for(int i = 0; i < 3; i ++)
        {
            if(LevelManager.Ins.player.suitData.GetSuit((SuitType)i).IsEquipped == true)
            {
                LevelManager.Ins.player.suitData.GetSuit((SuitType)i).IsEquipped = false;
                break;
            }
        }
    }
}
