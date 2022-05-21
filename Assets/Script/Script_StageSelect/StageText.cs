using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageText : MonoBehaviour
{
    GameObject Player;
    PlayerMove PlayerMoveScript;

    GameObject Path;
    Path PathScript;

    public GameObject StageObj = null;

    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerMoveScript = Player.GetComponent<PlayerMove>();

        Path = GameObject.FindWithTag("Path");
        PathScript = Path.GetComponentInChildren<Path>();
    }

    void Update()
    {
        int TextPathNum = PlayerMoveScript.PathNum;
        bool TextPath = PathScript.bPath;

        Text StageText11 = StageObj.GetComponent<Text>();
        Text StageText12 = StageObj.GetComponent<Text>();

        if (TextPathNum == 1) StageText11.text = "S T A G E : 1-1";
        if (TextPathNum == 2) StageText12.text = "S T A G E : 1-2";
        if (TextPathNum == 3) StageText12.text = "S T A G E : 1-3";

        if (TextPathNum == 4) StageText11.text = "S T A G E : 2-1";
        if (TextPathNum == 5) StageText12.text = "S T A G E : 2-2";
        if (TextPathNum == 6) StageText12.text = "S T A G E : 2-3";

        if (TextPathNum == 7) StageText11.text = "S T A G E : 3-1";
        if (TextPathNum == 8) StageText12.text = "S T A G E : 3-2";
        if (TextPathNum == 9) StageText12.text = "S T A G E : 3-3";

        if (TextPathNum == 10) StageText11.text = "S T A G E : 4-1";
        if (TextPathNum == 11) StageText12.text = "S T A G E : 4-2";
        if (TextPathNum == 12) StageText12.text = "S T A G E : 4-3";

        if (TextPathNum == 13) StageText11.text = "S T A G E : 5-1";
        if (TextPathNum == 14) StageText12.text = "S T A G E : 5-2";
        if (TextPathNum == 15) StageText12.text = "S T A G E : 5-3";
    }
}
