using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour 
{
    // 文字の点滅速度
    public float speed = 1.0f;
	public float EnterSpeed = 10.0f;

    // テキスト画像読み込み
    private Text text;
    private Image image;
    private float time;
	private Text textGame;
    private Text textEnd;

	private bool textChange;
	private bool bEnterSpeed;

    private enum ObjType
	{
        TEXT = 0,
        IMAGE = 1,
		MAXOBJTYPE
    };

    private ObjType thisObjType = ObjType.TEXT;

    void Start() 
	{
        if (this.gameObject.GetComponent<Image>()) 
		{
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }
		else if (this.gameObject.GetComponent<Text>()) 
		{
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }

		textChange = false;
		bEnterSpeed = false;

		//textGame = GameObject.Find("GameStart").GetComponent<Text>();
        //textEnd = GameObject.Find("GameEnd").GetComponent<Text>();
    }

    void Update () 
	{

        if (thisObjType == ObjType.IMAGE)
		{
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.TEXT) 
		{
            text.color = GetAlphaColor(text.color);
        }

		if(Input.GetKeyDown(KeyCode.Space))
		{
			bEnterSpeed = true;
			Destroy (this.gameObject, 1.0f);
		}
		//Invoke("StartText",2.0f);
    }

	// 点滅の計算
    Color GetAlphaColor(Color color) 
	{
		// 通常時
		if(!bEnterSpeed)
		{
			time += Time.deltaTime * 5.0f * speed;
			color.a = Mathf.Sin(time) * 0.5f + 0.5f;
		}

		// 消滅時
		else
		{
			time += Time.deltaTime * 5.0f * EnterSpeed;
			color.a = Mathf.Sin(time) * 50.0f;
		}
        return color;
    }

	void StartText()
	{
		textGame.text = "G:Game";
		textEnd.text = "E:End";
	}
}