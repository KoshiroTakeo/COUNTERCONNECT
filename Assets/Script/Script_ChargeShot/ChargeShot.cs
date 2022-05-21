using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot: MonoBehaviour
{
    public int TotalDamage;     // チャージ中に受けたダメージの総量
    public float ChargeTime;    // チャージした時間
    public  bool bCharge;       // チャージ中かどうか(PlayerHPUIに判定を渡すため)

    public GameObject PlayerBullet; // チャージショットのオブジェクト

    [SerializeField] GameObject ChargeEffect;           // チャージ中のエフェクト
    private bool chargeEffect;


    // サウンド
    GameObject SE;
    SoundSE soundSE;

    // Start is called before the first frame update
    void Start()
    {
        TotalDamage = 0;
        ChargeTime = 0.0f;

        // SEオブジェクト取得
        SE = GameObject.FindWithTag("SE");
    }

    // Update is called once per frame
    void Update()
    {
        // エフェクト追従
        ChargeEffect.transform.position = transform.position;

        if (Input.GetKey("e") || Input.GetKey("joystick button 3")) //Yボタン        変更点(前田)
        {
            bCharge = true;
            ChargeTime += Time.deltaTime;


            // まだエフェクトが出ていないなら
            if (!chargeEffect)
            {
                // チャージエフェクト
                ChargeEffect = Instantiate(ChargeEffect, transform.position, transform.rotation);

                chargeEffect = true;

                // エフェクト表示
                ChargeEffect.SetActive(true);
            }
        }

        // Eキーを離した瞬間に、ダメージを受けているならチャージショットをする
        if (Input.GetKeyUp("e") || Input.GetKeyUp("joystick button 3") && TotalDamage > 0)  //変更点(前田)
        {
            // なっている音を止める
            SE.GetComponent<SoundSE>().Stop();

            chargeEffect = false;

            // エフェクト非表示
            ChargeEffect.SetActive(false);

            // チャージSE
            SE.GetComponent<SoundSE>().PlaySE(6);

            Instantiate(PlayerBullet, transform.position, transform.rotation);
            bCharge = false;
        }
        else if (Input.GetKeyUp("e") || Input.GetKeyUp("joystick button3") && TotalDamage == 0) //変更点(前田)
        {
            bCharge = false;
            InitStatus();
        }
    }

    /*
     * SetDamage関数
     * プレイヤーが実際にダメージを受けるところで、eキーが押されているならダメージをためるようにする
     * 引数 : damage くらうダメージ量
     * */
    public void SetDamage(int damage)
    {
        // チャージSE
        SE.GetComponent<SoundSE>().PlaySE(5);

        TotalDamage += damage * 5;
    }

    /*
     * InitStatus関数
     * TotalDamageや、ChargeTimeの値を受け渡した後に、それぞれの値を初期化させるために呼ばせる。
     */
    public void InitStatus()
    {
        TotalDamage = 0;
        ChargeTime = 0.0f;
    }
}
