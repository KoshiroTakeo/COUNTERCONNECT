using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChargeShot : MonoBehaviour
{
    // ChargeShotスクリプトから受け取るモノ(ダメージ総量を計算するため)
    private float ChargeTime;
    private int TotalDamage;

    private float damage;   // 当たった敵に与えるダメージ


    public float lifeTime;  // 消えるまでの秒数

    /*********** ゲームオブジェクト,スクリプト取得 ***********/
    GameObject player;
    ChargeShot shot;

    [SerializeField] GameObject ChargeShotEffect;           // エフェクト


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);  // 生成されてから"lifeTime"秒後に消えるように(何にも当たらなかった時用の保険)

        // ChargeShotスクリプトで算出されたダメージ総量、チャージ時間を取得
        player = GameObject.Find("Player");
        shot = player.GetComponent<ChargeShot>();
        ChargeTime = shot.ChargeTime;
        TotalDamage = shot.TotalDamage;

        damage = ChargeTime * TotalDamage / 10; // ダメージ総量 与えられるダメージの計算は今のところ適当

        // 受け取ったら元スクリプトの数値を初期化
        shot.InitStatus();

        // エフェクト生成
        ChargeShotEffect = Instantiate(ChargeShotEffect, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward);

        // エフェクトの追従
        ChargeShotEffect.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider obj)
    {
        // 敵に当たったらその敵に相応のダメージを与える
        if (obj.gameObject.tag == "Enemy")
        {
            // 関数使うために取得
            EnemyHPUI damageHP = obj.GetComponent<EnemyHPUI>();
            // バグ抑止
            if (damageHP == null)
            {
                Debug.Log("ダメージを与えたいやつがHPを持ってません");
                return;
            }

            // ダメージを与える
            damageHP.Damage(damage,true);

            // 触れたらオブジェクトを消す
            Destroy(gameObject);

            // エフェクト削除
            Destroy(ChargeShotEffect);
        }
    }
}
