using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    GameObject Path1, Path2, Path3, Path4, Path5,
               Path6, Path7, Path8, Path9, Path10,
               Path11, Path12, Path13, Path14, Path15;

    // パスのゲームオブジェクト配列
    GameObject[] PathBox;

	// 現在のパス番号取得用変数
	public int PathNum;

	// プレイヤー移動変数
	private float MoveZ;

    void Start()
    {
		// パス配列
        PathBox = new GameObject[] 
        { Path1, Path2, Path3, Path4, Path5,
          Path6, Path7, Path8, Path9, Path10,
          Path11, Path12, Path13, Path14, Path15
        };

		MoveZ = 0f;
		PathNum = 1;
    }

    void Update()
    {
		// プレイヤー移動
		this.transform.DOLocalMove(new Vector3(0f, 0f, MoveZ), 1f);

		// ステージ移動 
		if(Input.GetKeyDown(KeyCode.D) && MoveZ < 1400.0f)
		{
			MoveZ += 100.0f;
            GameObject.Find("GameManager").GetComponent<KeySound>().Select();
		}
		if(Input.GetKeyDown(KeyCode.A) && MoveZ> 0)
		{
			MoveZ += -100.0f;
            GameObject.Find("GameManager").GetComponent<KeySound>().Select();
        }

        //コントローラー
        //// ステージ移動 
        //if (Input.GetAxis("Horizontal") < 0 && MoveZ < 1400.0f)
        //{
        //    MoveZ += 100.0f;
        //    GameObject.Find("GameManager").GetComponent<KeySound>().Select();
        //}
        //if (Input.GetAxis("Horizontal") > 0 && MoveZ > 0)
        //{
        //    MoveZ += -100.0f;
        //    GameObject.Find("GameManager").GetComponent<KeySound>().Select();
        //}
    }

	void OnCollisionStay(Collision collision)
	{	
		// 現在のパスナンバーを取得
		if (collision.gameObject.name == "Path1")	PathNum = 1;
		if (collision.gameObject.name == "Path2")	PathNum = 2;
		if (collision.gameObject.name == "Path3")	PathNum = 3;
		if (collision.gameObject.name == "Path4")	PathNum = 4;
		if (collision.gameObject.name == "Path5")	PathNum = 5;
        if (collision.gameObject.name == "Path6")   PathNum = 6;
        if (collision.gameObject.name == "Path7")   PathNum = 7;
        if (collision.gameObject.name == "Path8")   PathNum = 8;
        if (collision.gameObject.name == "Path9")   PathNum = 9;
        if (collision.gameObject.name == "Path10")  PathNum = 10;
        if (collision.gameObject.name == "Path11")  PathNum = 11;
        if (collision.gameObject.name == "Path12")  PathNum = 12;
        if (collision.gameObject.name == "Path13")  PathNum = 13;
        if (collision.gameObject.name == "Path14")  PathNum = 14;
        if (collision.gameObject.name == "Path15")  PathNum = 15;
    }
}
