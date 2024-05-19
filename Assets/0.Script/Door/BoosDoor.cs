using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosDoor : MonoBehaviour
{
    private float targetPosition = 0;
    private float speed = 2f;
    private bool isMoving = false;
    public Transform InitPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isMoving)
            {
                isMoving = true;
                StartCoroutine(Init());
            }
        }
    }
    

    void Update()
    {
        if (isMoving)
        {
            targetPosition += speed * Time.deltaTime;
            transform.Translate(0, speed * Time.deltaTime, 0);
            if (targetPosition >= 2.1f)
            {
                isMoving = false;
            }
        }
    }
    
    IEnumerator Init()
    {
        yield return new WaitForSeconds(3f);
        transform.position = InitPoint.position;
        targetPosition = 0; 
        isMoving = false;
    }
}
