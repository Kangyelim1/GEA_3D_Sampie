using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotItemPrefab : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemText;

    // 참조 0개
    public void ItemSetting(Sprite itemSprite, string txt)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
    }

   
}
    
    
