using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gold :MonoBehaviour
{
    public TMP_Text Plus;
    public TMP_Text AddGoldNumber;
    public int addGoldNum = 0;
    private Rigidbody2D rigid2d;

    private void Start()
    {
        Plus.gameObject.SetActive(false);
        AddGoldNumber.gameObject.SetActive(false);
        rigid2d = GetComponent<Rigidbody2D>();
        
        // isTrigger를 해제합니다.
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // isTrigger를 다시 True로 설정합니다.
            GetComponent<Collider2D>().isTrigger = true;
            
            
            addGoldNum += Random.Range(2, 20);
            AddGoldNumber.text = addGoldNum.ToString();
            Plus.gameObject.SetActive(true);
            AddGoldNumber.gameObject.SetActive(true);
            PlayerManager.Instance.PlayerStatInfo.Money += addGoldNum;
            Invoke("Add",0.1f);
        }
    }

    public void Add()
    {
        Plus.gameObject.SetActive(false);
        AddGoldNumber.gameObject.SetActive(false);
        gameObject.IsDestroyed();

    }
}
