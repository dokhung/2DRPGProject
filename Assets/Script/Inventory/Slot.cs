using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image img;
    public Text countText;

    public int itemCnt = 0;
    public UnityAction action;
    public AllEnum.ItemType itemType = AllEnum.ItemType.None;
    public AllEnum.PotionType potionType = AllEnum.PotionType.None;
    public AllEnum.PotionItemList PotionName = AllEnum.PotionItemList.None;
    public AllEnum.PotionItemList SeletedPotion = AllEnum.PotionItemList.None;
    
    public float holdTime = 2.0f;
    private bool isPressed = false;
    private float pressedTime = 0.0f;
    public int TypeNumber = 0;
    public bool FullHP = false;
    public bool FullMP = false;
    public bool HPMPFull = false;
    public bool isQuickHP = false;
    public bool isQuickMP = false;
    public bool isQuickHPMP = false;
    public int ItemCnt
    {
        get { return itemCnt; }
        set
        {
            itemCnt = value;
            countText.text = itemCnt.ToString();
            countText.gameObject.SetActive(itemCnt > 0);
        }
    }

    private void Start()
    {
        countText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerManager.Instance.PlayerStatInfo.HP == PlayerManager.Instance.PlayerStatInfo.MaxHP)
        {
            FullHP = true;
        }
        else
        {
            FullHP = false;
        }

        if (PlayerManager.Instance.PlayerStatInfo.MP == PlayerManager.Instance.PlayerStatInfo.MaxMP)
        {
            FullMP = true;
        }
        else
        {
            FullMP = false;
        }
        
        if (PlayerManager.Instance.PlayerStatInfo.HP == PlayerManager.Instance.PlayerStatInfo.MaxHP && PlayerManager.Instance.PlayerStatInfo.MP == PlayerManager.Instance.PlayerStatInfo.MaxMP)
        {
            HPMPFull = true;
        }
        else
        {
            HPMPFull = false;
        }

        if (QucikSlotHPMP.Instance.SetHPMP_inHP && QucikSlotHPMP.Instance.SetHP)
        {
            isQuickHP = true;
        }

        if (QucikSlotHPMP.Instance.SetHPMP_inMP && QucikSlotHPMP.Instance.SetMP)
        {
            isQuickMP = true;
        }

        if (QucikSlotHPMP.Instance.SetHPMP_inMP && QucikSlotHPMP.Instance.SetMP && QucikSlotHPMP.Instance.SetHPMP_inHP && QucikSlotHPMP.Instance.SetHP)
        {
            isQuickHPMP = true;
        }
    }

    public void CreateItem(Item item)
    {
        ItemCnt++;
        itemType = AllEnum.ItemType.Etc;
        img.sprite = item.ItemImage;
        countText.gameObject.SetActive(true);
        action = item.UseItem;
        
        int potionNumber = 0;

        if (item is HPPlus10Potion || item is HPPlus100Potion)
        {
            potionNumber = 1;
            potionType = AllEnum.PotionType.HP;
            if (item is HPPlus10Potion)
            {
                PotionName = AllEnum.PotionItemList.HPPlus10;
            }
            else if (item is HPPlus100Potion)
            {
                PotionName = AllEnum.PotionItemList.HPPlus100;
            }
        }
        else if (item is MP10Potion || item is MP100Potion)
        {
            potionNumber = 2;
            potionType = AllEnum.PotionType.MP;
            if (item is MP10Potion)
            {
                PotionName = AllEnum.PotionItemList.MP10;
            }
            else if (item is MP100Potion)
            {
                PotionName = AllEnum.PotionItemList.MP100;
            }
        }
        else if (item is HPMPPlus10Potion || item is HPMPPlus100Potion)
        {
            potionNumber = 3;
            potionType = AllEnum.PotionType.HPMP;
            if (item is HPMPPlus10Potion)
            {
                PotionName = AllEnum.PotionItemList.HPMPPlus10;
            }
            else if (item is HPMPPlus100Potion)
            {
                PotionName = AllEnum.PotionItemList.HPMPPlus100;
            }
        }

        if (potionNumber != 0 && PotionName != AllEnum.PotionItemList.None)
        {
            TypeNumber = potionNumber;
            SeletedPotion = PotionName;
        }
        
    }

    public void SetCount(int cnt)
    {
        itemCnt += cnt;
        countText.text = itemCnt.ToString();
        countText.gameObject.SetActive(true);
        if (TypeNumber == 1)
        {
            if (SeletedPotion == AllEnum.PotionItemList.HPPlus10)
            {
                if (QuickSlotHP.Instance.ItemCount == 0)
                {
                    QuickSlotHP.Instance.CountText.text = itemCnt.ToString();     
                }
                if (QuickSlotHP.Instance.ItemCount != 0 && SeletedPotion == AllEnum.PotionItemList.HPPlus10)
                {
                    QuickSlotHP.Instance.CountText.text = itemCnt.ToString();     
                }
            }
            if (SeletedPotion == AllEnum.PotionItemList.HPPlus100)
            {
                if (QuickSlotHP.Instance.ItemCount == 0)
                {
                    QuickSlotHP.Instance.CountText.text = itemCnt.ToString();     
                }
                if (QuickSlotHP.Instance.ItemCount != 0 && SeletedPotion == AllEnum.PotionItemList.HPPlus100)
                {
                    QuickSlotHP.Instance.CountText.text = itemCnt.ToString();     
                }
            }
            
        }
        if (TypeNumber == 2)
        {
            if (SeletedPotion == AllEnum.PotionItemList.MP100)
            {
                if (QucikSlotMP.Instance.ItemCount == 0)
                {
                    QucikSlotMP.Instance.CountText.text = itemCnt.ToString();     
                }
                if (QucikSlotMP.Instance.ItemCount != 0 && SeletedPotion == AllEnum.PotionItemList.MP100)
                {
                    QucikSlotMP.Instance.CountText.text = itemCnt.ToString();     
                }
            }
            if (SeletedPotion == AllEnum.PotionItemList.MP10)
            {
                if (QucikSlotMP.Instance.ItemCount == 0)
                {
                    QucikSlotMP.Instance.CountText.text = itemCnt.ToString();     
                }
                if (QucikSlotMP.Instance.ItemCount != 0 && SeletedPotion == AllEnum.PotionItemList.MP10)
                {
                    QucikSlotMP.Instance.CountText.text = itemCnt.ToString();     
                }
            }
        }
        if (TypeNumber == 3)
        {
            if (SeletedPotion == AllEnum.PotionItemList.HPMPPlus10)
            {
                if (!QucikSlotHPMP.Instance.SetHPMP_inHP && !QucikSlotHPMP.Instance.SetHPMP_inMP)
                {
                    if (!QucikSlotHPMP.Instance.SetHP)
                    {
                        QuickSlotHP.Instance.CountText.text = itemCnt.ToString();
                    }
                    else if (!QucikSlotHPMP.Instance.SetMP && QucikSlotHPMP.Instance.SetHP)
                    {
                        QucikSlotMP.Instance.CountText.text = itemCnt.ToString();
                    }
                }
            }

            else if (SeletedPotion == AllEnum.PotionItemList.HPMPPlus100)
            {
                if (!QucikSlotHPMP.Instance.SetHPMP_inHP && !QucikSlotHPMP.Instance.SetHPMP_inMP)
                {
                    if (!QucikSlotHPMP.Instance.SetHP)
                    {
                        QucikSlotMP.Instance.CountText.text = itemCnt.ToString();
                    }
                    else if (!QucikSlotHPMP.Instance.SetMP && QucikSlotHPMP.Instance.SetHP)
                    {
                        QucikSlotMP.Instance.CountText.text = itemCnt.ToString();
                    }
                }
            }
        }
        
    }
    
    // 버튼 함수
    public void OnClickUseItem()
    {
        
        Stat.PlayerStat ps = PlayerManager.Instance.PlayerStatInfo;
        if (ps.MP < ps.MaxMP && ps.HP < ps.MaxHP && !HPMPFull && potionType == AllEnum.PotionType.HPMP)
        {
            if (action != null) action.Invoke();
            if (img != null)
            {
                if (QucikSlotHPMP.Instance.SetHPMP_inHP)
                {
                    if (QucikSlotHPMP.Instance.SetHP)
                    {
                        if (isQuickHP)
                        {
                            Debug.Log("HP");
                            ItemCnt--;
                            countText.text = itemCnt.ToString();
                            QuickSlotHP.Instance.ItemCount--;
                            QuickSlotHP.Instance.CountText.text = ItemCnt.ToString();
                            Debug.Log("QuickSlotHP.Instance.ItemCount :: " + QuickSlotHP.Instance.ItemCount);
                            if (ItemCnt == 0 && QuickSlotHP.Instance.ItemCount == 0)
                            {
                                Debug.Log("HP 0개입니다.");
                                ClearSlot();
                            }  
                        }
                        
                    }
                    
                }
                if (QucikSlotHPMP.Instance.SetHPMP_inMP)
                {
                    if (QucikSlotHPMP.Instance.SetMP)
                    {
                        if (isQuickMP)
                        {
                            Debug.Log("MP");
                            ItemCnt--;
                            countText.text = itemCnt.ToString();
                            QucikSlotMP.Instance.ItemCount--;
                            QucikSlotMP.Instance.CountText.text = ItemCnt.ToString(); 
                            Debug.Log("QucikSlotMP.Instance.ItemCount :: " + QucikSlotMP.Instance.ItemCount);
                            if (ItemCnt == 0 && QucikSlotMP.Instance.ItemCount == 0)
                            {
                                Debug.Log("MP 0개");
                                ClearSlot();
                            }  
                        }
                        
                    }
                    
                }

            }
            else if (ItemCnt == 0 && QuickSlotHP.Instance.ItemCount == 0)
            {
                if (QuickSlotHP.Instance.ItemCount == 0)
                {
                    if (QucikSlotHPMP.Instance.SetHPMP_inHP)
                    {
                        ClearSlot();
                        QucikSlotHPMP.Instance.SetHPMP_inHP = false;
                        Debug.Log("채력퀵슬롯에서 삭제함");
                    }
                    else
                    {
                        Debug.Log("HP에러");
                    }
                }
                else if (QucikSlotMP.Instance.ItemCount == 0)
                {
                    if (QucikSlotHPMP.Instance.SetHPMP_inMP)
                    {
                        ClearSlot();
                        QucikSlotHPMP.Instance.SetHPMP_inMP = false;
                        Debug.Log("마나퀵슬롯에서 삭제함");   
                    }
                    
                }
                else
                {
                    Debug.Log("에러");
                }
                
            }
        }
        else if (ps.HP < ps.MaxHP && !FullHP && potionType == AllEnum.PotionType.HP)
        {
            if (action != null) action.Invoke();
            if (img != null)
            {
                ItemCnt--;
                countText.text = itemCnt.ToString();
                QuickSlotHP.Instance.ItemCount--;
                QuickSlotHP.Instance.CountText.text = ItemCnt.ToString();
            }
            else
            {
                return;
            }
            
            if (ItemCnt == 0 && QuickSlotHP.Instance.ItemCount == 0)
            {
                ClearSlot();
            }
        } 
        else if (ps.MP < ps.MaxMP && !FullMP && potionType == AllEnum.PotionType.MP)
        {
            if (action != null) action.Invoke();
            if (img != null)
            {
                ItemCnt--;
                countText.text = itemCnt.ToString();
                QucikSlotMP.Instance.ItemCount--;
                QucikSlotMP.Instance.CountText.text = ItemCnt.ToString();
            }
            else
            {
                return;
            }
            
            if (ItemCnt == 0 && QucikSlotMP.Instance.ItemCount == 0)
            {
                ClearSlot();
            }
        }  
        
        
    }

    public void ClearSlot()
    {
        if (TypeNumber == 3)
        {
            if (QuickSlotHP.Instance.Img != null && QucikSlotHPMP.Instance.SetHPMP_inHP && QucikSlotHPMP.Instance.SetHP)
            {
                Debug.Log("HP");
                QuickSlotHP.Instance.ItemCount = 0;
                QuickSlotHP.Instance.Img.sprite = null;
                QuickSlotHP.Instance.CountText.text = string.Empty;
                QuickSlotHP.Instance.CountText.gameObject.SetActive(false);
                QucikSlotHPMP.Instance.SetHPMP_inHP = false;
                QucikSlotHPMP.Instance.SetHP = false;
                
            }
            else if (QucikSlotMP.Instance.Img != null && QucikSlotHPMP.Instance.SetHPMP_inMP && QucikSlotHPMP.Instance.SetMP)
            {
                Debug.Log("MP");
                QucikSlotMP.Instance.ItemCount = 0;
                QucikSlotMP.Instance.Img.sprite = null;
                QucikSlotMP.Instance.CountText.text = string.Empty;
                QucikSlotMP.Instance.CountText.gameObject.SetActive(false);
                QucikSlotHPMP.Instance.SetHPMP_inMP = false;
                QucikSlotHPMP.Instance.SetMP = false;
            }
            else
            {
                Debug.Log("에러");
            }
        }
        if (TypeNumber == 2)
        {
            QucikSlotMP.Instance.ItemCount = 0;
            QucikSlotMP.Instance.Img.sprite = null;
            QucikSlotMP.Instance.CountText.text = string.Empty;
            QucikSlotMP.Instance.CountText.gameObject.SetActive(false);
        }
        else if (TypeNumber == 1)
        {
            QuickSlotHP.Instance.ItemCount = 0;
            QuickSlotHP.Instance.Img.sprite = null;
            QuickSlotHP.Instance.CountText.text = string.Empty;
            QuickSlotHP.Instance.CountText.gameObject.SetActive(false);
        }

        itemCnt = 0;
        action = null;
        img.sprite = null;
        countText.text = string.Empty;
        itemType = AllEnum.ItemType.None;
        potionType = AllEnum.PotionType.None;
    }
}


