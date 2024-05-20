using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InBoss : MonoBehaviour
{
    public Transform InBossTr;
    private bool InPotal = false;

    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InPotal = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && InPotal)
        {
            InputManager.Instance.transform.position = InBossTr.transform.position;
        }
    }
}
