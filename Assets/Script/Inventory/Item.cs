using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Text cntTxt;

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
    public Sprite sprite 
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
}
