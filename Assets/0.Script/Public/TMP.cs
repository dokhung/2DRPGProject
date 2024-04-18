using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP : MonoBehaviour
{
    // 원래 스케일 값을 저장하기 위한 변수
    private Vector3 originalScale;

    private void Start()
    {
        // 시작할 때 원래 스케일 값을 저장
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // 항상 원래 스케일로 설정
        transform.localScale = originalScale;
    }
}

