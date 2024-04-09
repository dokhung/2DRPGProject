using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeScript : MonoBehaviour
{
    public float Speed = 5.0f;

    GameObject MonsterCrab;

    private void Start()
    {
        MonsterCrab = GameObject.Find("MonsterCrab");
    }

    private void Update()
    {
        Vector3 dir = MonsterCrab.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * Speed * Time.deltaTime, dir.y * Speed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
