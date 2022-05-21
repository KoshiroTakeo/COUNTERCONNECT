using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneManager : MonoBehaviour
{
    GameObject Player;
    PlayerMove PlayerMoveScript;

    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerMoveScript = Player.GetComponent<PlayerMove>();
    }

    void Update()
    {
        int ScenePathNum = PlayerMoveScript.PathNum;

        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
            {
                GetComponent<KeySound>().Decision();

                switch (ScenePathNum)
                {
                    case (1):  FadeManager.Instance.LoadScene("Stage1-1(Turtorial)", 0.3f); break;
                    case (2):  FadeManager.Instance.LoadScene("Stage1-2", 0.3f); break;
                    case (3):  FadeManager.Instance.LoadScene("Stage1-3", 0.3f); break;
                    case (4):  FadeManager.Instance.LoadScene("Stage2-1", 0.3f); break;
                    case (5):  FadeManager.Instance.LoadScene("Stage2-2", 0.3f); break;
                    case (6):  FadeManager.Instance.LoadScene("Stage2-3", 0.3f); break;
                    case (7):  FadeManager.Instance.LoadScene("Stage3-1", 0.3f); break;
                    case (8):  FadeManager.Instance.LoadScene("Stage3-2", 0.3f); break;
                    case (9):  FadeManager.Instance.LoadScene("Stage3-3", 0.3f); break;
                    case (10): FadeManager.Instance.LoadScene("Stage4-1", 0.3f); break;
                    case (11): FadeManager.Instance.LoadScene("Stage4-2", 0.3f); break;
                    case (12): FadeManager.Instance.LoadScene("Stage4-3", 0.3f); break;
                    case (13): FadeManager.Instance.LoadScene("Stage5-1", 0.3f); break;
                    case (14): FadeManager.Instance.LoadScene("Stage5-2", 0.3f); break;
                    case (15): FadeManager.Instance.LoadScene("Stage5-3", 0.3f); break;
                }
            }
        }
    }
}
