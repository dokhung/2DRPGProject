using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoss : MonoBehaviour
{
    public Transform InBossTr;
    private bool InPotal = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("보스한테 갈수있음 위를 누르세요");
            InPotal = true;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && InPotal)
        {
            InputManager.Instance.transform.position = InBossTr.transform.position;
            Debug.Log("보스와 접견 성능");
        }
    }
}
