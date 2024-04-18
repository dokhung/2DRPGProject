using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamegeNumber : MonoBehaviour
{
    public Text DamegeInt;


    private void Update()
    {
        DamegeInt.text = PlayerManager.Instance.PlayerStatInfo.Att.ToString();
    }
}
