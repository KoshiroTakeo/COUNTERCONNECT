using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageImage : MonoBehaviour
{
    public Image image;
    private Sprite sprite;

    GameObject Player;
	PlayerMove PlayerMoveScript;


    void Start()
    {
		Player = GameObject.Find ("Player");
        PlayerMoveScript = Player.GetComponent<PlayerMove>();
	}


    void Update()
    {
		int nPathNum = PlayerMoveScript.PathNum;

        image = this.GetComponent<Image>();
        image.sprite = sprite;

        Debug.Log(nPathNum);

        // ステージ番号によって画像の表示を変更
        switch (nPathNum)
        {
            case (1):
                sprite = Resources.Load<Sprite>("ST1-1");
            break;

            case (2):
                sprite = Resources.Load<Sprite>("ST1-2");
            break;

            case (3):
                sprite = Resources.Load<Sprite>("ST1-3");
            break;

            case (4):
                sprite = Resources.Load<Sprite>("ST2-1");
            break;

            case (5):
                sprite = Resources.Load<Sprite>("ST2-2");
            break;

            case (6):
                sprite = Resources.Load<Sprite>("ST2-3");
            break;

            case (7):
                sprite = Resources.Load<Sprite>("ST3-1");
            break;

            case (8):
                sprite = Resources.Load<Sprite>("ST3-2");
            break;

            case (9):
                sprite = Resources.Load<Sprite>("ST3-3");
            break;

            case (10):
                sprite = Resources.Load<Sprite>("ST4-1");
            break;

            case (11):
                sprite = Resources.Load<Sprite>("ST4-2");
            break;

            case (12):
                sprite = Resources.Load<Sprite>("ST4-3");
            break;

            case (13):
                sprite = Resources.Load<Sprite>("ST5-1");
            break;

            case (14):
                sprite = Resources.Load<Sprite>("ST5-2");
            break;

            case (15):
                sprite = Resources.Load<Sprite>("ST5-3");
                break;

            default:
            break;
        }
    }
}
