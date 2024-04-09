using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QucikSlotMP : Singleton<QucikSlotMP>
{
    /*
     * 용도
     * 마나을 회복하기 위한 포션을 퀵슬롯에 설치하기 위한 클래스
     */

    public Image Img;
    public Text CountText;

    public int ItemCount = 0;

    private void Start()
    {
        Img = GetComponent<Image>();
        CountText.gameObject.SetActive(false);
    }
    
    public void AddPotion_MP(Sprite ItemSprite,AllEnum.PotionType type,int itemCount,AllEnum.PotionItemList Name)
    {
        if (Name == AllEnum.PotionItemList.MP10)
        {
            Img.sprite = ItemSprite;
            ItemCount = itemCount;
            CountText.gameObject.SetActive(true);
            CountText.text = ItemCount.ToString();
            QucikSlotHPMP.Instance.SetMP = true;
        }
        else if (Name == AllEnum.PotionItemList.MP100)
        {
            Img.sprite = ItemSprite;
            ItemCount = itemCount;
            CountText.gameObject.SetActive(true);
            CountText.text = ItemCount.ToString();
            QucikSlotHPMP.Instance.SetMP = true;
        }
    }
    
    public void QuickSlotPotionMPClick()
    {
        Image MPItemImg = QucikSlotMP.Instance.Img; 
    }
}
