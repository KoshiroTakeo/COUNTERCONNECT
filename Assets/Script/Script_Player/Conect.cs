using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conect : MonoBehaviour 
{
    private FixedJoint fixedJoint;
    [SerializeField] List<GameObject> EnemyList = new List<GameObject>();
    GameObject GameManager;
    int ConectEnemyNum;
    bool DestroyEnemy;

    public GameObject CurrentConect;
    public bool bConect;

    float time;
    float oldtime;

    // サウンド
    GameObject SE;
    SoundSE soundSE;


    EnemyHPUI EnemyHPUI;

    // エフェクト用
    [SerializeField] GameObject DamageEffect;               // ヒット時のエフェクト
    [SerializeField] GameObject ConectingEffect;           // コネクト中のエフェクト
    [SerializeField] GameObject ConectEffect;              // コネクト時のエフェクト
    [SerializeField] GameObject ConectReleaseEffect;       // コネクト解除時のエフェクト

    [SerializeField] GameObject EnemyConectingEffect;           // コネクト中のエフェクト
    [SerializeField] GameObject EnemyConectEffect;              // コネクト時のエフェクト
    [SerializeField] GameObject EnemyConectReleaseEffect;       // コネクト解除時のエフェクト

    [SerializeField] PlayerHPUI PlayerHPUIScript;          // HPUIの座標取得用
    [SerializeField] Expand_Ring Expand_Ring;              // レベル取得用

    // コネクト開始エフェクト格納
    GameObject ConectEffectTmp;

    // コネクト中のエフェクト格納
    GameObject ConectingEffectTmp;
    GameObject EnemyConectingEffectTmp;

    Vector3 PlayrePos;
    Vector3 EnemyPos;


    public bool bDeathFlag;    // 死亡フラグ用

    void Start()
    {
        bConect = false;
        //sounds = GetComponents<AudioSource>();
        GameManager = GameObject.FindWithTag("Manager");

        // 変数初期化
        ConectEnemyNum = -1;
    
        time = 0.0f;
        bDeathFlag = false;

        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");
    }


    void Update()
    {
        time += Time.deltaTime;

        //シーン上の敵を取得
        EnemyList = GameManager.GetComponent<EnemyManager>().SurviveEnemyList;

        //デバック用強制解除
        if (Input.GetMouseButtonDown(0)) bConect = false;

        // コネクト解除処理
        ReleaseConect();


        if (EnemyConectingEffectTmp  == null)
            return;

        // エフェクト追従
        EnemyConectingEffectTmp.transform.position = CurrentConect.transform.position;
        ConectingEffectTmp.transform.position = PlayerHPUIScript.transform.position;

    }

    //------------------------------------------------------------
    // コネクト解除関数
    // 戻り値：なし
    // 引き数：なし
    // 内　容：敵が死んだら、bConectをTrueに。
    //         bConectがTrueになったらコネクト解除される。
    //------------------------------------------------------------
    void ReleaseConect()
    {
        bDeathFlag = false;  // 死亡フラグOFF

        // 敵が死んだとき、コネクト解除 （コネクト中の敵番号と、死んだ敵番号が一緒なら）
        if (ConectEnemyNum == GameManager.GetComponent<EnemyManager>().DeathEnemyNumber)
        {
            bConect = false;        // コネクトフラグOFF
            bDeathFlag = true;      // 死亡フラグON
            ConectEnemyNum = -1;    // コネクト中の敵番号初期化
        }

        // --- コネクト解除 ---
        if (!bConect)
        {
            if (fixedJoint)
            {
                // --- FixedJoint削除 ---
                Destroy(fixedJoint);

                // コネクト解除SE
                SE.GetComponent<SoundSE>().PlaySE(3);

                // コネクト中のエフェクト削除
                Destroy(ConectingEffectTmp);
                Destroy(EnemyConectingEffectTmp);

                // --- エフェクト ---
                // レベルをもとに、エフェクト拡大率を求める
                PlayrePos = PlayerHPUIScript.transform.position;
                EnemyPos = CurrentConect.transform.position;

                Instantiate(ConectReleaseEffect,new Vector3(PlayrePos.x,PlayrePos.y + 0.4f,PlayrePos.z) , transform.rotation);   // プレイヤーの位置にパーティクル生成
                Instantiate(ConectReleaseEffect, new Vector3(EnemyPos.x, EnemyPos.y + 0.4f, EnemyPos.z), transform.rotation);    // エネミーの位置にパーティクル生成（敵の位置）
            }
        }
    }

    //------------------------------------------------------------
    // 当たり判定
    // 戻り値：なし
    // 引き数：Collider
    // 内　容：ColliderにIsTriggerが入ってる時これを呼ぶ
    //------------------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        // ダメージ処理
        Damage(other);


        // コネクト処理
        JudgeEnemy(other);


    }


    //------------------------------------------------------------
    // ダメージ関数
    // 戻り値：なし
    // 引き数：Collider
    // 内　容：コネクト時、敵の弾がプレイヤーのリングに当たった時、
    //         コネクト中の敵へダメージ発生。        
    //------------------------------------------------------------
    void Damage(Collider other)
    {
        // ダメージランダム生成(10 ~ 15)
        int damage = Random.Range(10, 15);

        // コネクト時リングに当たり判定を持たせる
        if (bConect)
        {
            if (other.tag == "bullet")
            {
                // 被弾SE（コネクト時）
                SE.GetComponent<SoundSE>().PlaySE(16);

                other.gameObject.SetActive(false);                                  // 弾の消去
                CurrentConect.GetComponent<EnemyHPUI>().Damage(damage,true);        // コネクト中の敵にダメージ 

                // ダメージエフェクトを青色に変更
                DamageEffect.GetComponent<ParticleSystem>().startColor = new Color(0.0f, 0.0f, 0.8f, 1.0f);
                Instantiate(DamageEffect, transform.position, transform.rotation);   //　エフェクト発生


            }
        }
    }


    //--------------------------------------------------------------------
    // コネクト関数
    // 戻り値：なし
    // 引き数：Collider
    // 内　容：EnemyリングとPlayerリング接触時、Spaceを押すとコネクト。        
    //--------------------------------------------------------------------
    void JudgeEnemy(Collider col)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            // ボム演出中なら無視
            if (gameObject.GetComponentInParent<BombScript>().GetFlag()) return;


            // 接触しているのがリングの時 & チャクラムリングがついていない & ボムリングがついていない
            if (col.gameObject.tag == "Rings" && !bConect && 
                !col.transform.root.gameObject.GetComponent<EnemyHPUI>().GetChakramFlag() &&
                !col.transform.root.gameObject.GetComponent<EnemyHPUI>().GetBombFlag())
            {
                CurrentConect = col.transform.root.gameObject;                                              // コネクトしたgameObjectを取得
                ConectEnemyNum = CurrentConect.transform.gameObject.GetComponent<EnemyHPUI>().MyNumber;     // コネクトしたgameObjectの番号を取得


                //if (time >= 0.5f)
                //{
                //    bConect = !bConect;
                //    time = 0.0f;
                //}

                bConect = true;
                time = 0.0f;

                // コネクトSE
                SE.GetComponent<SoundSE>().PlaySE(2);

                // === FixedJointでコネクトさせる === 
                if (fixedJoint == null)
                {
                    // --- プレイヤーをリングの端に動かす ---
                    Vector3 toVec = AngleVec(this.gameObject, col.gameObject);          // 敵のいる方向を取得
                    float Distans = Vector3.Distance(transform.parent.position, col.transform.parent.position); // 二点間の距離を求める
                    float MaxDistans = 2.35f + (float)Expand_Ring.GetLevel() * 0.2f;   // リングの端の位置を計算（レベルによってリングの大きさが変わるため）1
                    float DiffDistans = MaxDistans - Distans;                           // リングの端と、今のリング位置の差分を求める

                    if (DiffDistans < 0) DiffDistans = DiffDistans * -1.0f;             // マイナスになった時修正用


                    // --- プレイヤーの位置を移動させる ---
                    // 左にいるとき
                    if (toVec.x > 0)
                    {
                        transform.parent.position = new Vector3(transform.parent.position.x - DiffDistans, transform.parent.position.y, transform.parent.position.z);
                    }
                    // 右にいるとき
                    if (toVec.x < 0)
                    {
                        transform.parent.position = new Vector3(transform.parent.position.x + DiffDistans, transform.parent.position.y, transform.parent.position.z);
                    }
                    // 下にいるとき
                    if (toVec.z > 0)
                    {
                        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - DiffDistans);
                    }
                    // 上にいるとき
                    if (toVec.z < 0)
                    {
                        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z + DiffDistans);
                    }
          

                    // --- エフェクト ---
                    PlayrePos = PlayerHPUIScript.transform.position;
                    EnemyPos = CurrentConect.transform.position;

                    // コネクトエフェクト生成
                    ConectEffectTmp = Instantiate(ConectEffect, new Vector3(PlayrePos.x, PlayrePos.y + 0.4f, PlayrePos.z), transform.rotation);   // プレイヤーの位置にパーティクル生成
                    Instantiate(EnemyConectEffect, new Vector3(EnemyPos.x, EnemyPos.y + 0.4f, EnemyPos.z), transform.rotation);    // エネミーの位置にパーティクル生成（敵の位置）

                    // レベルに合わせてエフェクト大きさを変更
                    ConectEffectTmp.GetComponentInChildren<ParticleSystem>().startSize += (float)Expand_Ring.GetLevel() * 0.05f;


                    // コネクト中のエフェクト生成
                    ConectingEffectTmp = Instantiate(ConectingEffect, PlayrePos, transform.rotation);
                    EnemyConectingEffectTmp = Instantiate(EnemyConectingEffect, EnemyPos, transform.rotation);

                    // レベルに合わせてエフェクト大きさを変更
                    ConectingEffectTmp.GetComponentInChildren<ParticleSystem>().startSize += (float)Expand_Ring.GetLevel() * 0.7f;

                    // --- FixedJointで合体 ---
                    gameObject.AddComponent<FixedJoint>();                    // FixdJoint追加
                    fixedJoint = GetComponent<FixedJoint>();                  // 子オブジェクトからFixedJoint取得

                    fixedJoint.connectedBody = col.gameObject.GetComponentInParent<Rigidbody>();    // 子オブジェクトのrigidbodyをアタッチ

                    fixedJoint.breakForce = 100000.0f;
                    fixedJoint.breakTorque = 100000.0f;
                    fixedJoint.enableCollision = true;
                    fixedJoint.enablePreprocessing = true;
                }
            }

            /* 20210428 新規追加_畠山 */
            // コネクト状態を任意解除できるように
            if (bConect && time > 0.0f)
            {
                bConect = false;
                ConectEnemyNum = -1;
                time = 0.0f;
                Debug.Log("コネクト解除");
            }
        }
    }

    //--------------------------------------------------------------------
    // 敵の方向を返す関数
    // 戻り値：Vector3 
    // 引き数：GameObject プレイヤー,GameObject エネミー
    // 内　容：エネミーとプレイヤーの位置から、エネミーのいる方向を返す      
    //--------------------------------------------------------------------
    Vector3 AngleVec(GameObject Player,GameObject Enemy)
    {
        Vector3 fromVec = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        Vector3 toVec = new Vector3(Enemy.transform.position.x, 0, Enemy.transform.position.z);

        // プレイヤーとエネミーの座標の差分を正規化して返す
        return Vector3.Normalize(toVec - fromVec);
    }

    //--------------------------
    // ゲッター
    //--------------------------
    public bool GetbConect
    {
        get { return this.bConect; }
    }

    public GameObject GetConectEnemy
    {
        get { return CurrentConect; }
    }

    public int GetEnemyNumber
    {
        get { return ConectEnemyNum; }
    }
}
