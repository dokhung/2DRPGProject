using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class NomalMonsterAI : Monster
{
    public AllEnum.NomalMonsterStateBT nomalmonsterai;
    public Animator anime;

    private Coroutine currentCoroutine;

    private Vector2 Destination;
    private float Distance;
    
    [SerializeField] private float searchRange = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float chaseSpeed = 2f;
    private bool isInRange = false;
    private bool isInAttackRange = false;
    private bool isCollisionWithDoor = false;

    private int IdleCount = 0;
    private bool TrueMove = false;
    private int GuardCount = 0;

    public TMP_Text monsterDamageText;
    public GameObject[] dropItems;

    private Rigidbody2D rigidBody;
    public Transform initialPosition;
    private SpriteRenderer spriteRenderer;
    private Monster.MonsterStat monsterStat;
    private float MoveSpeed = 0;
    private float MovePoint = 0f;
    
    public float detectionRadius = 1f; 

    private void Start()
    {
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.gameObject.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        MoveSpeed = Random.Range(1f, 3f);
    }

    private void OnEnable()
    {
        InitializeMonsterStat();
    }

    public override void InitializeMonsterStat()
    {
        monsterStat = new Monster.MonsterStat(PlayerManager.Instance.Level);
    }

    private void Update()
    {
        BehaviourTree();
    }

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

    private void ChangeState(AllEnum.NomalMonsterStateBT newState)
    {
        nomalmonsterai = newState;
        ResetCurrentCoroutine();
    }

    private void ResetCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    private void CheckPlayerDistance()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, searchRange, LayerMask.GetMask("Player"));
        isInRange = playerCollider != null;

        if (isInRange)
        {
            if (isInAttackRange)
            {
                ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
            }
            else
            {
                ChangeState(AllEnum.NomalMonsterStateBT.State_Chase);
            }
        }
        else
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Guard);
        }
    }

    private void Idle()
    {
        if (!isCollisionWithDoor)
        {
            CheckPlayerDistance();
        }
        if (!isInRange)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(IdleTimeState());
            }
        }
        else
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
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
            ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
        }
    }

    private void Combat()
    {
        if (isInAttackRange)
        {
            anime.SetBool("Run",false);
            anime.SetTrigger("Attack");
        }
        else if (isInRange)
        {
            ChangeState(AllEnum.NomalMonsterStateBT.State_Chase);
        }
    }

    private void Chase()
    {
        // 추적
    }


    private void Return()
    {
        //추적중지
    }

    private IEnumerator IdleTimeState()
    {
        while (true)
        {
            if (IdleCount >= Random.Range(3, 6))
            {
                ChangeState(AllEnum.NomalMonsterStateBT.State_Guard);
                IdleCount = 0;
                yield break;
            }

            yield return new WaitForSeconds(3f);
            spriteRenderer.flipX = Random.value > 0.5f;
            IdleCount += 1;
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

                if (MovePoint >= Random.Range(1, 10))
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

                if (MovePoint <= Random.Range(-1, -10))
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

    public void TakeDamage(int damage)
    {
        monsterDamageText.gameObject.SetActive(true);
        anime.SetTrigger("Hit");
        monsterStat.hp -= damage;
        monsterDamageText.text = damage.ToString();
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.transform.DOMoveY(1, 1).SetRelative();
        Invoke("ResetDamageText", 1f);

        if (monsterStat.hp <= 0)
        {
            DropItems();
        }
    }

    public void ResetDamageText()
    {
        monsterDamageText.gameObject.SetActive(false);
    }

    private void Die()
    {
        UIManager.Instance.SetEXP += monsterStat.giveExp;
        gameObject.SetActive(false);
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door1"))
        {
            Debug.Log("Door1 근처에 있음. 추적 중지");
            anime.SetBool("Run", false);
            ChangeState(AllEnum.NomalMonsterStateBT.State_Idle);
        }
    }
}
