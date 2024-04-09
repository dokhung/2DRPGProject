using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotHP : Singleton<QuickSlotHP>
{
    /*
     * 용도
     * 채력을 회복하기 위한 포션을 퀵슬롯에 설치하기 위한 클래스
     */

    public Image Img;
    public Text CountText;
    public AllEnum.PotionItemList PotionName = AllEnum.PotionItemList.None;

    public int ItemCount = 0;
    

    private void Start()
    {
        Img = GetComponent<Image>();
        CountText.gameObject.SetActive(false);
    }


    public void AddPotion_HP(Sprite ItemSprite,AllEnum.PotionType type,int itemCount,AllEnum.PotionItemList Name)
    {
        ItemCount = itemCount;
        PotionName = Name;
        if (PotionName == AllEnum.PotionItemList.HPPlus10)
        {
            Img.sprite = ItemSprite;
            ItemCount = itemCount;
            CountText.gameObject.SetActive(true);
            CountText.text = ItemCount.ToString();
            QucikSlotHPMP.Instance.SetHP = true;
        }
        else if (PotionName == AllEnum.PotionItemList.HPPlus100)
        {
            Img.sprite = ItemSprite;
            ItemCount = itemCount;
            CountText.gameObject.SetActive(true);
            CountText.text = ItemCount.ToString();
            QucikSlotHPMP.Instance.SetHP = true;
        }
        
    }
    
    // 버튼 함수
    public void QuickSlotPotionHPClick()
    {
        Debug.Log("채력포션 퀵슬롯 클릭");
        if (Img.sprite != null && ItemCount > 0 && PotionName != null)
        {
            
            Debug.Log("성공");
            if (PotionName == AllEnum.PotionItemList.HPPlus10)
            {
                ItemCount--;
                CountText.text = ItemCount.ToString();
                PlayerManager.Instance.PlayerStatInfo.HP += 10;
                Debug.Log("10회복");
            }
            else if (PotionName == AllEnum.PotionItemList.HPPlus100)
            {
                ItemCount--;
                CountText.text = ItemCount.ToString();
                PlayerManager.Instance.PlayerStatInfo.HP += 100;
                Debug.Log("100회복");
            }
            if (ItemCount == 0)
            {
                QucikSlotHPMP.Instance.SetHP = false;
            }
        }
    }
}
