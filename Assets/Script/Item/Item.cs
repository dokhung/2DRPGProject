using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour
{
    // 아이템 갯수
    public int Count = 0;
    // 이미지
    public Sprite ItemImage;

    public AllEnum.ItemType itemType = AllEnum.ItemType.None;
    public AllEnum.PotionItemList potionItemList = AllEnum.PotionItemList.None;
    public AllEnum.PotionType potionType = AllEnum.PotionType.None;
    public AllEnum.EquipType equiptype = AllEnum.EquipType.None;
    public int potionNumber = 0;

    public abstract void UseItem();
}

// 아이템이 전략패턴

