using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PantShop : UICanvas
{
    public PantsData pantsData;
    public PantsType pantsType;
    public TextMeshProUGUI description;
    private int currentIndex = 0;
    public TextMeshProUGUI pricePant;
    public TextMeshProUGUI select;
    public Canvas buttonBuyPant;
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
        for(int i = 0; i < 9; i ++)
        {
            if(pantsData.GetPants((PantsType)i).IsUnlocked)
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

    private void Item()
    {
        description.text = pantsData.GetPants((PantsType)currentIndex).Description;    
        if(!pantsData.GetPants((PantsType)currentIndex).IsUnlocked)
        {
            buttonSelect.gameObject.SetActive(false);
            buttonBuyPant.gameObject.SetActive(true);
            pricePant.text = "" + pantsData.GetPants((PantsType)currentIndex).Price;
        }
        else
        {
            buttonSelect.gameObject.SetActive(true);
            buttonBuyPant.gameObject.SetActive(false);
            if(pantsData.GetPants((PantsType)currentIndex).IsEquipped)
            {
                select.text = "UnEquip";
            }
            else
            {
                select.text = "Select";
            }
        }
    }

    public void ButtonSelectBatman()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 0;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectChambi()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 1;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectComy()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 2;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectDabao()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 3;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectOnion()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 4;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectPokemon()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 5;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectRainbow()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 6;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectSkull()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 7;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonSelectVantim()
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
        LevelManager.Ins.player.RemovePant();
        currentIndex = 8;
        LevelManager.Ins.player.SetPant((PantsType)currentIndex);
    }

    public void ButtonExitShop()
    {   
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonBuyPant()
    {
        if(LevelManager.Ins.player.coin >= pantsData.GetPants((PantsType)currentIndex).Price)
        {
            LevelManager.Ins.player.BuyItem((int)pantsData.GetPants((PantsType)currentIndex).Price);
            pantsData.GetPants((PantsType)currentIndex).IsUnlocked = true;
            imageLock[currentIndex].gameObject.SetActive(false);
        }
    }

    public void ButtonSelection()
    {  
        if(!pantsData.GetPants((PantsType)currentIndex).IsEquipped)
        {
            LevelManager.Ins.player.RemovePant();
            if(LevelManager.Ins.player.suitType != SuitType.EmptySuit)
            {
                UnEquipItem();
            }
            LevelManager.Ins.player.pantsType = (PantsType)currentIndex;
            LevelManager.Ins.player.SetPant(LevelManager.Ins.player.pantsType);
            pantsData.GetPants(LevelManager.Ins.player.pantsType).IsEquipped = true;
            LevelManager.Ins.player.SetUpPantIndicator();
            for(int i = 0; i < 9; i ++)
            {
                if(pantsData.GetPants((PantsType)i).IsEquipped == true && pantsData.GetPants((PantsType)i) != pantsData.GetPants(LevelManager.Ins.player.pantsType))
                {
                    pantsData.GetPants((PantsType)i).IsEquipped = false;
                    break;
                }
            }
        }
        else
        {
            pantsData.GetPants((PantsType)currentIndex).IsEquipped = false;
            LevelManager.Ins.player.RemovePant();
            LevelManager.Ins.player.pantsType = PantsType.EmptyPant;
        }
    }

    public void ButtonHatShop()
    {
        //LevelManager.Ins.player.RemovePant();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.HatShop);
    }

    public void ButtonShieldShop()
    {
        //LevelManager.Ins.player.RemovePant();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.SupportItemShop);
    }

    public void ButtonSuitShop()
    {
        //LevelManager.Ins.player.RemovePant();
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
