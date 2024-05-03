using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerButton : MonoBehaviour
{
     public ControllerMove PlayerInputBtn;
     public Dir Right = Dir.End;
     public void MoveX()
     {
          PlayerInputBtn.SetDir(Right);
     }
     public void PointUp() 
     {
          PlayerInputBtn.SetDir(Dir.End);
     }

     public void Jump()
     {
          PlayerInputBtn.Jump();
     }

     public void Attack()
     {
          PlayerInputBtn.AttackAnim();
     }

     public void ArrowAttack()
     {
          PlayerInputBtn.E_Btn();
     }
}
