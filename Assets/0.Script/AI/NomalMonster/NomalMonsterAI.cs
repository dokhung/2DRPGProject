using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NomalMonsterAI : Monster
{
    /*
     * 설명
     * 몬스터는 Idle상태로 시작을 한다.
     * Idle상태는 중립상태중 하나로 취급한다.
     * Idle특징 : 이동이 없는 재자리 순찰
     * Guard특징 : 중립상태인것은 같지만 Idle과 다르게 움직임이 있다.
     */
    public AllEnum.NomalMonsterStateBT nomalmonsterai = AllEnum.NomalMonsterStateBT.State_Idle;
    public Animator anime;
    
    //코루틴
    private Coroutine idleCoroutine;
    private Coroutine guardCoroutine;
    
    
    //범위
    private Vector2 Destination;
    private float Distance;
    
    // 몬스터의 플레이어 발견
    private Vector2 FindPlayer;
    public float searchRange = 10f;
    
    //State Bool Type
    public bool isInRange = false;
    public bool Fight = false;
    public bool isSuspicious = false; // 수상함
    public int IdleCount = 0;
    public bool TrueMove = false;
    public int GuardCount = 0;
    
    
    
    //데미지 스킨
    public TMP_Text monsterDamageText;
    
    //드롭 아이템 리스트
    [Header("드롭아이템List")]
    public GameObject[] dropItems;

    private Rigidbody2D rigidBody;
    public Transform initialPosition;
    
    // 몬스터 정보
    private SpriteRenderer spriteRenderer;
    private Monster.MonsterStat monsterStat;
    
    // 이동 관련 변수
    private float MoveSpeed = 1f;
    private float MovePoint = 0f;
    private void Start()
    {
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.gameObject.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable() // OnEnable은 Start()보다 라이프사이클이 더 빠름
    {
        InitializeMonsterStat();
    }
    
    public override void InitializeMonsterStat()
    {
        monsterStat = new Monster.MonsterStat(PlayerManager.Instance.Level);
    }

    private void Update()
    {
        OnBehaviors();
    }

    private void OnBehaviors()
    {
        switch (nomalmonsterai)
        {
            case AllEnum.NomalMonsterStateBT.State_Idle:
                // State_Idle : 재자리순찰
                IdleNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Guard:
                // State_Guard : 이동순찰
                GuardNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Combat:
                // State_Combat : 공격
                CombatNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Chase:
                // State_Chase : 추적
                ChaseNode();
                break;
        }
    }

    // 해당 함수로 상태를 변경시킴
    private void ChangeState(AllEnum.NomalMonsterStateBT newState)
    {
        nomalmonsterai = newState;
        OnBehaviors();
    }

    public void IdleNode()
    {
        Idle();
    }
    public void GuardNode()
    {
        Guard();
    }
    public void CombatNode()
    {
        Combat();
    }
    public void ChaseNode()
    {
        Chase();
    }
    
    public void Idle() // 재자리 경계
    {
        //플레이어가 범위에 없는동안 무한 코투린Idle 상태
        if (!isInRange)
        {
            // 3초에 왼쪽 오른쪽
            if (idleCoroutine == null)
            {
                idleCoroutine = StartCoroutine(IdleTimeState());
            }  
        }
    }
    public void Guard() // 경계 이동
    {
        switch (isInRange)
        {
           case true :
               ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
               break;
           case false :
               MoveGuard();
               break;
        }
    }
    public void Combat() // 배틀
    {
        Debug.Log("Combat");
        if (isInRange)
        {
            Debug.Log("플레이어를 공격할수있는 공격범위내에 들어갔습니다");
            if (Fight)
            {
                NomalAttack();
            }
            else
            {
                Debug.Log("마법공격가능 범위");
            }
        }
    }
    public void Chase()
    {
        Debug.Log("Chase");
    }

    public void Death() // 죽음
    {
        Debug.Log("Death");
        anime.SetTrigger("Death");
    }

    public void MoveGuard()
    {
        int MoveNumber_PMin = Random.Range(5, 10);
        int MoveNumber_PMax = Random.Range(11, 20);
        int MoveNumber_MMin = Random.Range(-5, -10);
        int MoveNumber_MMax = Random.Range(-11, -20);
        
        int ChangeInt = Random.Range(3, 6);
        switch (TrueMove)
        {
            case true :
                GuardCount += 1;
                spriteRenderer.flipX = true;
                anime.SetTrigger("Run");
                MovePoint += MoveSpeed * Time.deltaTime;
                transform.Translate(MoveSpeed * Time.deltaTime,0,0);
                if (MovePoint >= Random.Range(MoveNumber_PMin,MoveNumber_PMax))
                {
                    Debug.Log("이동 변경");
                    TrueMove = false;
                    
                }
                break;
            case false :
                GuardCount += 1;
                spriteRenderer.flipX = false;
                anime.SetTrigger("Run");
                MovePoint -= MoveSpeed * Time.deltaTime;
                transform.Translate(-MoveSpeed * Time.deltaTime,0,0);
                if (MovePoint <= Random.Range(MoveNumber_MMin,MoveNumber_MMax))
                {
                    Debug.Log("이동 변경");
                    TrueMove = true;
                }
                break;
        }
        Debug.Log("ChangeInt :: "+ChangeInt);
        if (GuardCount >= ChangeInt)
        {
            Debug.Log("이동거리 충분 완료");
            ChangeState(AllEnum.NomalMonsterStateBT.State_Idle);
            GuardCount = 0;
            Debug.Log("GuardCount초기화완료");
        }
    }

    public void NomalAttack()
    {
        
    }

    public void Patrol()
    {
        Debug.Log("Patrol");
    }

    

    IEnumerator IdleTimeState()
    {
        while (true)
        {
            if (IdleCount >= Random.Range(3,6))
            {
                ChangeState(AllEnum.NomalMonsterStateBT.State_Guard);
                IdleCount = 0;
                Debug.Log("Idle초기화");
                yield break;  // 현재 코루틴을 종료합니다.
            }
            // 3초마다 행동 변경
            yield return new WaitForSeconds(3f);
            // flipX 속성 반전
            spriteRenderer.flipX = !spriteRenderer.flipX;
            // 행동을 한 후 cnt 증가
            IdleCount += 1;
        }
    }
    // public void SearchForTarget()
    // {
    //     Debug.Log("SearchForTarget");
    //     Destination = GameObject.FindGameObjectWithTag("Player")
    //         .transform.position;
    //     Distance = Vector2.Distance(gameObject.transform.position,
    //         Destination);
    //     if (Distance < 10)
    //     {
    //         Debug.Log("Combat");
    //         ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
    //     }
    // }
    
    public void TakeDamege(int damege)
    {
        monsterDamageText.gameObject.SetActive(true);
        anime.SetTrigger("Hit");
        monsterStat.hp -= damege;
        monsterDamageText.text = damege.ToString();
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.transform.DOMoveY(1, 1).SetRelative();
        Invoke("ResetDamageText", 1f);
        if (monsterStat.hp <= 0)
        {
            DropItems();
        }
    }
    
    public override void DropItems()
    {
        GameObject ItemToDrop = dropItems[Random.Range(0, dropItems.Length)];
        GameObject DroppedItem = Instantiate(ItemToDrop, transform.position, Quaternion.identity);
        Rigidbody2D ItemRigidBody = DroppedItem.GetComponent<Rigidbody2D>();
        ItemRigidBody.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        anime.SetBool("Death", true);
        Invoke("Die", 1f);
    }
    
    
    
}
