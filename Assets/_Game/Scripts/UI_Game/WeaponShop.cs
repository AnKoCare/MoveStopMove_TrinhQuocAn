using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShop : UICanvas
{
    public WeaponData weaponData;
    private int currentIndex = 0;
    public Image spriteWeapon;
    public TextMeshProUGUI nameWeapon;
    public TextMeshProUGUI description;
    public TextMeshProUGUI weaponPrice;
    public Canvas buttonSelect;
    public Canvas buttonBuyWeapon;
    public TextMeshProUGUI equipped;

    private void Update() 
    {
        Item();    
    }

    public override void Setup()
    {
        base.Setup();
    }

    public override void SetDeActive()
    {
        base.SetDeActive();
    }

    public void Item()
    {
        spriteWeapon.sprite = weaponData.GetWeapon((WeaponType)currentIndex).WeaponImage;
        nameWeapon.text = weaponData.GetWeapon((WeaponType)currentIndex).WeaponName;
        description.text = weaponData.GetWeapon((WeaponType)currentIndex).description;
        weaponPrice.text = "" + weaponData.GetWeapon((WeaponType)currentIndex).Prices;
        if(!weaponData.GetWeapon((WeaponType)currentIndex).IsUnlocked)
        {
            buttonBuyWeapon.gameObject.SetActive(true);
            buttonSelect.gameObject.SetActive(false);
        }
        else
        {
            buttonBuyWeapon.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(true);
            if(weaponData.GetWeapon((WeaponType)currentIndex).IsEquipped)
            {
                equipped.text = "Equipped";
            }
            else
            {
                equipped.text = "Select";
            }
        }
    }

    public void ButtonEnterLeft()
    {
        if(currentIndex == 0) return;
        currentIndex --;
    }

    public void ButtonEnterRight()
    {
        if(currentIndex == 8) return;
        currentIndex ++;
    }

    public void ButtonExitShop()
    {
        LevelManager.Ins.player.RemoveWeapon();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonSelection()
    {
        LevelManager.Ins.player.RemoveWeapon();
        LevelManager.Ins.player.weaponType = (WeaponType)currentIndex;
        LevelManager.Ins.player.SetUpWeaponAndHairIndicator();
        for(int i = 0; i < 9; i++)
        {
            if(weaponData.GetWeapon((WeaponType)i).IsEquipped == true)
            {
                weaponData.GetWeapon((WeaponType)i).IsEquipped = false;
            }
        }
        weaponData.GetWeapon((WeaponType)currentIndex).IsEquipped = true;
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonBuyWeapon()
    {
        if(LevelManager.Ins.player.coin > weaponData.GetWeapon((WeaponType)currentIndex).Prices)
        {
            weaponData.GetWeapon((WeaponType)currentIndex).IsUnlocked = true;
            LevelManager.Ins.player.BuyItem((int)weaponData.GetWeapon((WeaponType)currentIndex).Prices);
        }
    }

}
