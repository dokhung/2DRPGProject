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
    

    public enum State
    {
        Idle,
        BeShot,
        Attack,
        Trace,
        
        
        None
    }
    
    
}
