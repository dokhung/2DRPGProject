using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Text cntTxt;


    private DropItem dropItem;
    public AllEnum.ItemType Type { get; set; }

    private int cnt;
    public int Count 
    {
        get { return cnt; }
        set
        {
            cnt = value;
            if (cnt > 0)
                cntTxt.text = cnt.ToString();
            else
                cntTxt.text = string.Empty;
        }
    }
    public Sprite Sprite 
    {
        get { return iconImg.sprite; }
        set
        {
            iconImg.sprite = value;
        } 
    }

    public void Start()
    {
        Count = 0;
    }

    public void OnUseItem()
    {
        switch(Type)
        {
            // 소모아이템
            case AllEnum.ItemType.Etc:
                {
                    if (PlayerManager.Instance.PlayerStatInfo.HP >= PlayerManager.Instance.PlayerStatInfo.MaxHP)
                        return;

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
                }
                break;
        }
    }

    public void Emtpy()
    {
        iconImg.sprite = null;
        Count = 0;
    }

    public void SetDropItem(DropItem dropItem)
    {
        this.dropItem = dropItem;
        Sprite = dropItem.Sprite;
        Type = dropItem.itemType;
        Count++;
    }
}
