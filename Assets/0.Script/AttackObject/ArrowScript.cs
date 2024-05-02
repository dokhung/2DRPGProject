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
        if (InputManager.Instance.gameObject.transform.localScale.x <= 0)
        {
            RightArrow();
        }
        else if (InputManager.Instance.gameObject.transform.localScale.x >= 0)
        {
            LeftAtrow();
        }
    }

    public void RightArrow()
    {
        gameObject.transform.Translate(10*speed*Time.deltaTime,0,0);
        Invoke("Stop",0.5f);
    }

    public void LeftAtrow()
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
            int damege = PlayerManager.Instance.playerStat.Att;
            LV1_Monster monsterScript = other.collider.GetComponent<LV1_Monster>();
            monsterScript.TakeDamage(damege);
            InputManager.Instance.ArrowBtn.SetActive(true);
            InputManager.Instance.BowBody.SetActive(false);
            InputManager.Instance.Sword.SetActive(true);
            InputManager.Instance.ArrowAttack = false;
            InputManager.Instance.anim.SetBool("BowAttack",false);
            Destroy(gameObject);
        }
    }
}
