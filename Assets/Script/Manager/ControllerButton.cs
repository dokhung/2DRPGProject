using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerButton : MonoBehaviour
{
     public ControllerMove PlyaerInputBtn;
     public Dir Right = Dir.End;
     
     


     public void MoveX()
     {
          PlyaerInputBtn.SetDir(Right);
     }
     public void PointUp() 
     {
          PlyaerInputBtn.SetDir(Dir.End);
     }

     public void Jump()
     {
          PlyaerInputBtn.Jump();
     }

     public void Attack()
     {
          PlyaerInputBtn.Attack();
     }

     public void ArrowAttack()
     {
          PlyaerInputBtn.ArrowAttack();
     }
}
