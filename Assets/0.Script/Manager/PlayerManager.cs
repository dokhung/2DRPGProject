using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
public struct PlayerStat
{
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int Att;
    public int Skill;
    public int Exp;
    public int MaxExp;
    public int Level;
    public int Gold;
    public int Def;

    public PlayerStat(int hp,int mp, int att, int skill, int exp, int maxEXP, int level, int gold, int DEF)
    {
        HP = hp;
        MaxHP = hp;
        MP = mp;
        MaxMP = mp;
        Att = att;
        Skill = skill;
        Exp = exp;
        MaxExp = maxEXP;
        Level = level;
        Gold = gold;
        Def = DEF;
    }
}
public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerStat playerStat;
    // 시작 공격력은 2로 지정
    
    private void Start()
    {
        StatSetting();
    }
    
    void StatSetting()
    {
        playerStat = new PlayerStat(50, 30, 5, 1, 0, 10, 1, 0, 1);
    }

    private void Update()
    {
        LevelUp();
    }

    void LevelUp()
    {
        if (UIManager.instance.SetEXP == UIManager.instance.SetMAXEXP || UIManager.instance.SetEXP >= UIManager.instance.SetMAXEXP)
        {
            // playerStat.Level += 1;
            // playerStat.Exp = 0;
            // playerStat.MaxExp += 10;
            // playerStat.MaxHP += 10;
            // playerStat.MaxMP += 1;
            // playerStat.HP = playerStat.MaxHP;
            // playerStat.MP = playerStat.MaxMP;
            // playerStat.Att += 2;
            // playerStat.Def += 1;
            UIManager.instance.SetLevel += 1;
            UIManager.instance.SetEXP = 0;
            UIManager.instance.SetMAXEXP += 10;
            UIManager.instance.SetMAXHP += 10;
            UIManager.instance.SetMAXMP += 5;
            UIManager.instance.SetHP = UIManager.instance.SetMAXHP;
            UIManager.instance.SetMP = UIManager.instance.SetMAXMP;
            UIManager.instance.SetAtt += 2;
            UIManager.instance.SetDef += 1;
             
        }
    }
}
