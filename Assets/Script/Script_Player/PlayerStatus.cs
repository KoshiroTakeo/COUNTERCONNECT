using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    // Expのゲージ用
    GameObject obj;
    Image ExpGauge;     // ゲージのimageを取得


    // -- レベルアップ処理用 --
    int Level;           // 現在レベル
    float CurrentExp;            // 現在の経験値
    float MaxExp;                // レベルアップに必要な経験値
    float DiffExp;               // レベルアップ時に余った値を保存

    float count;                // ゲージ増加カウント
    float CountExp;             // ゲージ増加カウント
    float temp;

    [Header("経験値増加スピード")] public float ExpUpSpeed = 0.1f;
    [Header("増加スピード（x倍）")] public float UpSpeed = 1.5f;
    [Header("増加ライフ（x倍）")] public float UpLife = 1.5f;


    GameObject EnemyManager;
    GameObject Player;
    Conect conectScript;

    // -- エフェクト用 --
    public ParticleSystem LevelUpEffect;
    private Vector3 scale;
    private ParticleSystem particlePos;


    // サウンド
    GameObject SE;
    SoundSE soundSE;

    bool bLevelUpFlag;

    //----------------------
    // 初期化
    //----------------------
    void Start()
    {
        // 変数初期化
        Level = 1;
        CurrentExp = 0;
        MaxExp = 15;
        EnemyManager = GameObject.FindWithTag("Manager");
        count = 0;
        temp = 0;

        // エフェクトサイズ初期化
        LevelUpEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);

        // コネクトスクリプト取得
        Player = GameObject.Find("Player");
        conectScript = Player.GetComponentInChildren<Conect>();

        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");


        obj = GameObject.Find("UI/ExpGauge/Gauge");
        obj.AddComponent<Image>();
        ExpGauge = obj.GetComponent<Image>();


        bLevelUpFlag = false;
    }


    //----------------------
    // 更新
    //----------------------
    void Update()
    {
        // レベルアップ処理
        LevelUp();

    }


    //------------------------------------------------------------
    // レベルアップ処理関数
    // 戻り値：なし
    // 引き数：なし
    // 内　容：レベルアップ処理。ステータス増加。経験値ゲージ増加。       
    //------------------------------------------------------------
    private void LevelUp()
    {
        bLevelUpFlag = false;

        // レベルアップエフェクトの座標更新
        if (particlePos != null)
            particlePos.transform.position = transform.position;
       

        // 経験値のゲージ増加
        if (count < CurrentExp)
        {
            count += ExpUpSpeed;
            temp += ExpUpSpeed;
        }
        else
        {
            count = 0.0f;
            CurrentExp = 0.0f;
        }


        // -- ゲージ反映 --
        ExpGauge.fillAmount = temp / MaxExp;


        // -- レベルアップ処理 --
        if (ExpGauge.fillAmount >= 1.0f)
        {
            bLevelUpFlag = true;

            // LevelUpSE
            SE.GetComponent<SoundSE>().PlaySE(0);

            // 初期化
            temp = 0.0f;
            CountExp = 0.0f;

            // レベルアップエフェクト生成
            particlePos= Instantiate(LevelUpEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f), new Quaternion(0, 90, 90, 0));

            // レベルアップ
            Level++;

            // --- ステータス調整 ---
            Player.GetComponent<PlayerController>().SetSpeed(Player.GetComponent<PlayerController>().speed * UpSpeed);   // スピード増加
            Player.GetComponent<PlayerHPUI>().MaxHP *= UpLife;          // MaxHP増加
            Player.GetComponent<PlayerHPUI>().currentPlayerHP = Player.GetComponent<PlayerHPUI>().MaxHP;    // HPマックスまで回復

            // レベルに応じて、必要経験値が増加
            MaxExp = MaxExp + (Level * 1.1f);

            // エフェクトを大きくする
            LevelUpEffect.transform.localScale = new Vector3(LevelUpEffect.transform.localScale.x + 0.3f, LevelUpEffect.transform.localScale.y + 0.2f, LevelUpEffect.transform.localScale.z);
        }

    }

    //-----------
    // ゲッター
    //-----------
    public int GetLevel
    {
        get { return Level; }
    }
    public bool GetLevelUpFlag()
    {
        return bLevelUpFlag;
    }

    //-----------
    // セッター
    //-----------
    public void SetExp(float exp)
    {
        CurrentExp += exp;
        CountExp += exp;
    }
}
