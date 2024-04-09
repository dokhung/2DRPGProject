using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputManager : Singleton<InputManager>
{
    float x = 0;
    float y = 0;
    public float speed = 2;
    public float jumpPower = 4;
    public int jumpCount = 0;
    Vector3 vec = Vector3.zero;
    public bool isJump = false;
    public bool isAttacking = false;
    

    private Rigidbody2D rigid;
    // private Vector3 scaleVec = Vector3.one;
    private SpriteRenderer sp;
    public Animator anim;
    
    private Stat.PlayerStat Stat;
    public bool IsHit = false;
    public bool PlayerIsHit = false;
    
    // 화살 발사용
    [Header("화살 발사")]
    public GameObject ArrowObject;
    public Transform ArrowPrefablocation;

    public GameObject BowBody;
    //발사 유무
    public bool ArrowAttack = false;
    public GameObject ArrowBtn;
    
    //무기
    public GameObject Sword;
    
    
    //범위
    public float detectionRadius = 0.4f;
    // 피격
    public GameObject HeadColor;
    //private SpriteRenderer HeadCol;
    
    
    
    
    

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        BowBody.SetActive(false);
        //HeadCol = HeadColor.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        vec.x = x;
        vec.y = y;
        x = Input.GetAxisRaw("Horizontal");
        if (x > 0)
        {
            anim.SetBool("Dash",true);
            // SpriteRenderer HeadCol = HeadColor.gameObject.GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (x < 0) {
            anim.SetBool("Dash",true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            anim.SetBool("Dash",false);
        }
        y = Input.GetAxisRaw("Vertical");
        transform.Translate(vec.normalized * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount == 0)
            {
                jumpCount += 1;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                //anim.SetTrigger("Jump");
                jumpCount++;
            }
            else if (jumpCount >= 1 && jumpCount <= 2)
            {
                if (PlayerManager.instance.PlayerStatInfo.MP >= 1)
                {
                    PlayerManager.instance.PlayerStatInfo.MP -= 1;
                    jumpCount += 1;
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    //anim.SetTrigger("Jump");
                    jumpCount++;
                }
                else
                {
                    return;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("인위적으로 공격력을 증가시킴");
            PlayerManager.instance.PlayerStatInfo.Att += 10;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerManager.instance.PlayerStatInfo.HP -= 10;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerManager.instance.PlayerStatInfo.MP -= 10;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("인위적으로 공격력을 감소");
            PlayerManager.instance.PlayerStatInfo.Att = 5;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("인위적으로 공격력을 0으로");
            PlayerManager.instance.PlayerStatInfo.Att = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rigid.AddForce(Vector2.right * 10,ForceMode2D.Impulse);
            PlayerManager.instance.PlayerStatInfo.MP -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            E_Btn();
        }
        else if (!isAttacking && Input.GetKey(KeyCode.Q))
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
        
        // 공격범위
        CheckDetection();
        
    }

    public void E_Btn()
    {
        if (PlayerManager.instance.PlayerStatInfo.MP > 0)
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
        PlayerManager.instance.PlayerStatInfo.MP -= 1;
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
    
    // 몬스터를 탐지하는 함수
    private void CheckDetection()
    {
        // 현재 오브젝트 주위에 있는 콜라이더 투디들을 가져 옵니다.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        // 감지된 각 콜라이더의 대해서 반복문을 실행합니다.
        foreach (Collider2D collider in colliders)
        {
            // 몬스터만 가져옴
            LV1_Monster monsterScript = collider.GetComponent<LV1_Monster>();
            // 레벨1의 몬스터가 있을때 
            // 차후 모든 몬스터의 경우로 수정하고자 합니다.
            if (monsterScript != null)
            {
                Vector3 directionToMonster = collider.transform.position - transform.position;
                // 플레이어의 방향을 결정하고 localScal.x는 플레이어의 x스케일을 나타내며, 이 값이
                //음수인지 양수인지에 따라 방향을 결정합니다.
                float playerDirection = Mathf.Sign(transform.localScale.x * -1);
                
                //Vector3.Dot은 두 벡터 간의 내적을 계산 합니다.
                //벡터의 방향만 남겨두고 크기를 1로 정규화한 벡터 입니다.
                float dotProduct = Vector3.Dot(directionToMonster.normalized, 
                    // new Vector3(playerDirection, 0, 0)은 플레이어의 방향을 나타내는 벡터입니다.
                    new Vector3(playerDirection, 0, 0));
                int RandomNum = Random.Range(1, 5);
                int MinDamege = PlayerManager.instance.PlayerStatInfo.Att - RandomNum;
                int MaxDamege = PlayerManager.instance.PlayerStatInfo.Att + RandomNum;
                int damege = Random.Range(MinDamege, MaxDamege);
                
                 //내적 결과가 양수(두 벡터의 방향이 일치함)이고, 플레이어가 공격 상태인 경우에만 실행합니다.
                 if (dotProduct > 0 && anim.GetBool("SwordAttack") && !IsHit)
                 {
                     IsHit = true;
                     monsterScript.TakeDamage(damege);
                     Invoke("TimeHit",1.5f);
                 }
            }
        }
    }

    public void TimeHit()
    {
        IsHit = false; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            PickUp(other);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            int PlusMoney = Random.Range(1, 10);
            PlayerManager.instance.PlayerStatInfo.Money += PlusMoney;
            Destroy(other.gameObject);
        }
    }

    public void PickUp(Collision2D other)
    { 
        Item itemComponent = other.gameObject.GetComponent<Item>();
        if (itemComponent.itemType == AllEnum.ItemType.Etc)
        {
            InventoryManager.instance.Registration(itemComponent);
            Destroy(other.gameObject); 
        }
        else
        {
            Debug.Log("장비템이요");
        }
    }
}
