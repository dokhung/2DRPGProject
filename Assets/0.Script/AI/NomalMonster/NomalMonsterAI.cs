using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NomalMonsterAI : Monster
{
    public AllEnum.NomalMonsterStateBT nomalmonsterai;
    public Animator anime;

    private Coroutine idleCoroutine;
    private Coroutine guardCoroutine;

    private Vector2 Destination;
    private float Distance;

    private Vector2 FindPlayer;
    public float searchRange = 10f;

    public bool isInRange = false;
    public bool Fight = false;
    public bool isSuspicious = false;
    public int IdleCount = 0;
    public bool TrueMove = false;
    public int GuardCount = 0;

    public TMP_Text monsterDamageText;

    public GameObject[] dropItems;

    private Rigidbody2D rigidBody;
    public Transform initialPosition;

    private SpriteRenderer spriteRenderer;
    private Monster.MonsterStat monsterStat;

    private float MoveSpeed = 0;
    private float MovePoint = 0f;

    private void Start()
    {
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.gameObject.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        MoveSpeed = Random.Range(1f, 3f);
        nomalmonsterai = (Random.value > 0.5f) ? AllEnum.NomalMonsterStateBT.State_Idle : AllEnum.NomalMonsterStateBT.State_Guard;
        Debug.Log($"Initial state: {nomalmonsterai}");
        OnBehaviors();
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
        OnBehaviors();
    }

    private void OnBehaviors()
    {
        switch (nomalmonsterai)
        {
            case AllEnum.NomalMonsterStateBT.State_Idle:
                IdleNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Guard:
                GuardNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Combat:
                CombatNode();
                break;
            case AllEnum.NomalMonsterStateBT.State_Chase:
                ChaseNode();
                break;
        }
    }

    private void ChangeState(AllEnum.NomalMonsterStateBT newState)
    {
        nomalmonsterai = newState;
        OnBehaviors();
        if (!isInRange)
        {
            ResetCoroutines();  
        }
    }

    private void ResetCoroutines()
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

    public void Idle()
    {
        switch (isInRange)
        {
           case false :
               if (idleCoroutine == null)
               {
                   idleCoroutine = StartCoroutine(IdleTimeState());
               }
               break;
           case true :
               ChangeState(AllEnum.NomalMonsterStateBT.State_Combat); 
               break;
        }
    }

    public void Guard()
    {
        switch (isInRange)
        {
            case false :
                MoveGuard();
                break;
            case true :
                ChangeState(AllEnum.NomalMonsterStateBT.State_Combat);
                break;
        }
    }

    public void Combat()
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

    public void MoveGuard()
    {
        switch (TrueMove)
        {
            case true:
                spriteRenderer.flipX = true;
                anime.SetBool("Run", true);
                MovePoint += MoveSpeed * Time.deltaTime;
                transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);

                if (MovePoint >= Random.Range(1, 10))
                {
                    TrueMove = false;
                    GuardCount += 1;
                }
                break;
            case false:
                spriteRenderer.flipX = false;
                anime.SetBool("Run", true);
                MovePoint -= MoveSpeed * Time.deltaTime;
                transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);

                if (MovePoint <= Random.Range(-1, -10))
                {
                    TrueMove = true;
                    GuardCount += 1;
                }
                break;
        }

        if (GuardCount >= Random.Range(3, 5))
        {
            anime.SetBool("Run", false);
            GuardCount = 0;
            ChangeState(AllEnum.NomalMonsterStateBT.State_Idle);
        }
    }

    public void NomalAttack()
    {
        // Implement attack logic here
    }

    public void Patrol()
    {
        Debug.Log("Patrol");
    }

    IEnumerator IdleTimeState()
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

    public void TakeDamege(int damege)
    {
        monsterDamageText.gameObject.SetActive(true);
        anime.SetTrigger("Hit");
        monsterStat.hp -= damege;
        monsterDamageText.text = damege.ToString();
        monsterDamageText.transform.position = initialPosition.transform.position;
        monsterDamageText.transform.DOMoveY(1, 1).SetRelative();
        Invoke("ResetDamegeText", 1f);
        if (monsterStat.hp <= 0)
        {
            DropItems();
        }
    }

    public void ResetDamegeText()
    {
        monsterDamageText.gameObject.SetActive(false);
    }
    
    private void Die()
    {
        UIManager.Instance.SetEXP += monsterStat.giveExp;
        // deadAnimation.SetActive(false);
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

    private void ResetDamageText()
    {
        monsterDamageText.gameObject.SetActive(false);
    }
    
    public void Playerinvincibility()
    {
        SpriteRenderer HeadColor = InputManager.Instance.HeadColor.gameObject.GetComponent<SpriteRenderer>();
        HeadColor.color = Color.white;
        InputManager.Instance.PlayerIsHit = false; 
        InputManager.Instance.NotBehitAnim();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidBody = other.rigidbody;
            Vector2 forceDirection = spriteRenderer.flipX ? new Vector2(-2, 2) : new Vector2(2, 2);
            UIManager.Instance.SetHP -= monsterStat.att;
            if (!UIManager.Instance.OnDamege && !InputManager.Instance.PlayerIsHit)
            {
                playerRigidBody.AddForce(forceDirection, ForceMode2D.Impulse);
                InputManager.Instance.PlayerIsHit = true;
                UIManager.Instance.PlayerBeHitDamege(monsterStat.att);
                SpriteRenderer HeadColor = InputManager.Instance.HeadColor.gameObject.GetComponent<SpriteRenderer>();
                HeadColor.color = Color.red;
                InputManager.Instance.BehitAnim();
                Invoke("Playerinvincibility",1f);
            }
        }
    }
}
