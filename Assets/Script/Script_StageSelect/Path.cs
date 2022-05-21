using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject Player;
	public bool bPath;

    void Start()
    {
        Player = GameObject.Find ("Player");
		bPath = true;
    }

    void Update()
    {
        
    }

	// 接触中
	void OnCollisionStay(Collision collision)
	{	
		bPath = true;

        if (collision.gameObject.name == "Player")
		{	
			// 接触中パスを白色に
            GetComponent<Renderer>().material.color = Color.white;


        }
	}

	// 離れた時
    private void OnCollisionExit(Collision collision)
	{
		bPath = false;

        if(collision.gameObject.name == "Player")
		{			
			// 離れた時パスを灰色に
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
