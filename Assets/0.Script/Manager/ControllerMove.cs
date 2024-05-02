using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{     
    Right,
    Left,
    Jump,
    
    
    End
}

public class ControllerMove : MonoBehaviour
{
    
    Vector3 vec = Vector3.zero;

    Dir myDir = Dir.End;
    
    [Header("점프 횟수")]
    private float JumpCount = 0;

    private Rigidbody2D rigid;
    [Header("JumpPower")]
    public float jumpPower = 4;
    [Header("점프를 하였는가?")]
    public Animator anim;
    //public Transform SkinObj;
    
    // 화살을 발사한다.
    public GameObject ArrowObject;
    // public bool AttackArrow = false;
    public Transform ArrowPrefablocation;

    public GameObject ArrowBtn;
    
    public GameObject BowBody;
    //발사 유무
    public bool ArrowAttack = false;
    
    //무기
    public GameObject Sword;
    
    public bool isAttacking = false;
    
    
    //범위
    public float detectionRadius = 0.4f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (myDir)
        {
            case Dir.Right:
                vec.x = 5;
                transform.Translate(vec * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                MoveAnim();
                break;
            case Dir.Left:
                vec.x = -5;
                transform.Translate(vec * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                MoveAnim();
                break;
            // default: anim.SetBool("Run",false);
            //     break;
        }
    }
    public void SetDir(Dir dir)
    {
        myDir = dir ;
    }

    public void Jump()
    {
        if (JumpCount == 0)
        {
            JumpCount += 1;
            //anim.SetTrigger("Jump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else
        {
            jumpPower = 0;
        }
    }

    public void AttackAnim()
    {
        if (!isAttacking)
        {
            anim.SetBool("SwordAttack",true);
            StartCoroutine(StartAttackCooldown());
        }

        IEnumerator StartAttackCooldown()
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
            anim.SetBool("SwordAttack",false);
        }
    }

    public void E_Btn()
    {
        if (PlayerManager.Instance.playerStat.MP > 0)
        {
            if (gameObject.transform.localScale.x > 0)
            {
                BowAttack();

            }
            else if (gameObject.transform.localScale.x < 0)
            {
                BowAttack();
            } 
        }
    }

    public void BowAttack()
    {
        if (!ArrowAttack)
        {
            BoolArrow();
            Invoke("NewArrowAttack",0.7f);
        }
    }
    
    public void BoolArrow()
    {
        UIManager.Instance.SetMP -= 1;
        ArrowAttack = true;
        Sword.SetActive(false);
        BowBody.SetActive(true);  
        anim.SetBool("BowAttack",true);
    }

    public void NewArrowAttack()
    {
        GameObject obj;
        obj = Instantiate(ArrowObject,ArrowPrefablocation.position,ArrowPrefablocation.rotation);
        obj.SetActive(true);
        ArrowBtn.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpCount = 0;
            jumpPower = 4;
        }
    }

    public void MoveAnim()
    {
        anim.SetBool("Dash",true);
    }
}