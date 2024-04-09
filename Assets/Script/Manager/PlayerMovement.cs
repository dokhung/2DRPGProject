using System;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
    {
        public bool LeftMove = false;
        public bool RightMove = false;
        
        Vector3 moveVelocity = Vector3.zero;
        private float moveSpeed = 5;
        private SpriteRenderer sp;

        private void Update()
        {
            if (LeftMove)
            {
                // transform.localScale = new Vector3(-5, 5, 1);
                moveVelocity = new Vector3(-5, 5, 1);
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;
            }
            else if (RightMove)
            {
                //transform.localScale = new Vector3(5, 5, 1);
                moveVelocity = new Vector3(5, 5, 1);
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;
            }
            
        }

}