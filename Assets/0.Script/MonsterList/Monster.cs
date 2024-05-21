using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private AllEnum.BTNodeState State = AllEnum.BTNodeState.None;
    
    // 일반
    public struct MonsterStat
    {
        // public int level;
        public int hp;
        public int maxHP;
        public int att;
        public int giveExp;
        public int giveMoney;
        public MonsterStat(int Level)
        {
            this.hp = 10 * Level;
            this.maxHP = 10 * Level;
            this.att = 1 * Level;
            this.giveExp = 10 * Level;
            this.giveMoney = 10 * Level;
        }
    }




    public abstract void InitializeMonsterStat();
    public abstract void DropItems();
    // public abstract void MonsterHPInit();

}
