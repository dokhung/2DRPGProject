using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Transform itemParent;
    private List<Slot> items = new List<Slot>();

    // 퀵슬롯 연결부
    [SerializeField] private QuickSlot[] quickSlot;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemParent.childCount; i++)
        {
            items.Add(itemParent.GetChild(i).GetComponent<Slot>());
        }
    }

    public int FindEmtpy(Sprite sp)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Sprite != null && items[i].Sprite.Equals(sp))
                return i;
        }

        return -1;
    }
    public void CreateItem(DropItem dropItem)
    {
        int findIdx = FindEmtpy(dropItem.Sprite);
        // 인벤토리에 자리가 있을경우 추가
        if (findIdx != -1)
        {
            items[findIdx].Count++;
            items[findIdx].PotionType = dropItem.potionType;
            items[findIdx].Type = dropItem.itemType;
        }
        // 인벤토리에 자리가 없을경우 생성
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Sprite == null)
                {
                    items[i].SetDropItem(dropItem);
                    break;
                }
            }
        }
    }

    public void EquipQuickSlot(int index, Slot slot)
    {
        quickSlot[index].SetItem(slot);
    }

    public void SlotReflush(Slot slot, QuickSlot qSlot)
    {
        foreach (var item in items)
        {
            if(item.Equals(slot))
            {
                item.OnUseItem();
                qSlot.SetItem(item);
                break;
            }
        }
    }

    public void QuickSlotReflush(Slot slot)
    {
        foreach (var item in quickSlot)
        {
            if (item.slot.Equals(slot))
            {
                item.SetItem(slot);
                break;
            }
        }
    }

}