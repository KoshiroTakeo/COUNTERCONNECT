using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramShot : MonoBehaviour
{
    public GameObject ChakramPrefab;
    [Header ("弾の速度")]public float ShotSpeed= 1000.0f;
    [Header ("弾の生存時間")] public float Life = 2.0f;
    [Header("チャクラム時に減少するゲージ量(0～100)")] public float MinusGauge = 30.0f;


    private GameObject bullet;  // 生成した弾の情報を保存用
    private bool bShotFlag;     // ショット中か判定
    private float BulletLife;   // 弾生存時間


    private GameObject Player;
    private MobiusRing mobiusRing;


    // サウンド
    GameObject SE;
    SoundSE soundSE;

    Vector3 BulletPos;

    //-------------
    // start
    //-------------
    void Start()
    {
        // Playre情報取得
        Player = GameObject.FindWithTag("Player");
        mobiusRing = Player.GetComponent<MobiusRing>();


        bShotFlag = false;
        BulletLife = Life;

        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");
    }

    //-------------
    // update
    //-------------
    void Update()
    {


        // 弾を打つ
        Shot();

        // 弾を消す
        Destroy();
    }


    //------------------------------------------------------------
    // ショット処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：弾を打つ。ゲージが3割以上ないと発射できない。
    //------------------------------------------------------------
    void Shot()
    {
        // キーが押された & 弾が出ていない & ゲージが3割以上ある時
        if (Input.GetKeyDown(KeyCode.C) && !bShotFlag)
        {
            // チャクラムSE
            SE.GetComponent<SoundSE>().PlaySE(4);

            // ゲージ消費
            mobiusRing.MinusGauge(MinusGauge);

            // 弾発射中
            bShotFlag = true;

            // プレイヤーの位置取得
            BulletPos = transform.position;

            Debug.Log(transform.forward);

            // プレイヤーの向きによって、弾発射位置をずらす
            if (transform.forward.x > 0.1f)
                BulletPos.x += 1.5f;
            if (transform.forward.x < -0.1f)
                BulletPos.x -= 1.5f;

            if (transform.forward.z > 0.1f)
                BulletPos.z += 1.5f;
            if (transform.forward.z < -0.1f)
                BulletPos.z -= 1.5f;

            // 弾の生成
            bullet = GameObject.Instantiate(ChakramPrefab,BulletPos, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * ShotSpeed);
        }
    }


    //------------------------------------------------------------
    // 弾の消滅処理
    // 戻り値：なし
    // 引き数：なし
    // 内　容：弾を消す。
    //------------------------------------------------------------
    void Destroy()
    {
        // 弾の生存時間を減らす
        if (bShotFlag)
            BulletLife -= Time.deltaTime;

        // 弾の生存時間がなくなったら消える
        if (BulletLife < 0)
        {
            bShotFlag = false;
            Destroy(bullet);
            BulletLife = Life;
        }
    }
}
