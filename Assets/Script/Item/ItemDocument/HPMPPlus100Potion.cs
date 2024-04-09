using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMPPlus100Potion : Item
{
    int Plusnum = 100;
    public override void UseItem()
    {
        int Hp = PlayerManager.Instance.PlayerStatInfo.HP;
        int MaxHP = PlayerManager.Instance.PlayerStatInfo.MaxHP;
        int Mp = PlayerManager.Instance.PlayerStatInfo.MP;
        int MaxMP = PlayerManager.Instance.PlayerStatInfo.MaxMP;
        
        PlayerManager.Instance.PlayerStatInfo.HP = Mathf.Clamp(Hp + Plusnum, 0, MaxHP);
        PlayerManager.Instance.PlayerStatInfo.MP = Mathf.Clamp(Mp + Plusnum, 0, MaxMP);
    }
}
