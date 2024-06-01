using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Slime : Monster
{
    // 몬스터 상태 열거형
    private enum MonsterState
    {
        Normal,
        Detection
    }

    // 몬스터 상태
    private MonsterState currentState = MonsterState.Normal;

    // 몬스터 정보
    private SpriteRenderer spriteRenderer;
    private Monster.MonsterStat monsterStat;
    
    // 이동 관련 변수
    private bool isMovingRight = false;
    private float moveSpeed = 1f;
    private float movePoint = 0f;
    
    // UI 및 애니메이션
    public TMP_Text monsterDamageText;
    public GameObject deadAnimation;
    public Animator animator;
    
    // 드랍 아이템 리스트
    [Header("드롭아이템List")]
    public GameObject[] dropItems;

    private Rigidbody2D rigidBody;
    public Transform initialPosition;

    private void OnEnable() // OnEnable은 Start()보다 라이프사이클이 더 빠름
    {
        InitializeMonsterStat();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterDamageText.gameObject.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();
        monsterDamageText.transform.position = initialPosition.transform.position;
    }

    public void InitializeMonsterStat()
    {
        monsterStat = new MonsterStat(PlayerManager.Instance.Level);
    }

    private void Update()
    {
        if (currentState == MonsterState.Normal)
        {
            MoveInNormalState();
        }
    }

    private void MoveInNormalState()
    {
        if (!isMovingRight)
        {
            spriteRenderer.flipX = true;
            movePoint += moveSpeed * Time.deltaTime;
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            if (movePoint >= Random.Range(3, 6))
            {
                isMovingRight = true;
                currentState = MonsterState.Detection;
                if (currentState == MonsterState.Detection)
                {
                    moveSpeed -= 1;
                    StartCoroutine(MoveAndJump());
                }
            }
        }
        else
        {
            spriteRenderer.flipX = false;
            movePoint -= moveSpeed * Time.deltaTime;
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            if (movePoint <= Random.Range(-3, -6))
            {
                isMovingRight = false;
                currentState = MonsterState.Detection;
                if (currentState == MonsterState.Detection)
                {
                    moveSpeed -= 1;
                    StartCoroutine(MoveAndJump());
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentState = MonsterState.Detection;
        monsterDamageText.gameObject.SetActive(true);
        animator.SetTrigger("Hit");
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

    private void ResetDamageText()
    {
        monsterDamageText.gameObject.SetActive(false);
    }

    IEnumerator MoveAndJump()
    {
        yield return new WaitForSeconds(2f);
        currentState = MonsterState.Normal;
        moveSpeed += 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = MonsterState.Detection;
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
                Invoke("ResetState", 5f); 
                Invoke("Playerinvincibility",1f);
            }
        }
    }

    public void Playerinvincibility()
    {
        SpriteRenderer HeadColor = InputManager.Instance.HeadColor.gameObject.GetComponent<SpriteRenderer>();
        HeadColor.color = Color.white;
        InputManager.Instance.PlayerIsHit = false; 
        InputManager.Instance.NotBehitAnim();
    }

    public void ResetState()
    {
        currentState = MonsterState.Normal;
    }

    public void DropItems()
    {
        GameObject ItemToDrop = dropItems[Random.Range(0, dropItems.Length)];
        GameObject DroppedItem = Instantiate(ItemToDrop, transform.position, Quaternion.identity);
        Rigidbody2D ItemRigidBody = DroppedItem.GetComponent<Rigidbody2D>();
        ItemRigidBody.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        animator.SetBool("Death", true);
        Invoke("Die", 1f);
    }

    private void Die()
    {
        UIManager.Instance.SetEXP += monsterStat.giveExp;
        deadAnimation.SetActive(false);
        gameObject.SetActive(false);
    }
}
