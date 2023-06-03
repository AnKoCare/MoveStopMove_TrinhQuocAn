using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinShop : UICanvas
{
    public Button BtnNextItem;
    public HairData hairData;
    public HairsType hairsType;
    private int currentIndex = 0;
    public TextMeshProUGUI description;
    public TextMeshProUGUI priceHat;
    public TextMeshProUGUI select;
    public Canvas buttonBuyHat;
    public Canvas buttonSelect;
    public List<Image> imageLock;

    // private void Start() {
    //     BtnNextItem.onClick.AddListener(() => Test(currentIndex));   
    // }

    private void Update() 
    {
        Item();
    }

    private void Item()
    {
        description.text = hairData.GetHair((HairsType)currentIndex).Description;    
        if(!hairData.GetHair((HairsType)currentIndex).IsUnlocked)
        {
            buttonSelect.gameObject.SetActive(false);
            buttonBuyHat.gameObject.SetActive(true);
            priceHat.text = "" + hairData.GetHair((HairsType)currentIndex).Price;
        }
        else
        {
            buttonSelect.gameObject.SetActive(true);
            buttonBuyHat.gameObject.SetActive(false);
            if(hairData.GetHair((HairsType)currentIndex).IsEquipped)
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
        base.Setup();
        currentIndex = 0;
        CameraFollow.Ins.SetupSuitShop();
        LevelManager.Ins.player.ChangeState(new Dance_CharSkin());
        for(int i = 0; i < 10; i ++)
        {
            if(hairData.GetHair((HairsType)i).IsUnlocked)
            {
                imageLock[i].gameObject.SetActive(false);
            }
            else
            {
                imageLock[i].gameObject.SetActive(true);
            }
        }
    }


    public void Test(int index) {

    }

    public override void SetDeActive()
    {
        base.SetDeActive(); 
    }

    public void ButtonSelectArrow()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 0;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectCowboy()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 1;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectCrown()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 2;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectEar()
    {
        if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
        {
            LevelManager.Ins.player.RemoveCharacterColor();
            LevelManager.Ins.player.RemovePant();
            LevelManager.Ins.player.RemoveSupportItem();
            LevelManager.Ins.player.RemoveTailItem();
            LevelManager.Ins.player.RemoveWingItem();
        }
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 3;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectHat()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 4;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectHatCap()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 5;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectHatYeallow()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 6;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectHeadPhone()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 7;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectHorn()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 8;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    public void ButtonSelectRau()
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
        LevelManager.Ins.player.RemoveHair();
        currentIndex = 9;
        LevelManager.Ins.player.SetHair((HairsType)currentIndex);
    }
    
    public void ButtonExitShop()
    {   
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }
    public void ButtonSelection()
    {  
        if(!hairData.GetHair((HairsType)currentIndex).IsEquipped)
        {
            LevelManager.Ins.player.RemoveHair();
            if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
            {
                UnEquipItem();
            }
            LevelManager.Ins.player.hairsType = (HairsType)currentIndex;
            LevelManager.Ins.player.SetHair(LevelManager.Ins.player.hairsType);
            hairData.GetHair(LevelManager.Ins.player.hairsType).IsEquipped = true;
            LevelManager.Ins.player.SetUpWeaponAndHairIndicator();
            for(int i = 0; i < 10; i ++)
            {
                if(hairData.GetHair((HairsType)i).IsEquipped == true && hairData.GetHair((HairsType)i) != hairData.GetHair(LevelManager.Ins.player.hairsType))
                {
                    hairData.GetHair((HairsType)i).IsEquipped = false;
                    break;
                }
            }
        }
        else
        {
            hairData.GetHair((HairsType)currentIndex).IsEquipped = false;
            LevelManager.Ins.player.RemoveHair();
            LevelManager.Ins.player.hairsType = HairsType.EmptyHair;
        }
        
    }

    public void ButtonBuyHat()
    {
        if(LevelManager.Ins.player.coin >= hairData.GetHair((HairsType)currentIndex).Price)
        {
            LevelManager.Ins.player.BuyItem((int)hairData.GetHair((HairsType)currentIndex).Price);
            hairData.GetHair((HairsType)currentIndex).IsUnlocked = true;
            imageLock[currentIndex].gameObject.SetActive(false);
        }
    }

    public void ButtonPantShop()
    {
        //LevelManager.Ins.player.RemoveHair();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.PantShop);
    }

    public void ButtonShieldShop()
    {
        //LevelManager.Ins.player.RemoveHair();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.SupportItemShop);
    }

    public void ButtonSuitShop()
    {
        //LevelManager.Ins.player.RemoveHair();
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
