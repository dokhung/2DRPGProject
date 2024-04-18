using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarrningPost : MonoBehaviour
{
    public GameObject WarrningpostImg;

    private void Start()
    {
        WarrningpostImg.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WarrningpostImg.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WarrningpostImg.SetActive(false);
        }
    }
}
