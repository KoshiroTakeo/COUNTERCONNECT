using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// --- 更新履歴 ---
// 2021/04/15 コネクト時のダメージ判定を変更。Conect.csへ移した。
// 2021/04/23 レベルアップ処理を追加

public class PlayerHPUI : MonoBehaviour
{
    // 最大HPと現在HP
    [HideInInspector] public float MaxHP = 200;
    [HideInInspector] public float currentPlayerHP;

    public Image image;

    GameObject Player;
    Conect conectScript;
    GameObject Enemy;
    EnemyHPUI enemyHPUI;

    // プレイヤー消滅(HP0)判定
    bool PlayerLife;

    //パーティクル
    public GameObject particleHit;
    public GameObject particleDelete;

    [System.NonSerialized] public bool conect;

    // リングの配列
    private GameObject[] chakramring = new GameObject[10];
    private GameObject[] BombRing = new GameObject[16];


    // サウンド
    GameObject SE;
    SoundSE soundSE;

    bool Death; // 死亡フラグ

    [SerializeField] GameObject HPRecover;


    void Start()
    {
        // 現在HPを最大HPに
        currentPlayerHP = MaxHP;

        // HPのUI
        image.fillAmount = 1;

        // コネクトスクリプト取得
        Player = GameObject.Find("Player");
        conectScript = Player.GetComponentInChildren<Conect>();

        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");

        Death = false;
    }

 

    void Update()
    {
        // コネクト状態取得
        conect = conectScript.bConect;

 
        // 現在HPをSliderに反映、 小数点以下を見るためfloat型に
        image.fillAmount = currentPlayerHP / MaxHP;

        // 常にリングを探す
        chakramring = GameObject.FindGameObjectsWithTag("ChakramRing");
        BombRing = GameObject.FindGameObjectsWithTag("BombRing");


        // HPが0以下で消滅
        if (currentPlayerHP < 0 && !Death)
        {
            // 死亡フラグON
            Death = true;

            // エフェクト発生
            Instantiate(particleDelete, transform.position, transform.rotation);    

            // 死亡SE
            SE.GetComponent<SoundSE>().PlaySE(13);

            Destroy(gameObject, 0.1f);                                              // エネミー消滅
        }
    }

    //------------------------------------------------------------
    // 当たり判定
    // 戻り値：なし
    // 引き数：Collider
    // 内　容：弾との当たり判定を行う
    //------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // ダメージランダム生成(10 ~ 15)
        int damage = Random.Range(10, 15);

        
        // コネクトしていない時
        if (!conect)
        {
            // 弾に当たった時
            if (other.gameObject.tag == "bullet")
            {
                // ダメージエフェクト赤色
                particleHit.GetComponent<ParticleSystem>().startColor = new Color(0.8f, 0.0f, 0.0f, 1.0f);

                //　エフェクト発生
                Instantiate(particleHit, transform.position, transform.rotation);

                // 弾の消去
                other.gameObject.SetActive(false);

                // 自身の現在HPからダメージを引く
                currentPlayerHP = currentPlayerHP - damage;

                // 被弾SE
                SE.GetComponent<SoundSE>().PlaySE(15);


                // 全てのチャクラムにダメージ処理
                foreach(GameObject chakramring in chakramring)
                {
                    chakramring.GetComponent<ChakramRing>().Damage(1);
                }


                // 全てのボムリングにダメージ処理
                foreach (GameObject BombRing in BombRing)
                {
                    BombRing.GetComponent<BombRing>().Damage();
                }
            }
        }
        
    }

    // **************************************************
    // ダメージ関数
    // 田村作成

    // 呼ばれたらダメージを与える関数
    // 第一引数：与えたいダメージ量
    public void Damage(float damage)
    {
        // ボム演出中なら無視
        if (gameObject.GetComponent<BombScript>().GetFlag()) return;

        // コネクトしてるなら
        if (conectScript.bConect)
        {
            // 被弾SE（コネクト時）
            SE.GetComponent<SoundSE>().PlaySE(16);

            // エネミーのダメージを与える
            conectScript.GetConectEnemy.GetComponent<EnemyHPUI>().currentEnemyHP -= damage;
        }
        else
        {
            // チャージ中なら
            if (GetComponent<ChargeShot>().bCharge)
            {
                // ダメージをためる
                GetComponent<ChargeShot>().SetDamage((int)damage);
            }
            else
            {
                // チャージしてないならダメージを受ける
                currentPlayerHP -= damage;
            }
        }
    }


    public void Addlife(int life)
    {

        Instantiate(HPRecover, transform.position, transform.rotation);

        currentPlayerHP += life;
    }

}

