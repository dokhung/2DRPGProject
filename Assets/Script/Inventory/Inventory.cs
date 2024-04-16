using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Transform itemParent;
    private List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemParent.childCount; i++)
        {
            items.Add(itemParent.GetChild(0).GetComponent<Item>());
        }
    }

    public int FindEmtpy(Sprite sp)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].sprite.Equals(sp))
                return i;
        }

        return -1;
    }
    public void CreateItem(Sprite sprite)
    {
        int findIdx = FindEmtpy(sprite);
        // �κ��丮�� �ڸ��� ������� �߰�
        if (findIdx != -1)
        {
            items[findIdx].Count++;
        }
        // �κ��丮�� �ڸ��� ������� ����
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].sprite == null)
                {
                    items[i].sprite = sprite;
                    items[i].Count++;
                    break;
                }
            }
        }
    }
}
