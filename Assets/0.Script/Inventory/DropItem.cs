using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

    public AllEnum.ItemType itemType;
    
    public Sprite Sprite
    {
        get { return sr.sprite; }
    }
}