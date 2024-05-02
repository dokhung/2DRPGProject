using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPoolManager : MonoBehaviour
{
    /*
     * 몬스터를 생성하여 사냥후 재활성화 함
     */
    [Header("UseMonster")]
    public GameObject MonsterPrefab;
    public Transform monsterParent;

    public GameObject BigMonsterPrefab;

    [Header("NeedMonsterCount")] 
    [SerializeField]
    private int MonsterpoolCount = 5;
    [SerializeField]
    private int poolBigSize = 2;

    private List<GameObject> objectPool = new List<GameObject>();
    private List<GameObject> BigobjectPool = new List<GameObject>();
    
    [Header("Movement Parameters")]
    public float moveSpeed = 5f;
    public float moveInterval = 0.05f; // 움직임 인터벌

    private float timer;
    private SpriteRenderer sp;

    public bool ItMonster = false;
    private Coroutine cr = null;
    public Monster.MonsterStat MonsterStat;
    public int Level = 1;
    
    


    void Start()
    {
        MonsterStat = new Monster.MonsterStat(PlayerManager.Instance.playerStat.Level);
        sp = GetComponent<SpriteRenderer>();
        for (int i = 0; i < MonsterpoolCount; i++)
        {
            GameObject obj = Instantiate(MonsterPrefab);
            obj.transform.SetParent(monsterParent);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
        PlaceRandomObjects();

    }

    private void Update()
    {
        ResetMiniMonsters();
    }

    void PlaceRandomObjects()
    {
        for (int i = 0; i < MonsterpoolCount; i++)
        {
            GameObject obj = GetPooledObject();
            if (obj != null)
            {
                // 랜덤한 위치로 배치
                float x = Random.Range(55f, 70f);
                //float y = Random.Range(0f, 3f);
                obj.transform.position = new Vector3(x, 0, 0f);
                obj.SetActive(true);
                ItMonster = true;
            }
        }
    }

    GameObject GetPooledObject()
    {
        for (int i = 0; i < MonsterpoolCount; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        return null;
    }
    void ResetMiniMonsters()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                // 랜덤한 위치로 배치
                float x = Random.Range(55f, 70f);
                //float y = Random.Range(0f, 3f);
                obj.transform.position = new Vector3(x, 0, 0f);
                obj.SetActive(true);
            }
        }
    }
}
