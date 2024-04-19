using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Transform itemParent;
    private List<Slot> items = new List<Slot>();
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
        // �κ��丮�� �ڸ��� ������� �߰�
        if (findIdx != -1)
        {
            items[findIdx].Count++;
            items[findIdx].Type = dropItem.itemType;
        }
        // �κ��丮�� �ڸ��� ������� ����
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
}
