using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Text cntTxt;

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
                    Count--;
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
}
