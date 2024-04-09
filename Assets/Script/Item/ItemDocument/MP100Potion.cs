using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP100Potion : Item
{ 
    public override void UseItem()
    {
        int Mp = PlayerManager.Instance.PlayerStatInfo.MP;
        int maxMP = PlayerManager.Instance.PlayerStatInfo.MaxMP;
        if (Mp + 100 > maxMP)
        {
            int amountToHeal = maxMP - Mp;
            PlayerManager.Instance.PlayerStatInfo.MP += amountToHeal;
        }
        else
        {
            PlayerManager.Instance.PlayerStatInfo.MP += 100;
        }
    }
}
