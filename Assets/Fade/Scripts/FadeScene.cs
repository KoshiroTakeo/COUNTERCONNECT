using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    public Fade fade;
    int index = SceneManager.GetActiveScene().buildIndex;
    float seconds;
    bool FadeTime;

    void Start()
    {
        FadeTime = false;
    }


    void Update()
    {
        if (Input.GetKeyDown (KeyCode.B))
        {
            FadeTime = true;
            FadeInOut();

            if (FadeTime)
            {
                seconds += Time.deltaTime;

                if (seconds >= 1.0f)
                {
                    FadeLoadScene(1);
                    seconds = 0;
                    FadeTime = false;
                }
            }
        }
    }

    public void FadeInOut()
    {
        Invoke("FadeIn", 0.1f);
        Invoke("FadeOut", 0.6f);
    }

    void FadeIn()
    {
        fade.FadeIn(0.5f, () => print("フェードイン完了"));
    }

    void FadeOut()
    {
        fade.FadeOut(0.5f, () => print("フェードアウト完了"));
    }

    void FadeLoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
