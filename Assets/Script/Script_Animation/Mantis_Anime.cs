//ボスカマキリのアニメ遷移

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis_Anime : MonoBehaviour
{
    //===== アニメ格納 =====
    private Animator Anim_Sickle;  
    private Animator Anim_Shell;  
    private Animator Anim_Body;  

    //===== 初期処理 =====
    void Start()
    {
        //変数animに、Animatorコンポーネントを設定する
        Anim_Sickle = this.gameObject.transform.Find("Boss2Sickle").GetComponent<Animator>();
        Anim_Shell = this.gameObject.transform.Find("Boss2Shell").GetComponent<Animator>();
        Anim_Body = this.gameObject.transform.Find("Boss2Body").GetComponent<Animator>();
    }

    //===== 主処理 =====
    void Update()
    {
        
        //Fan
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Anim_Sickle.SetTrigger("Fan");
            Anim_Shell.SetTrigger("Fan");
            Anim_Body.SetTrigger("Fan");           
        }

        

        //kirisaki
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Anim_Sickle.SetTrigger("Kirisaki");
            Anim_Shell.SetTrigger("Kirisaki");
            Anim_Body.SetTrigger("Kirisaki");

        }

        //shot
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Anim_Sickle.SetTrigger("Shot");
            Anim_Shell.SetTrigger("Shot");
            Anim_Body.SetTrigger("Shot");

        }

        ////Spin ※ズレる為未使用
        //if (Input.GetKey(KeyCode.Alpha4))
        //{
        //    //Anim_Sickle.SetTrigger("Spin");
        //    Anim_Shell.SetTrigger("Spin");
        //    Anim_Body.SetTrigger("Spin");

        //}

        //TailShot ※ズレ調整のためアニメーションイベントで一部制御、スピード変更
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Anim_Sickle.SetTrigger("TailShot");
            Anim_Shell.SetTrigger("TailShot");
            Anim_Body.SetTrigger("TailShot");

        }

        //Throw
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Anim_Sickle.SetTrigger("Throw");
            Anim_Shell.SetTrigger("Throw");
            Anim_Body.SetTrigger("Throw");

        }

        ////Roll ※ShellがY軸マイナスに落ちるので未使用
        //if (Input.GetKey(KeyCode.Alpha7))
        //{
        //    Anim_Sickle.SetTrigger("Roll");
        //    Anim_Shell.SetTrigger("Roll");
        //    Anim_Body.SetTrigger("Roll");

        //}
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
}

