using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWaveManager : MonoBehaviour
{
    // 必要なオブジェクト、スクリプトを取得
    [SerializeField] GameObject Wave;
    [SerializeField]  WaveManager WaveManagerScript;

    // 現在のWave
    public int nWaves;

    // 現在のステージ
    public int nStage;

    // ウェーブ及びステージのクリア判定
    public bool nWaveClear;

    // クリア時の遷移判定取得
    public bool MoveOn;

    // エフェクト用判定
    public bool Effect;

    // エフェクトアタッチ用オブジェクト
    public GameObject MoveEffect;

    // プレイヤー座標取得オブジェクト
    GameObject Player;

    void Start()
    {
        MoveOn = false;
        Effect = false;

        // 必要なオブジェクト、スクリプトを取得
        Wave = GameObject.Find("GameManager");
        WaveManagerScript = Wave.GetComponent<WaveManager>();

        Player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    public void StartSceneWave()
    {
        // プレイヤーの座標を取得
        Vector3 PlayerPos = Player.transform.position;

        // 必要なオブジェクト、スクリプトを取得
        nWaves = WaveManagerScript.Waves;
        nStage = WaveManagerScript.Stage;

        nWaveClear = WaveManagerScript.WaveClear;

        Debug.Log(nWaveClear);

        //// ウェーブクリアならシーン遷移
        //if (nWaveClear)
        //{

            //　シーン遷移のエフェクト発生
            Instantiate(MoveEffect, PlayerPos, Quaternion.identity);
            Effect = true;

        //if (Effect)
        //{
        //    Invoke("LoadScene", 1.0f);
        //}

        //}
    }


    // シーン遷移用関数
    void LoadScene()
    {
        FadeManager.Instance.LoadScene("Stage1-2", 0.3f);
        MoveOn = true;
    }


}
