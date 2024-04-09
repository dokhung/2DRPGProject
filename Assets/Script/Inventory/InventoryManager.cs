using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : Singleton<InventoryManager>
{
   public List<Slot> Slots = new List<Slot>();

   private void Start()
   {
      for (int i = 0; i < transform.GetChild(0).childCount; i++)
      {
         Slots.Add(transform.GetChild(0).GetChild(i).GetComponent<Slot>());
      }
   }

   // 아이템 인벤토리 PickUp()함수 실행후 1단계
   public void Registration(Item item)
   {
        int slotIndex = -1;
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].img.sprite != null && Slots[i].img.sprite.name == item.ItemImage.name)
            {
                slotIndex = i;
                break;
            }
        }

        if (slotIndex == -1)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].img.sprite == null)
                {
                    //Slots[i].SetItem(item, 1);
                    Slots[i].CreateItem(item);
                    break;
                }
            }
        }
        else
        {
            //Slots[slotIndex].IncreaseItemCount(1);
            Slots[slotIndex].SetCount(1);
            
        }
    }
}
