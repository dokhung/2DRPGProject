using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongScriptButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdTime = 1.0f;
    private bool isPressed = false;
    private float pressedTime = 0.0f;
    public Image img;
    public int TypeNumber = 0;
    int SlotIndex { get; set; }
    private Slot[] slots;

    // 슬롯의 인덱스 정보를 전달하는 함수
    private void Start()
    {
        slots = FindObjectsOfType<Slot>();
        SlotIndex = transform.parent.GetSiblingIndex();
        img = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pressedTime = Time.time;
        StartCoroutine(LongPressCoroutine());
    }
    

    private IEnumerator LongPressCoroutine()
    {
        yield return new WaitForSeconds(holdTime);

        if (isPressed)
        {
            GetItemInfo();
            isPressed = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        StopCoroutine(LongPressCoroutine());
    }

    private void Update()
    {
        if (isPressed && Time.time - pressedTime > holdTime)
        {
            GetItemInfo();
            isPressed = false;
        }
    }

    private void GetItemInfo()
    {
        Slot slot = slots[SlotIndex];
        Slot slotScript = GetComponentInParent<Slot>();
        QucikInventoryManager.Instance.AddPotionInfo(img.sprite, slotScript.potionType, slotScript.itemCnt, slotScript.PotionName, slotScript.TypeNumber,SlotIndex,slot);
    }
}

