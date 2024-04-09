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


    public enum PotionItemList
    {
        HPPlus10,
        HPPlus100,
        MP10,
        MP100,
        HPMPPlus10,
        HPMPPlus100,
        
        None
       
    }

    public enum PotionType
    {
        HP,
        MP,
        HPMP,
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
