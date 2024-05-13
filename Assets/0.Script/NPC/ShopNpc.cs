using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    private void OnMouseDown()
    {
        UIManager.Instance.ShopDialog.SetActive(true);
    }
}
