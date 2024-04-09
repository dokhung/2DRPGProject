using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float DamegeSpeed = 5.0f;

    GameObject Target;

    private void Start()
    {
        Target = GameObject.Find("DameageInitTaget");
    }

    private void Update()
    {
        Vector3 dir = Target.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * DamegeSpeed * Time.deltaTime, dir.y * DamegeSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
