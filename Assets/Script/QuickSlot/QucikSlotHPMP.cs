using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QucikSlotHPMP : FunctionSingleton<QucikSlotHPMP>
{
    /*
     * 기획
     * 둘다 회복하는 포션을 퀵슬롯에 등록을 한다.
     * 우선적으로 HP 혹은 MP의 퀵슬롯중 등록한 포션이 없는경우를 우선순위로 두어서 퀵슬롯에 저장을 할수있도록한다.
     * 만일 퀵슬롯에 둘다 등록이 되어있다면 갯수가 적은 퀵슬롯의 정보를 먼저 받아서 교체를 하는식으로 진행을 한다.
     * 둘다 갯수가 같을경우 HP포션과의 교체를 우선적으로 한다.
     * 만약 퀵슬롯중 하나라도 둘다 회복을 하는 포션이 이미 퀵슬롯에 등록이 되어 있을때는 둘다 회복하는 포션이 1개라면 둘다 회복하는 포션을 교체하고 2개이상이면 하나만 회복하는 포션을 교체한다.
     * 둘다 비어있다면 HP를 우선적으로 한다.
     * HPMP포션은 양쪽으로 넣을수 없도록하며 HP포션과 MP포션중 하나가 있을경우 넣을수없도록 설정한다.
     */

    public bool SetHP = false;
    public bool SetMP = false;
    public bool SetHPMP_inHP = false;
    public bool SetHPMP_inMP = false;
    public bool SetHPMP = false;

    

    public void AddPotionInfo(Sprite ItemSprite,AllEnum.PotionType type,int itemCount,AllEnum.PotionItemList Name)
    {
        Debug.Log("Name :: " + Name);
        Image HPItemImg = QuickSlotHP.Instance.Img;
        Image MPItemImg = QucikSlotMP.Instance.Img;
        if (Name == AllEnum.PotionItemList.HPMPPlus10)
        {
            if (HPItemImg.sprite == null)
            {
                if (SetHPMP_inMP && SetMP)
                {
                    Debug.Log("이미 쌍방 회복 포션이 존재함");
                }
                else
                {
                    SetHP = true;
                    SetHPMP_inHP = true;
                    HPItemImg.sprite = ItemSprite;
                    QuickSlotHP.Instance.CountText.gameObject.SetActive(true);
                    QuickSlotHP.Instance.ItemCount = itemCount;
                    QuickSlotHP.Instance.CountText.text = itemCount.ToString();
                    Debug.Log("QuickSlotHP.Instance.ItemCount :: "+QuickSlotHP.Instance.ItemCount); 
                }
            }
        }
        else if (Name == AllEnum.PotionItemList.HPMPPlus100)
        {
            if (HPItemImg.sprite == null)
            {
                if (SetHPMP_inMP && SetMP)
                {
                    Debug.Log("넣을수 없음");
                }
                else
                {
                    SetHP = true;
                    SetHPMP_inHP = true;
                    HPItemImg.sprite = ItemSprite;
                    QuickSlotHP.Instance.CountText.gameObject.SetActive(true);
                    QuickSlotHP.Instance.ItemCount = itemCount;
                    QuickSlotHP.Instance.CountText.text = itemCount.ToString();
                    Debug.Log("QuickSlotHP.Instance.ItemCount :: "+QuickSlotHP.Instance.ItemCount);  
                }
            }
        }
    }
}
