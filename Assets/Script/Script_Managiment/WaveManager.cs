using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ++++++++++++++++++++++++ 使い方 ++++++++++++++++++++++++
 * 
 * ******** ClearCondition ********
 * 0 : 時間経過クリア
 * 1 : 規定数敵処理でクリア
 * 2 : ボス撃破でクリア
 * 3 : テスト用(O[オー]キーでウェーブが進む)
 * ********************************
 * 
 * ******** WaveClearCondition ********
 * 入力した数値がそのウェーブをクリアする条件と
 * することができる。
 * 例えば、Wave1のクリア条件を時間経過としたとき、
 * Element0に10と入力すると、10秒時間経過でウェーブクリアと
 * することができる
 * ************************************
 * 
 * ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 */

public class WaveManager : MonoBehaviour
{
    // クリアの条件分岐
    private enum ClearType
    {
        TIME,   // = 0
        ENEMY,  // = 1
        BOSS,   // = 2
        TEST    // = 3
    };

    // 各Waveで使うステージのプレハブを取得
    public GameObject[] StageList = new GameObject[3];

    // Waveのクリア条件取得
    public int[] ClearCondition = new int[3];

    // 現在のWave
    public int Waves = 0;

    // 現在のステージ
    public int Stage = 0;

    //0525追加
    public bool BossDeath;

    // ウェーブのシーン遷移フラグ
    public bool WaveScene;

    // Waveクリアのフラグ
    public bool WaveClear;

    // 各Waveのクリア条件
    public float[] WaveClearCondition = new float[3];

    // 現在の経過状況
    private float NowTime;      // 現在時間
    private int NowEnemy;     // 倒した敵の数   (EnemyManagerで倒した敵の数を管理してるからそこからもらう)
                              //  private bool  NowBoss;      // ボスを倒したか (boolで管理してるから要らないかな)

    // 倒した敵のカウントのための情報
    private int OldWaveEnemy;

    // 必要なオブジェクト、スクリプトを取得

    EnemyManager DeathEnemyList; // 倒した敵の数取得
    public GameObject Boss;
    bool BossSpawn;

    // 0521追加_Waveのシーン遷移に必要なもの***********
    GameObject SceneWave;
    SceneWaveManager SceneWaveManagerScript;

    PlayerFollower ClearCheck;
    //*************************************************

    void Start()
    {
        // 初期化処理
        WaveClear = false;
        WaveScene = false;

        //0525追加
        BossDeath = false;


    NowTime = 0.0f;
        NowEnemy = 0;

        Debug.Log("Wave" + (Waves + 1) + " Start"); // スタートしたことをログに。
        if (Waves == 0) Debug.Log("Stage" + (Stage + 1) + " Start");


        // 0521追加_Waveのシーン遷移に必要なもの***********
        SceneWave = GameObject.Find("GameManager");
        SceneWaveManagerScript = SceneWave.GetComponent<SceneWaveManager>();

        ClearCheck = GameObject.FindWithTag("MainCamera").GetComponent<PlayerFollower>();
        //*************************************************


        StageList[Waves] = Instantiate(StageList[Waves]);   // ステージ生成

        //0426 倒した敵の数取得
        DeathEnemyList = GameObject.FindWithTag("Manager").GetComponent<EnemyManager>();

        OldWaveEnemy = DeathEnemyList.DeathEnemyCount;      // 前ウェーブで倒した敵の数取得
    }

    void Update()
    {
        // 現在の倒した敵の数を取得
        // EnemyManagerのDeathEnemyCountは初期化できるが、倒した敵の番号の初期化が難しかったので
        // 前ウェーブで倒した敵の数から現在までに倒した敵の合計を引いて算出しています
        NowEnemy = DeathEnemyList.DeathEnemyCount - OldWaveEnemy;

        // そのWaveのクリア条件から、読み込む処理を取得
        switch (ClearCondition[Waves])
        {
            case (int)ClearType.TIME:
                TimeStage();

                break;

            case (int)ClearType.ENEMY:
                EnemyStage();
                break;

            case (int)ClearType.BOSS:
                BossStage();
                break;

            case (int)ClearType.TEST:
                if (Input.GetKeyDown("o"))
                {
                    
                    WaveClear = true;
                }
                break;

            default:
                Debug.Log("不正ステージ");
                break;
        }

        // Waveがクリアされたときの処理
        if (WaveClear)
        {
            WaveScene = true;

           // 現在ステージの消去
           Destroy(StageList[Waves]);

           // Waveを1つ進める
           // 最終Waveの場合、ゲームクリアとする
           Waves++;
           if (Waves == 3)
           {
               // Debug.Log("Stage Clear!!");
               Stage++;
               Waves = 0;
                ClearCheck.Clear();
                //Start();
                
           }
           else if (Waves < 3)
           {
                // 0521 Waveクリア時にワープっぽい演出を出す(竹尾)*********
                SceneWaveManagerScript.StartSceneWave();
                //*********************************************************
                // 初期化処理をする
                Start();
           }
           else
           {
               Debug.Log("生成エラー");
           }


        
        }


    }

    // 時間経過クリアの処理
    private void TimeStage()
    {
        

        // 経過時間取得
        NowTime += Time.deltaTime;

        // 経過時間がクリアタイムを超過したらクリアフラグを立てる
        if (WaveClearCondition[Waves] <= NowTime)
        {
            Debug.Log("クリア");
            WaveClear = true;
        }
    }

    // 規定数敵撃退クリアの処理
    private void EnemyStage()
    {
        // 一定数の敵を倒したらクリアフラグを立てる
        if ((int)WaveClearCondition[Waves] <= NowEnemy)
        {

            WaveClear = true;
        }
    }

    // ボス撃退クリアの処理
    private void BossStage()
    {
        //ボス生成
        if (BossSpawn == false)
        {
            Vector3 SpawmPosition = new Vector3(0, 0, 0);
            Boss.transform.position = SpawmPosition;
            Instantiate(Boss);
            BossSpawn = true;
        }


        // ボスを倒したかどうか
        // そのままクリアフラグを立てちゃってもいいかな
        if (BossDeath == true /* ボスが死んだ情報を得る処理 */)
        {
            Debug.Log("Bossdeath");
            WaveClear = true;
        }
    }

    private void SceneMove()
    {
        // 現在ステージの消去
        Destroy(StageList[Waves]);

        // Waveを1つ進める
        // 最終Waveの場合、ゲームクリアとする
        Waves++;
        if (Waves == 3)
        {
            // Debug.Log("Stage Clear!!");
            Stage++;
            Waves = 0;
            Start();
        }
        else if (Waves < 3)
        {
            // 初期化処理をする
            Start();
        }
        else
        {
            Debug.Log("生成エラー");
        }
    }
}
