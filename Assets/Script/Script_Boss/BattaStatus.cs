using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattaStatus : MonoBehaviour
{

[SerializeField] private GameObject enemy;
public float Hp;
public float Atk;
public float MoveSpeed;
public float Experience;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 400;
        Atk = 20;
        MoveSpeed = 30;
        Experience = 100;
    }

// Update is called once per frame
void Update()
{

}
}
