using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class NomalMonsterAI : Monster
{
    /*
     * memo
     * Collider2D chaseCollider = Physics2D.OverlapCircle(transform.position, searchRange, LayerMask.NameToLayer(("Player")));
       // 공격 범위 내의 플레이어 감지
       Collider2D attackCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.NameToLayer(("Player")));
     */
    #region 각종변수
    //Enum
    public AllEnum.NomalMonsterStateBT nomalmonsterai;
    //Idle상태용 변수
    private float CoolTime = 0f;
    private bool isLook = true;
    private float interval = 3f;
    private float IdlelookCount = 0;
    public Animator anime;
    private Rigidbody2D rigidBody;
    private SpriteRenderer Sr;
    private Monster.MonsterStat monsterStat;
    #endregion
    #region Start()
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Sr = gameObject.GetComponent<SpriteRenderer>();
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
            //Idle :: 비전투상태
            case AllEnum.NomalMonsterStateBT.State_Idle:
                Idle();
                break;
        }
    }
    #endregion
    #region 실시간상태변형
    private void ChangeState(AllEnum.NomalMonsterStateBT newState)
    {
        nomalmonsterai = newState;
    }
    #endregion

    #region 비전투상태

    void Idle()
    {
        Debug.Log("현재상태 :: " + (nomalmonsterai));
        CoolTime += Time.deltaTime;
        if (CoolTime >= interval)
        {
            CoolTime = 0f;
            if (isLook)
            {
                LookRight();
            }
            else
            {
                LookLeft();
            }

            isLook = !isLook;
            if (IdlelookCount >= 3)
            {
                Debug.Log("ok");
            }
        }
    }
    void LookLeft()
    {
        Sr.flipX = true;
        IdlelookCount += 1;
    }

    void LookRight()
    {
        Sr.flipX = false;
        IdlelookCount += 1;
    }

    void LeftRun()
    {
        Debug.Log("왼쪽으로 걷고 있어요");
    }

    void RightRun()
    {
        Debug.Log("오른쪽으로 걷고 있어요"); 
    }
    #endregion
}
