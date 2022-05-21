using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobiusRing : MonoBehaviour
{
    GameObject MebiusGauge;
    Image image;
    Conect conect;

    public float UpSpeed;
    public float DownSpeed;

    private bool bUse;

    [SerializeField] GameObject GaugeRecover;

    void Start()
    {
        bUse = false;
        conect = GetComponentInChildren<Conect>();

        MebiusGauge = GameObject.Find("UI/MebiusGauge/MebiusMetar");
        MebiusGauge.gameObject.AddComponent<Image>();
        image =  MebiusGauge.GetComponent<Image>();
    }
 

    void Update()
    {
        if (conect.bConect || GetComponent<ChargeShot>().bCharge) bUse = true;
        else if (!conect.bConect && image.fillAmount < 1) bUse = false;
        else bUse = false;
    }

    void FixedUpdate()
    {
        // コネクト時、またはチャージショット中ゲージ減少
        if (bUse)
        {
            // fillAmout減少
            image.fillAmount -= DownSpeed * Time.deltaTime * 10;

            // 0になったらコネクト解除
            if(image.fillAmount <= 0)
            {
                image.fillAmount = 0.0f;
                conect.bConect = false;
            }
        }
        else
        {
            // fillAmout上昇
            image.fillAmount += UpSpeed * Time.deltaTime * 10;
        }
    }

    
    public void AddGauge(float life)
    {
        image.fillAmount += life;

        Instantiate(GaugeRecover, transform.position, transform.rotation);
    }


    //------------------------------------------------------------
    // ゲージマイナス処理関数
    // 戻り値：なし
    // 引き数：float(減らしたいゲージ量)
    // 内　容：ゲージを減らす。0～100で設定。
    //------------------------------------------------------------
    public void MinusGauge(float gauge)
    {
        // fillamoutが1.0最大のため、値を揃える
        gauge /= 100.0f;

        // メビウスゲージ反映
        image.fillAmount -= gauge;
    }
}
