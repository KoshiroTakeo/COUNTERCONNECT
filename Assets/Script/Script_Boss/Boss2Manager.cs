using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : MonoBehaviour
{
    //private DamageHP death;
    // =======================================
    // 動くために使う変数
    BossMove move;
    public Vector3 range;
    public float speed = 1;
    // =======================================

    private enum Boss2State
    {
        VOID,           // ボス虚無
        IDLE,           // ボス
        ATTACK,         // アタック
        CLEAR,          // HPが0を下回った

        MAX,
    }
    Boss2State nowState = Boss2State.VOID;

    //クリアフラグを立てる=====================
    ClearFlag clear;
    //=========================================

    // 攻撃に使う変数たち==========================
    ScytheManager scythe;
    HeadShoot head;
    WaistShot waist;
    TailShot tail;
    //

    //===== アニメ格納 =====
    private Animator Anim_Sickle;
    private Animator Anim_Shell;
    private Animator Anim_Body;



    // Start is called before the first frame update
    void Start()
    {
        //death = this.GetComponent<DamageHP>();
        move = this.GetComponent<BossMove>();

        head = this.GetComponent<HeadShoot>();
        waist = this.GetComponent<WaistShot>();
        scythe = this.GetComponent<ScytheManager>();
        tail = this.GetComponent<TailShot>();

        //変数animに、Animatorコンポーネントを設定する
        Anim_Sickle = this.gameObject.transform.Find("Boss2Sickle").GetComponent<Animator>();
        Anim_Shell = this.gameObject.transform.Find("Boss2Shell").GetComponent<Animator>();
        Anim_Body = this.gameObject.transform.Find("Boss2Body").GetComponent<Animator>();

        //clear = GameObject.FindWithTag("Manager").GetComponent<ClearFlag>();
    }

    // Update is called once per frame
    void Update()
    {


        switch (nowState)
        {
            case Boss2State.VOID:
                nowState = Boss2State.IDLE;
                break;

            case Boss2State.IDLE:
                nowState = Boss2State.ATTACK;
                break;

            case Boss2State.ATTACK:
                move.Move(range, speed);
                ATTACK();
                break;

            case Boss2State.CLEAR:
                Clear();
                break;

            case Boss2State.MAX:    // 特に何もしない
                Debug.Log("本来使われない状態です");
                break;

            default:
                break;
        }
    }

    void LateUpdate()
    {
        //if (death.m_bDeath)
        //{
        //    Debug.Log("クリア");
        //    nowState = Boss2State.CLEAR;

        //    clear.Clear();
        //}
    }

    // ==========================================================================================================================
    // ファンネルを起動するプログラム
    // --------------------------------------------------------------------------------------------------------------------------
    private enum Boss2Attack
    {
        VOID,
        HEAD_ATK,
        WAIST_SHOT,
        FUNNEL_BOOT,
        SCYTHE,
        SLASH,
        TAIL_SHOT,

        MAX,
    }
    Boss2Attack nowAttack = Boss2Attack.VOID;

    [Header("攻撃頻度")] public float frequency = 1;
    float time;

    void ATTACK()
    {
        time += Time.deltaTime;
        if (time >= frequency)
        {
            time = 0;
            nowAttack = (Boss2Attack)Random.Range(1, 6);
           //nowAttack = Boss2Attack.TAIL_SHOT;
        }
        else
        {
            nowAttack = Boss2Attack.VOID;
        }

        switch (nowAttack)
        {
            case Boss2Attack.VOID:
                break;

            case Boss2Attack.HEAD_ATK:
                head.Shot();

                break;

            case Boss2Attack.WAIST_SHOT:
                Anim_Sickle.SetTrigger("Shot");
                Anim_Shell.SetTrigger("Shot");
                Anim_Body.SetTrigger("Shot");

                break;

            case Boss2Attack.FUNNEL_BOOT:
                Anim_Sickle.SetTrigger("Fan");
                Anim_Shell.SetTrigger("Fan");
                Anim_Body.SetTrigger("Fan");
                break;

            case Boss2Attack.SCYTHE:
                Anim_Sickle.SetTrigger("Throw");
                Anim_Shell.SetTrigger("Throw");
                Anim_Body.SetTrigger("Throw");

                break;

            case Boss2Attack.SLASH:
                Anim_Sickle.SetTrigger("Kirisaki");
                Anim_Shell.SetTrigger("Kirisaki");
                Anim_Body.SetTrigger("Kirisaki");

                break;

            case Boss2Attack.TAIL_SHOT:
                Anim_Sickle.SetTrigger("TailShot");
                Anim_Shell.SetTrigger("TailShot");
                Anim_Body.SetTrigger("TailShot");

                break;

            case Boss2Attack.MAX:
                break;

            default:
                break;
        }
    }

    void Clear()
    {

    }

    //モーション終了処理
    public void AnimeEnd()
    {
        Debug.Log("complete");
        Anim_Sickle.SetTrigger("AnimeEnd");
        Anim_Shell.SetTrigger("AnimeEnd");
        Anim_Body.SetTrigger("AnimeEnd");
    }

    //モーション終了処理
    public void AnimeEnd_Shot()
    {
        Debug.Log("complete");
        Anim_Sickle.SetTrigger("AnimeEnd");
        Anim_Shell.SetTrigger("AnimeEnd");
        Anim_Body.SetTrigger("AnimeEnd");
    }

    //TailShot、アニメを同期させるための関数
    public void AnimeWait_TailShot()
    {
        Anim_Body.SetTrigger("Go");
    }

    //未使用
    public void AnimeWait_Spin()
    {
        Anim_Sickle.SetTrigger("Spin");
    }

    public void WaistShot()
    {
        waist.Shot();
    }

    public void TailShot()
    {
        tail.Shot();
    }

    public void Scythe()
    {
        scythe.ScytheCreate();
    }
}
