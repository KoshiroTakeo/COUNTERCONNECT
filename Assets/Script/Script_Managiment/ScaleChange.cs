/* 03/29現在 Yキーを押すとバー拡大,Iキーでバー縮小
    Update関数のif文の条件変更で拡大方法変更可能
    Hierarchy > Scene > Player > HPにスクリプト挿入後、
    GameObject[None(Game Object)]にPlayer > HPのプレハブを挿入で使用可能。*/

/*巨大になると幅が太くなりすぎるため、そこを模索中。。。*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    // PlayerのHPバーを取得するため
    public GameObject gameObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Y)) // 拡大
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0.1f,
                                                          gameObject.transform.localScale.y + 0.1f * 0.76f);
        }
        else if (Input.GetKey(KeyCode.I))   // 縮小
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.1f,
                                                          gameObject.transform.localScale.y - 0.1f * 0.76f);
        }
    }
}
