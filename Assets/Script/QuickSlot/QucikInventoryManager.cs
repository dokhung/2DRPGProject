using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class QucikInventoryManager : Singleton<QucikInventoryManager>
{
    public int SlotNum = 0;
    public void AddPotionInfo(Sprite ItemSprite,AllEnum.PotionType type, int ItemCount, AllEnum.PotionItemList Name, int TypeNumber,int SlotIndex, Slot SlotInfo)
    {
        Debug.Log("SlotInfo :: " + SlotInfo);
        SlotNum = SlotIndex;
        Debug.Log("SlotNum :: " + SlotNum);
        if (type == AllEnum.PotionType.HP && TypeNumber == 1)
        {
            Debug.Log("HP입니다.");
            QuickSlotHP.instance.AddPotion_HP(ItemSprite,type,ItemCount,Name);
        }
        else if (type == AllEnum.PotionType.MP && TypeNumber == 2)
        {
            Debug.Log("MP입니다.");
            QucikSlotMP.instance.AddPotion_MP(ItemSprite,type,ItemCount,Name);
        }
        else if (type == AllEnum.PotionType.HPMP && TypeNumber == 3)
        {
            Debug.Log("둘다 회복하는 포션의 정보가 들어왔다.");
            QucikSlotHPMP.Instance.AddPotionInfo(ItemSprite,type,ItemCount,Name);
        }
    }
}

//