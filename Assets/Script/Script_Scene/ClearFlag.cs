using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag : MonoBehaviour
{
	[SerializeField]
	private GameObject ClearUIPrefab;
	private GameObject ClearUIInstance;

	// 階層指定
	[SerializeField]
    Transform ClearUIParent;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clear()
    {
        Debug.Log("クリア!!");
        ClearUIInstance = GameObject.Instantiate(ClearUIPrefab, ClearUIParent) as GameObject;

    }
}
