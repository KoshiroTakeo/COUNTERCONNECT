using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    GameObject bomb;            // ボム情報用
    private bool UseBomb;       // 発動中か否か
    private float BombTime;     // 再利用までのインターバル
    private int BombCount;      // ボムの個数
    private int MaxBombCount;   // ボム最大数

    private float BombDirTime;  // ボムの演出時間
    private bool BombDirFlag;   // 演出が終わったか
    private bool GetBombFlag;   // 演出中か（他スクリプトに渡す用）


    // ボムリングプレハブ
    [SerializeField] GameObject BombRingPrefab;
    [SerializeField] GameObject BombEffect;
    GameObject BombEffectTmp;

    // ボム回復エフェクト
    [SerializeField] GameObject BombRecover;
    GameObject BombRecoverTmp;

    // メビウス演出用
    GameObject Mebiusl;
    Image MebiusDirecting;
    private float CurrentMebius;


    [Header("レベルアップによるボムの速度UP")] public float BombSpeed = 100.0f;
    GameObject Player;
    //int Level;
    Vector3 Speed;

    // サウンド
    GameObject SE;
    SoundSE soundSE;

    // ボムのUI
    GameObject BombUI;
    BombUI UIBombCount;


    //------------------
    // start
    //------------------
    void Start()
    {
        // 変数初期化
        BombTime = 9.0f;
        UseBomb = false;
        BombCount = 0;
        BombDirTime = 1.0f;
        MaxBombCount = 3;

        // 演出用
        // Mebius.fillAmount = 0.0f;
        CurrentMebius = 0.0f;
        GetBombFlag = false;


        Player = GameObject.FindWithTag("Player");
        //Level = Player.GetComponent<PlayerStatus>().GetLevel;   // レベル取得
        //Speed = new Vector3(Level * BombSpeed, Level * BombSpeed, Level * BombSpeed);


        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");


        Mebiusl = GameObject.Find("UI/MebiusDirecting");
        Mebiusl.AddComponent<Image>();
        MebiusDirecting = Mebiusl.GetComponent<Image>();

        BombUI = GameObject.Find("UI/BombImage/BombCount");
        BombUI.AddComponent<BombUI>();
        UIBombCount = BombUI.GetComponent<BombUI>();
    }


    //------------------
    // update
    //------------------
    void Update()
    {


        // ボムのUI用 
        UIBombCount.UpdateBomb(BombCount);


        // ボムの最大個数調整
        if (BombCount > MaxBombCount)
            BombCount = MaxBombCount;


        // ボム使用のインターバルをカウント
        if (UseBomb)
        {
            BombTime -= Time.deltaTime;

            // 演出時間
            BombDirTime -= Time.deltaTime;

            if (!BombDirFlag)
            {
                // 画面演出
                CurrentMebius += Time.deltaTime * 1.5f;
                MebiusDirecting.fillAmount = CurrentMebius / 1.0f;
            }
        }

        // インターバルが0以下になったら再利用可
        if (BombTime < 0)
        {
            BombDirFlag = false;
            UseBomb = false;
            BombTime = 9.0f;
            BombDirTime = 1.0f;
        }

        //　ボム使用中、エフェクトが追従
        if (UseBomb) BombEffectTmp.transform.position = transform.position;

        // ボム生成（16個）
        if (Input.GetKeyDown(KeyCode.B) && !UseBomb && BombCount > 0 && !Player.GetComponentInChildren<Conect>().GetbConect)
        {
            // ボムSE
            SE.GetComponent<SoundSE>().PlaySE(8);

            // 演出中
            GetBombFlag = true;

            // ボム生成フラグ
            UseBomb = true;

            // ボムを一つ減らす
            BombCount--;

            // ボムエフェクト
            BombEffectTmp = Instantiate(BombEffect, transform.position, transform.rotation);
        }

        // 演出終了時
        if (BombDirTime < 0 && !BombDirFlag)
        {
            // 演出終了
            GetBombFlag = false;

            MebiusDirecting.fillAmount = 0.0f;
            CurrentMebius = 0.0f;

            BombDirFlag = true;


            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 0.0f, transform.position.y, transform.position.z + 0.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0.0f, 0.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 2.0f, transform.position.y, transform.position.z + 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, 0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z + 4.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 1.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 6.0f, transform.position.y, transform.position.z + 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, 0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 8.0f, transform.position.y, transform.position.z + 0.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(1.0f, 0.0f, 0.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 6.0f, transform.position.y, transform.position.z - 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, -0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z - 4.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -1.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 2.0f, transform.position.y, transform.position.z - 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, -0.5f) * 1000 + Speed);



            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x + 0.0f, transform.position.y, transform.position.z + 0.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0.0f, 0.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 2.0f, transform.position.y, transform.position.z + 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, 0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z + 4.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 1.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 6.0f, transform.position.y, transform.position.z + 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, 0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 8.0f, transform.position.y, transform.position.z + 0.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 6.0f, transform.position.y, transform.position.z - 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, -0.5f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z - 4.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -1.0f) * 1000 + Speed);

            bomb = GameObject.Instantiate(BombRingPrefab, new Vector3(transform.position.x - 2.0f, transform.position.y, transform.position.z - 2.0f), transform.rotation);
            bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, -0.5f) * 1000 + Speed);



            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(1.0f, 0.0f, 0.0f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 1.0f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -1.0f) * 1000 + Speed);


            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, 0.5f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.0f, -0.5f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, 0.5f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.0f, -0.5f) * 1000 + Speed);


            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.25f, 0.0f, 0.75f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.25f, 0.0f, -0.75f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.25f, 0.0f, 0.75f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.25f, 0.0f, -0.75f) * 1000 + Speed);


            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(0.75f, 0.0f, 0.25f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbotutoridy>().AddForce(new Vector3(0.75f, 0.0f, -0.25f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.75f, 0.0f, 0.25f) * 1000 + Speed);

            //bomb = GameObject.Instantiate(BombRingPrefab, transform.position, transform.rotation);
            //bomb.GetComponent<Rigidbody>().AddForce(new Vector3(-0.75f, 0.0f, -0.25f) * 1000 + Speed);
        }

        // エフェクト追従
        if (BombEffectTmp == null) return;
        BombEffectTmp.transform.position = this.transform.position;
    }


    //--------------------------------------------------------------------
    // ボム個数追加処理
    // 戻り値：なし
    // 引き数：int(追加したい数)
    // 内　容：指定された数ボムを増やす ＆ ボム数の最大処理     
    //--------------------------------------------------------------------
    public void AddBombCount(int num)
    {
        // ボムエフェクト生成
        BombRecoverTmp = Instantiate(BombRecover, transform.position, transform.rotation);

        Destroy(BombRecoverTmp, 1.0f);
    
        // ボム加算
        BombCount += num;
    }


    // ボム演出秒数を返す
    public bool GetFlag()
    {
        return GetBombFlag;
    }

    public int GetBombCount()
    {
        return BombCount;
    }
}
