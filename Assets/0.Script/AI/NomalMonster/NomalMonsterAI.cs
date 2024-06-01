using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class NomalMonsterAI : Monster
{
    #region 각종변수
    //Enum
    public AllEnum.NomalMonsterStateBT nomalmonsterai;
    //Idle상태용 변수
    //Guard상태용 변수
    //Combat상태용 변수
    //Chase상태용 변수
    public Animator anime;
    private Coroutine currentCoroutine;
    private Coroutine idleCoroutine;
    private Coroutine guardCoroutine;
    private Vector2 Destination;
    private float Distance;
    [SerializeField] private float searchRange = 5f;
    [SerializeField] private float attackRange = 2f;
    private bool isInRange = false;
    private bool isInAttackRange = false;
    private bool isCollisionWithDoor = false;
    private int IdleCount = 0;
    private bool TrueMove = false;
    private int GuardCount = 0;
    private Rigidbody2D rigidBody;
    public Transform initialPosition;
    private SpriteRenderer spriteRenderer;
    private Monster.MonsterStat monsterStat;
    private float MoveSpeed = 0;
    private float MovePoint = 0f;
    #endregion
    #region Start()
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    #endregion
    #region OnEnable()
    private void OnEnable()
    {
        InitializeMonsterStat();
    }
    #endregion
    #region 몬스터스탯의 설정
    public void InitializeMonsterStat()
    {
        monsterStat = new Monster.MonsterStat(PlayerManager.Instance.Level);
    }
    #endregion
    #region Update() : 지속적으로 트리로 인한 상태변형을 받는중
    private void Update()
    {
        BehaviourTree();
    }
    #endregion
    #region BehaviourTree() : 트리 리스트
    private void BehaviourTree()
    {
        switch (nomalmonsterai)
        {
            case AllEnum.NomalMonsterStateBT.State_Idle:
                Idle();
                break;
            case AllEnum.NomalMonsterStateBT.State_Guard:
                Guard();
                break;
            case AllEnum.NomalMonsterStateBT.State_Combat:
                Combat();
                break;
            case AllEnum.NomalMonsterStateBT.State_Chase:
                Chase();
                break;
            case AllEnum.NomalMonsterStateBT.State_Return:
                Return();
                break;
        }
    }
    #endregion
    #region 실시간상태변형
    private void ChangeState(AllEnum.NomalMonsterStateBT newState)
    {
        nomalmonsterai = newState;
        ResetCurrentCoroutine();
    }
    #endregion
    

    private void ResetCurrentCoroutine()
    {
        if (idleCoroutine != null)
        {
            StopCoroutine(idleCoroutine);
            idleCoroutine = null;
        }

        if (guardCoroutine != null)
        {
            StopCoroutine(guardCoroutine);
            guardCoroutine = null;
        }
    }
    
    // 플레이어 감지
    // 플레이어를 감지하여 감지 범위인지 여부와
    // 감지 범위일때 공격이 가능한 범위인지 판단
    private void CheckPlayerDistance()
    {
        // 추격 범위 내의 플레이어 감지
        Collider2D chaseCollider = Physics2D.OverlapCircle(transform.position, searchRange, LayerMask.GetMask("Player"));
        // 공격 범위 내의 플레이어 감지
        Collider2D attackCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));
    
        // 추격 범위 내에 플레이어가 있는지 확인
        isInRange = chaseCollider != null;
        // 공격 범위 내에 플레이어가 있는지 확인
        isInAttackRange = attackCollider != null;

        // 상태 변경 로직
        if (isInRange)
        {
            switch (isInAttackRange)
            {
                case true:
                Debug.Log("isInAttackRange = true => Combat");
                ChangeState(AllEnum.NomalMonsterStateBT.State_Combat); 
                break;
                case false:
                Debug.Log("isInAttackRange = false => Chase");
                ChangeState(AllEnum.NomalMonsterStateBT.State_Chase);
                break;
            }
        }
        else
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Idle);
        }
    }

    private void Idle()
    {
        anime.SetBool("Run",false);
        CheckPlayerDistance();
        if (!isInRange)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(IdleTimeState());
            }
        }
        else
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Chase);
        }
    }

    private void Guard()
    {
        CheckPlayerDistance();
        if (!isInRange)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(GuardMovement());
            }
        }
        else
        {
            Debug.Log("Guard => State_Chase");
            ChangeState(AllEnum.NomalMonsterStateBT.State_Chase);
        }
    }

    private void Combat()
    {
        
    }

    private void Chase()
    {
        
    }
    private void Return()
    {
        
    }

    private IEnumerator IdleTimeState()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            spriteRenderer.flipX = !spriteRenderer.flipX;
            IdleCount += 1;
            Debug.Log(IdleCount);
            if (IdleCount >= Random.Range(3,6))
            {
                
                ChangeState(AllEnum.NomalMonsterStateBT.State_Guard);
            }
        }
    }

    private IEnumerator GuardMovement()
    {
        while (true)
        {
            if (TrueMove)
            {
                spriteRenderer.flipX = true;
                anime.SetBool("Run", true);
                MovePoint += MoveSpeed * Time.deltaTime;
                transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);

                if (MovePoint >= Random.Range(1, 20))
                {
                    TrueMove = false;
                    GuardCount += 1;
                }
            }
            else
            {
                spriteRenderer.flipX = false;
                anime.SetBool("Run", true);
                MovePoint -= MoveSpeed * Time.deltaTime;
                transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);

                if (MovePoint <= Random.Range(-1, -20))
                {
                    TrueMove = true;
                    GuardCount += 1;
                }
            }

            if (GuardCount >= Random.Range(3, 5))
            {
                anime.SetBool("Run", false);
                GuardCount = 0;
                ChangeState(AllEnum.NomalMonsterStateBT.State_Idle);
                yield break;
            }

            yield return null;
        }
    }
}
