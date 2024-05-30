using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnum : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Equip, // 장비
        Etc // 소비
    }

    public enum EquipType
    {
        Sword,
        Arrow,
        
        None
    }

    public enum PotionType
    {
        HP,
        MP,
        ALL,
        None
    }
    

    public enum NomalMonsterStateBT
    {
        State_Idle, // 재자리 좌우 보면서 순찰
        State_Guard, // 경계순찰
        State_Combat, // 공격
        State_Chase, // 추적
        State_Return, // 추적 포기
        
        
        
        
        None
    }
    
    public enum HIMonsterStateBT
    {
        State_Idle, // 중지
        State_Running,
        State_Attack,
        State_Chase,
        
        
        
        
        None
    }
    
    public enum BossMonsterStateBT
    {
        State_Idle, //
        State_Running,
        State_Attack,
        State_Chase,
        
        
        
        
        None
    }
    
    
}
