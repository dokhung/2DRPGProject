using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text count;

    [HideInInspector] public Slot slot;

    public void SetItem(Slot slot)
    {
        this.slot = slot;

        icon.sprite = slot.Sprite;
        count.text = $"{slot.Count}";

        if(slot.Count == 0)
        {
            icon.sprite = null;
            count.text = string.Empty;
            this.slot = null;
        }
    }

    public void Empty()
    {
        icon.sprite = null;
        count.text = string.Empty;
    }

    public void OnUseItem()
    {
        Inventory.Instance.SlotReflush(slot, this);
    }
}
