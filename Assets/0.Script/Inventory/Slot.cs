using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Text cntTxt;


    private DropItem dropItem;

    private float quickSlotTimer = 0;
    public AllEnum.PotionType PotionType { get; set; }
    public AllEnum.ItemType Type { get; set; }

    private int cnt;
    private bool mouseDown = false;
    public int Count 
    {
        get { return cnt; }
        set
        {
            cnt = value;
            cntTxt.text = cnt > 0 ? cnt.ToString() : string.Empty;
        }
    }
    public Sprite Sprite 
    {
        get { return iconImg.sprite; }
        set { iconImg.sprite = value; } 
    }

    void Start()
    {
        Count = 0;
    }

    void Update()
    {
        if(mouseDown)
        {
            quickSlotTimer += Time.deltaTime;
        }
    }

    public void OnButtonDown()
    {
        quickSlotTimer = 0;
        mouseDown = true;
    }

    public void OnButtonUp()
    {
        mouseDown = false;

        if (quickSlotTimer >= 2f)
        {
            if (Type == AllEnum.ItemType.Etc)
            {
                switch (PotionType)
                {
                    case AllEnum.PotionType.HP:
                    case AllEnum.PotionType.ALL:
                        Inventory.Instance.EquipQuickSlot(0, this);
                        break;
                    case AllEnum.PotionType.MP:
                        Inventory.Instance.EquipQuickSlot(1, this);
                        break;
                }
            }
        }
        else
        {
            OnUseItem();
        }
    }

    public void OnUseItem()
    {
        switch(Type)
        {
            // 소모아이템
            case AllEnum.ItemType.Etc:
                {
                    if (PlayerManager.Instance.PlayerStatInfo.HP >= PlayerManager.Instance.PlayerStatInfo.MaxHP)
                    {
                        Debug.Log("체력 가득 참");
                        return;
                    }

                    Count--;
                    switch(dropItem.potionType)
                    {
                        case AllEnum.PotionType.HP:
                            UIManager.Instance.SetHP += dropItem.addValue;
                            break;
                        case AllEnum.PotionType.MP:
                            PlayerManager.Instance.PlayerStatInfo.MP += dropItem.addValue;
                            break;
                        case AllEnum.PotionType.ALL:
                            UIManager.Instance.SetHP += dropItem.addValue;
                            PlayerManager.Instance.PlayerStatInfo.MP += dropItem.addValue;
                            break;
                    }

                    if(Count == 0)
                    {
                        Emtpy();
                    }
                    Inventory.Instance.QuickSlotReflush(this);
                }
                break;
        }
    }

    public void Emtpy()
    {
        iconImg.sprite = null;
        Count = 0;
        dropItem = null;
        quickSlotTimer = 0;
        PotionType = AllEnum.PotionType.None;
        Type = AllEnum.ItemType.None;
    }

    public void SetDropItem(DropItem dropItem)
    {
        this.dropItem = dropItem;
        Sprite = dropItem.Sprite;
        Type = dropItem.itemType;
        Count++;
    }
}
