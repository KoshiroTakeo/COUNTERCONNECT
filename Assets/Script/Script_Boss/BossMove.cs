using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private float angle;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }


    void Update()
    {

    }

    public void Move(Vector3 scale, float speed)
    {
        angle += Time.deltaTime * speed;

        transform.position = new Vector3(Mathf.Sin(angle) * (scale.x * 2), transform.position.y, Mathf.Sin(angle * 2) * scale.z);
        transform.position += new Vector3(pos.x, pos.y, pos.z);
    }
}
