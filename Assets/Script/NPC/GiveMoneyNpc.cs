using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveMoneyNpc : MonoBehaviour
{
    public TMP_Text textDisplay; // TMP Text 컴포넌트를 가리키는 변수
    public string fullText = "Give Money"; // 전체 표시할 문자열
    private string currentText = ""; // 현재 표시할 문자열
    public GameObject Money;
    public Transform Target;

    private void Start()
    {
        textDisplay.text = ""; // 시작 시 텍스트를 비움
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj;
            obj = Instantiate(Money, Target.position, Target.rotation);
            obj.SetActive(true);
            StartCoroutine(DisplayText());
        }
    }

    IEnumerator DisplayText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1); // 전체 문자열에서 i번째 글자까지 자름
            textDisplay.text = currentText; // TMP Text에 현재 표시할 문자열 설정

            yield return new WaitForSeconds(0.1f); // 0.1초 대기
        }
    }
}


