using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int startingGold = 100; // Vàng khởi đầu

    private int currentGold; // Số vàng hiện tại

    private const string goldKey = "Gold"; // Khóa lưu trữ vàng

    private void Start()
    {
        // Kiểm tra xem vàng đã được lưu trữ trước đó chưa
        if (PlayerPrefs.HasKey(goldKey))
        {
            // Nếu có, lấy giá trị vàng đã lưu trữ
            currentGold = PlayerPrefs.GetInt(goldKey);
        }
        else
        {
            // Nếu chưa, sử dụng giá trị vàng khởi đầu
            currentGold = startingGold;
        }

        // Hiển thị vàng hiện tại
        Debug.Log("Current Gold: " + currentGold);
    }

    public void BuyItem(int itemCost)
    {
        // Kiểm tra xem có đủ vàng để mua không
        if (currentGold >= itemCost)
        {
            // Trừ vàng sau khi mua
            currentGold -= itemCost;
            Debug.Log("Item purchased! Remaining Gold: " + currentGold);

            // Lưu trữ giá trị vàng mới
            PlayerPrefs.SetInt(goldKey, currentGold);
        }
        else
        {
            Debug.Log("Not enough gold to buy the item!");
        }
    }
}
