using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class PlayerManager : Singleton<PlayerManager>
{
    
    // public Player player;
    public Stat.PlayerStat PlayerStatInfo;
    // 시작 공격력은 2로 지정
    
    private void Start()
    {
        StatSetting();
    }
    
    public void StatSetting()
    {
        PlayerStatInfo = new Stat.PlayerStat(50, 30, 5, 1, 0, 10, 1, 0, 1);
        
    }

    private void Update()
    {
        // MinMaxAtt();
        LevelUp();
    }

    public void LevelUp()
    {
        if (PlayerStatInfo.ExpVal == PlayerStatInfo.MaxExpVal || PlayerStatInfo.ExpVal >= PlayerStatInfo.MaxExpVal)
        {
            PlayerStatInfo.Level += 1;
            PlayerStatInfo.ExpVal = 0;
            PlayerStatInfo.MaxExpVal += 10;
            PlayerStatInfo.MaxHP += 10;
            PlayerStatInfo.MaxMP += 1;
            PlayerStatInfo.HP = PlayerStatInfo.MaxHP;
            PlayerStatInfo.MP = PlayerStatInfo.MaxMP;
            PlayerStatInfo.Att += 2;
            PlayerStatInfo.Def += 1;
            
            
        }
    }
}
