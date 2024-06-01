using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    public SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ArrowAttack();
    }

    public void ArrowAttack()
    {
        // 삼향연산자로 바꿔야되는데.
        float direction = InputManager.Instance.gameObject.transform.localScale.x;
        if (direction <= 0)
            RightArrow();
        else
            LeftArrow(); 
    }

    public void RightArrow()
    {
        gameObject.transform.Translate(10*speed*Time.deltaTime,0,0);
        Invoke("Stop",0.5f);
    }

    public void LeftArrow()
    {
        sp.flipX = true;
        gameObject.transform.Translate(-10*speed*Time.deltaTime,0,0);
        Invoke("Stop",0.5f);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
        InputManager.Instance.ArrowAttack = false;
        InputManager.Instance.ArrowBtn.SetActive(true);
        InputManager.Instance.BowBody.SetActive(false);
        InputManager.Instance.Sword.SetActive(true);
        InputManager.Instance.anim.SetBool("BowAttack",false);
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            int damege = PlayerManager.Instance.Att;
            NomalMonsterAI NomalMonster = other.collider.GetComponent<NomalMonsterAI>();
            //NomalMonster.TakeDamage(damege);
            InputManager.Instance.ArrowBtn.SetActive(true);
            InputManager.Instance.BowBody.SetActive(false);
            InputManager.Instance.Sword.SetActive(true);
            InputManager.Instance.ArrowAttack = false;
            InputManager.Instance.anim.SetBool("BowAttack",false);
            Destroy(gameObject);
        }
    }
}