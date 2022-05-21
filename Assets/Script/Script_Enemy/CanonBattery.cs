using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBattery : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject enemy;
    public GameObject Bullet; // 弾

    //方向
    public int Pos;

    //敵との距離
    public Transform enemypos;

    //発射間隔
    public int interval;

    //経過時間
    private float time = 0f;

    void Start()
    {
        player = GameObject.Find("Player");
        //InvokeRepeating("Shot", 1, interval); // 1秒後に1秒ごとにShotを繰り返す 
    }

    void Update()
    {
        //時間を計る
        time += Time.deltaTime;

        float distance = Vector3.Distance(enemypos.position, player.transform.position);

        if (distance < 10)
        {
             //経過時間が生成時間になった時
            if (time > interval)
            {
                //経過時間を初期化して時間計測を始める
                time = 0f;
                //弾発射
                Shot();
            }
        }
    }

    void Shot() // 弾を発射する処理
    {
        GameObject Bullets = Instantiate(Bullet.gameObject, transform.position, transform.rotation); // 弾を砲台と同じ場所、同じ向きに生成する
        Vector3 Force; // 弾にかける力
        Force = transform.forward * Pos; // 弾にかける力を砲台の前方向にする
        Bullets.GetComponent<Rigidbody>().AddForce(Force); // 弾に力をかける
        //Destroy(Bullets.gameObject, 20); // 弾を2秒後に消す
    }

    private void OnTriggerEnter(Collider other)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        // if (other.gameObject.tag == "Player") 

    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    // 物体がトリガーに接触しとき、１度だけ呼ばれる
    //    if (collision.gameObject.tag == "Player")
    //        Destroy(this.gameObject);
    //}
    //void OnCollisionEnter(Collision other)
    //{
    //    // 何かに当たったら自分自身を削除
    //    if (other.gameObject.tag == "Player")
    //        Destroy(this.gameObject);
    //}
}