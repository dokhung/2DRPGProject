using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP10Potion : Item
{
    public override void UseItem()
    {
        int Mp = PlayerManager.Instance.PlayerStatInfo.MP;
        int maxMP = PlayerManager.Instance.PlayerStatInfo.MaxMP;
        int Plusnum = 10;
        if (Mp + Plusnum > maxMP)
        {
            int amountToHeal = maxMP - Mp;
            PlayerManager.Instance.PlayerStatInfo.MP += amountToHeal;
        }
        else
        {
            PlayerManager.Instance.PlayerStatInfo.MP += 10;
        }
    }
}
