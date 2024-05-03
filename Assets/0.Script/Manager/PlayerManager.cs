using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Stat")]
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int Att;
    public int Exp;
    public int MaxExp;
    public int Level;
    public int Gold;
    public int Def;

    private void Start()
    {
        Level = 1;
        HP = 50;
        MaxHP = 50;
        MP = 30;
        MaxMP = 30;
        Att = 5;
        Exp = 0;
        MaxExp = 10;
        Gold = 0;
        Def = 5;
    }

    private void Update()
    {
        LevelUp();
    }

    void LevelUp()
    {
        if (Exp == MaxExp || UIManager.instance.SetEXP >= UIManager.instance.SetMAXEXP)
        {
            Level += 1;
            Exp = 0;
            MaxExp += 10;
            MaxHP += 10;
            MaxMP += 1;
            HP = MaxHP;
            MP = MaxMP;
            Att += 2;
            Def += 1;
        }
    }
}
