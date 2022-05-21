using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;

    /* 2021/04/23 追加 */
    public bool gameClear;
    Vector3 startPos;
    Vector3 endPos;
    Quaternion startRot;
    Quaternion endRot;

    float time;
    /*******************/

    Vector3 rot;    // 回転
    public float speed; // 回転速度

    float MaxRot = 80.0f;
    float MinRot = 10.0f;

	/* 2021/05/08 追加 */
	 public bool bClearText;


    //20210331******************************
    [SerializeField] GameObject PlayerRing;
    //**************************************

    

    Vector3 playerPos;


    // start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - player.transform.position;

        //20210331******************************
        PlayerRing = player.transform.GetChild(0).gameObject; //最初のオブジェクトを取得
        //**************************************

        gameClear = false;

		bClearText = false;

        time = 0.0f;

        player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;

    }

    // Update is called once per frame

    //最後に実行されるようになる
    void LateUpdate()
    {
		// Pキー押したらゲームクリアに強制遷移
        if (Input.GetKeyDown("p"))
		{
			bClearText= true;
			Invoke("ClearCamera",2.0f);
           

        }

		rot = Vector3.zero;
		
        // プレイ中
        if (!gameClear)
        { 
            transform.position += player.transform.position - playerPos;
            playerPos = player.transform.position;

            CameraRot();
            transform.RotateAround(playerPos, Vector3.up, rot.x * speed);
            transform.RotateAround(playerPos, transform.right, rot.y * speed);
        }

        // クリア
        else
        {
            // クリア画面用のカメラ位置に
            if (time < 1.0f)
            {
                transform.position = Vector3.Slerp(startPos, endPos, time);
                transform.rotation = Quaternion.Slerp(startRot, endRot, time);

                time += Time.deltaTime;
            }
        }
    }

    // ダメージを受けた時にカメラを揺らす処理
    void CameraShake(float magnitude)
    {
        var x = transform.rotation.x + Random.Range(-1.0f, 1.0f) * magnitude;
        var y = transform.rotation.y + Random.Range(-1.0f, 1.0f) * magnitude;

        transform.rotation = Quaternion.Euler(x, y, transform.rotation.z);
    }

    void CameraRot()
    {
        float rsh = Input.GetAxis("R_Stick_H");     //Rスティック(左右)
        float rsv = Input.GetAxis("R_Stick_V");     //Rスティック(上下)

        if (rsh > 0)    //右
        {
            rot.x += 0.5f;
        }
        if (rsh < 0)    //左
        {
            rot.x -= 0.5f;
        }
        if (rsv > 0)    //上
        {
            rot.y += 0.5f;
        }
        if (rsv < 0)    //下
        {
            rot.y -= 0.5f;
        }

        if (Input.GetKey("j"))
        {
            rot.x += 0.5f;
        }
        if (Input.GetKey("l"))
        {
            rot.x -= 0.5f;
        }
        if (Input.GetKey("i"))
        {
            rot.y += 0.5f;
        }
        if (Input.GetKey("k"))
        {
            rot.y -= 0.5f;
        }
    }

	public void ClearCamera()
	{
       startPos = this.transform.position;
       endPos = new Vector3(player.transform.position.x - 5.0f, player.transform.position.y + 6.5f, player.transform.position.z - 5.0f);

       startRot = this.transform.rotation;
       endRot = new Quaternion(0.1f, 0.0f, 0.0f, 0.2f);

       gameClear = true;
	}

    public void Clear()
    {
        bClearText = true;
        Invoke("ClearCamera", 2.0f);
    }
}
