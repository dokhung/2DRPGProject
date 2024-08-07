using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPoolManager : MonoBehaviour
{
    /*
     * 몬스터를 생성하여 사냥후 재활성화 함
     * 몬스터가 소환될 위치를 정확히 잡아야한다.
     */
    [Header("UseMonster")]
    public GameObject MonsterPrefab;
    public Transform monsterParent;

    [Header("NeedMonsterCount")] 
    [SerializeField]private int MonsterpoolCount = 10;
    private List<GameObject> objectPool = new List<GameObject>();
    [Header("Movement Parameters")]
    private float timer;
    private SpriteRenderer sp;
    public bool ItMonster = false;
    private Coroutine cr = null;
    public Monster.MonsterStat MonsterStat;
    
    


    void Start()
    {
        MonsterStat = new Monster.MonsterStat(PlayerManager.Instance.Level);
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
                float x = Random.Range(60f, 100f);
                obj.transform.position = new Vector2(x, 0);
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
                float x = Random.Range(50f, 100f);
                obj.transform.position = new Vector2(x, 0);
                obj.SetActive(true);
            }
        }
    }
}
