using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    Rigidbody rb;           //重力
    public float speed;     //速度

    private Vector3 centerPos;
    private float radius;

    GameObject Camera;

    // -- ブースト関連 -- 
    GameObject player;
    [Header("ブースト時間")] public float BoostTime = 0.2f;               // ブーストする時間
    [Header("ブーストインターバル")] public float BoostInterval = 0.6f;   // ブーストのインターバル

    private float currentBoostTime;     // ブーストの経過時間
    private float currentInterval;      // ブーストの経過時間
    private float oldspeed;             // ブースト前のスピード保存用
    private float Dashspeed;            // ダッシュ状態のスピード保存用

    // フラグ
    private bool bBoostFlag;
    private bool bPushKeyFlag;
    private bool bDashFlag;
    private bool bNextBoostFlag;
    private bool bIntervalFlag;


    // -- プレイヤーの回転用 --
    private Vector3 latestPos;  //前回のPosition
    Quaternion rot;


    // サウンド
    GameObject SE;
    SoundSE soundSE;

    // エフェクト
    public GameObject BoostEffect;
    GameObject BoostEffectTmp;

    private bool bSeFlag;

    //-------------------
    // 初期化
    //-------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        centerPos = new Vector3(0.0f, 0.0f, 0.0f);
        radius = 50.0f;


        player = GameObject.FindWithTag("Player");      // プレイヤー情報を取得

        currentBoostTime = 0.0f;
        currentInterval = 0.0f;
        oldspeed = speed;                               // スピードを保存
        Dashspeed = speed * 1.2f;                       // ダッシュスピード保存

        bBoostFlag = false;
        bPushKeyFlag = false;
        bDashFlag = false;
        bNextBoostFlag = false;
        bIntervalFlag = false;

        Camera = GameObject.Find("Main Camera");


        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");

        bSeFlag = false;
    }


    //-------------------
    // 更新
    //-------------------
    void Update()
    {
        if(player.GetComponent<PlayerStatus>().GetLevelUpFlag())
        {
            Debug.Log("now");
            BoostEffect.GetComponentInChildren<ParticleSystem>().startSize += 0.05f;
        }
            


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SE.GetComponent<SoundSE>().PlaySE(7);
            bSeFlag = true;

            // レベルに合わせてエフェクト大きさを変更
            //BoostEffect.GetComponentInChildren<ParticleSystem>().startSize = (float)player.GetComponent<PlayerStatus>().GetLevel * 1.5f;
        }

    }


    void FixedUpdate()
    {
        // プレイヤー回転
        Rotation();

        // プレイヤー移動
        Move();

        // ブースト処理
        Boost();

        // 円形の移動制限
        Restriction();
    }


    //------------------------------------------------------------
    // プレイヤー回転処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：進行方向に回転させる
    //------------------------------------------------------------
    void Rotation()
    {
        Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        // 動いたら回転させる
        if (diff.magnitude > 0.01f)
        {
            rot = Quaternion.LookRotation(diff);
        }

        rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * 4);
        this.transform.rotation = rot;
    }


    //------------------------------------------------------------
    // プレイヤー移動処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：プレイヤーの移動
    //------------------------------------------------------------
    void Move()
    {

        //左右移動
        var inputHorizontal = Input.GetAxis("Horizontal");
        float lsh = Input.GetAxis("L_Stick_H");//Lスティック(左右)     変更点(前田)

        //上下移動
        var inputVertical = Input.GetAxis("Vertical");
        float lsv = Input.GetAxis("L_Stick_V");//Lスティック(上下)     変更点(前田)

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定(キーボード)
        Vector3 moveForward = cameraForward * inputVertical + Camera.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。(キーボード)
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に(キーボード)
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //変更点(前田)
        //// 方向キーの入力値とカメラの向きから、移動方向を決定(コントローラー)
        //Vector3 moveForward1 = cameraForward * lsv + Camera.transform.right * lsh;

        //// 移動方向にスピードを掛ける。(コントローラー)
        //rb.velocity = moveForward1 * speed + new Vector3(0, rb.velocity.y, 0);

        //// キャラクターの向きを進行方向に(コントローラー)
        //if (moveForward1 != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveForward1);
        //}
        //変更ここまで
    }


    //------------------------------------------------------------
    // 移動制限処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：円形に移動を制限する
    //------------------------------------------------------------
    void Restriction()
    {
        //自身と円形に移動制限させたい位置の中心点との距離を測り半径以上になっていれば処理
        if (Vector3.Distance(transform.position, centerPos) > radius)
        {
            //中心点から自身までの方向ベクトルを作る
            Vector3 nor = transform.position - centerPos;
            //作った方向ベクトルを正規化する
            nor.Normalize();
            //方向ベクトル分半径に移動させる
            transform.position = nor * radius;
        }
    }


    //------------------------------------------------------------
    // ブースト処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：ブーストの処理。ダッシュ状態の処理。
    //------------------------------------------------------------
    void Boost()
    {
        // コネクト中はブーストできない
        if (player.GetComponentInChildren<Conect>().GetbConect)
        {
            bSeFlag = false;

            bPushKeyFlag = false;       // キーを離した判定

            return;
        }



        // LeftShiftでブースト状態へ
        float tri = Input.GetAxis("L_R_Trigger");


        if (tri > 0 || Input.GetKey(KeyCode.LeftShift))
        {
            // エフェクト発生
            BoostEffectTmp = Instantiate(BoostEffect, transform.position, transform.rotation);

            // エフェクト削除（一秒後）
            Destroy(BoostEffectTmp, 1.0f);

            bPushKeyFlag = true;        // キーを押している判定

        }




        //キーを離した時
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            bSeFlag = false;

            bPushKeyFlag = false;       // キーを離した判定
        }




        // LeftShiftが押されている ＆ ブースト状態ではない ＆ ダッシュ状態でない
        if (bPushKeyFlag && !bBoostFlag && !bDashFlag && !bIntervalFlag)
        {
            bBoostFlag = true;          // ブースト中
            bIntervalFlag = true;       // インターバルON

            Dashspeed = speed * 1.2f;   // ダッシュ状態のスピード保存
            oldspeed = speed;           // 今のスピードを保存
            speed *= 2;                 // スピードを二倍に
        }


        // -- ブースト時 --
        if (bBoostFlag)
        {
            currentBoostTime += Time.deltaTime;  // Boost時間をはかる
        }
        else
        {
            speed = oldspeed;   // スピードを元に戻す 
            currentInterval += Time.deltaTime;  // Boostのインターバルをはかる
        }

        // ブーストインターバルが0.6秒たったら、再ブーストできるように
        if (BoostInterval < currentInterval)
        {
            bIntervalFlag = false;
        }


        //　0.2秒たったらブースト終了
        if (BoostTime < currentBoostTime)
        {
            // ブースト終了
            bBoostFlag = false;
            currentInterval = 0.0f;


            //　まだShiftが押されているなら、ダッシュ状態へ
            if (bPushKeyFlag)
            {
                bDashFlag = true;
            }
            else
            {
                bDashFlag = false;

                // リセット
                currentBoostTime = 0.0f;
                
            }
        }

        // -- ダッシュ状態 -- 
        if (bDashFlag)
        {
            speed = Dashspeed;  // ダッシュ速度を代入
        }


        if (!BoostEffectTmp) return;
        if (bPushKeyFlag) BoostEffectTmp.transform.position = transform.position;          // エフェクト追従
    }



    //------------
    // セッター
    //------------
    public void SetSpeed(float speed)
    {
        oldspeed = speed;
    }


    //------------
    // ゲッター
    //------------
    public bool GetBoostFlag
    {
        get { return bBoostFlag; }
    }

}
