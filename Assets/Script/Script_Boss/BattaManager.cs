using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattaManager : MonoBehaviour
{
    //ボスの形態管理**********************
    public enum Boss2State
    {
        VOID,           // ボス虚無0
        IDLE,           // 待機1
        ATTACK,         // 突進2
        LANDMINES,      // 地雷巻き3
        PRESS,          // ジャンプ4

        CLEAR,          // 死亡時5

        MAX,
    }
    Boss2State nowState = Boss2State.VOID;

    //************************************



    //使いそうな変数*********************
    GameObject GlassHopper; //ボス自身
    GameObject Player;      //プレイヤー
    Idole_Mode idole;       //待機変数
                            //突進変数
                            //地雷変数
                            //ジャンプ変数
                            //その他必要であれば追加する
    BattaStatus battaStatus;

    //***********************************

 




    // Start is called before the first frame update
    void Start()
    {
        GlassHopper = this.gameObject;
        battaStatus = GlassHopper.GetComponent<BattaStatus>();

        //待機変数 = this.GetComponent<待機>();
        idole = GlassHopper.GetComponent<Idole_Mode>();
        //突進変数 = this.GetComponent<突進>();
        
        //地雷変数 = this.GetComponent<地雷>();
        
        //ジャンプ変数 = this.GetComponent<ジャンプ>();
        
        //クリア変数 = this.GetComponent<クリア>();

    }



    // Update is called once per frame
    void Update()
    {
        //各関数に必要な変数を渡したほうが扱う変数に統一性が出る
        //各関数内で処理後、他形態の変数が返される
        switch (nowState)
        {
            case Boss2State.VOID:
                nowState = Boss2State.IDLE;
                Debug.Log(nowState);
                break;

            case Boss2State.IDLE:
                //待機変数.待機関数（他形態の変数、もしくはCLEARが返される）　
                idole.StartAction();
                Debug.Log(nowState);
                break;

            case Boss2State.ATTACK:
                //突進変数.突進関数（IDLE、もしくはCLEARが返される）
                Debug.Log(nowState);
                break;


            case Boss2State.LANDMINES:
                //地雷変数.地雷関数（IDLE、もしくはCLEARが返される）
                Debug.Log(nowState);
                break;

            case Boss2State.PRESS:
                Debug.Log(nowState);
                //ジャンプ変数.ジャンプ関数（IDLE、もしくはCLEARが返される）
                break;

            case Boss2State.CLEAR:
                Debug.Log(nowState);
                //クリア変数.クリア関数
                break;

            case Boss2State.MAX:    // 特に何もしない
                Debug.Log("本来使われない状態です");
                break;

            default:
                break;
        }
    }
    public void SetState(Boss2State x)
    {
        nowState = x;
    }
}
