using System;
using System.Collections;
using System.Collections.Generic;
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
    
    //타이밍
    public float flipInterval = 10.0f;
    
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
    private float MoveSpeed = 2f;

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
        StartCoroutine(LookMove());

    }
    public void Guard() // 경계 이동
    {
        Debug.Log("Grard");
        if (isSuspicious)
        {
            SearchForTarget();
        }
        else
        {
            Patrol();
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
                Debug.Log("공격이 시작되었습니다. 몬스터의 공격함수가 실행됩니다.");
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

    public void NomalAttack()
    {
       Debug.Log("일반공격");
    }

    public void Patrol()
    {
        Debug.Log("Patrol");
    }

    IEnumerator LookMove()
    {
        while (true)
        {
            // flipX 속성 반전
            spriteRenderer.flipX = !spriteRenderer.flipX;
            // 지정한 시간 동안 대기
            yield return new WaitForSeconds(flipInterval);
        }
    }

    public void SearchForTarget()
    {
        Debug.Log("SearchForTarget");
        Destination = GameObject.FindGameObjectWithTag("Player")
            .transform.position;
        Distance = Vector2.Distance(gameObject.transform.position,
            Destination);
        if (Distance < 10)
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
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
