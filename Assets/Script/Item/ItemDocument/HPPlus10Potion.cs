using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPlus10Potion : Item
{
    public override void UseItem()
    {
        int hp = PlayerManager.Instance.PlayerStatInfo.HP;
        int maxHP = PlayerManager.Instance.PlayerStatInfo.MaxHP;
        if (hp + 10 > maxHP)
        {
            int amountToHeal = maxHP - hp;
            PlayerManager.Instance.PlayerStatInfo.HP += amountToHeal;
        }
        else
        {
            PlayerManager.Instance.PlayerStatInfo.HP += 10;
        }
    }
}
