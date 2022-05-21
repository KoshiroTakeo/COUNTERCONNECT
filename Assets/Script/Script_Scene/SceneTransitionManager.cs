using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    void start()
	{

    }

	 void Update() 
	 {

        if (SceneManager.GetActiveScene().name == "TitleScene")
		{
            if (Input.anyKeyDown || Input.GetKeyDown("joystick button 1"))
            {
                Invoke("InGame", 0.3f);
                GetComponent<KeySound>().Decision();
            }
        }

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Invoke("InGame", 0.3f);
                GetComponent<KeySound>().Decision();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))	FadeManager.Instance.LoadScene ("TitleScene", 0.3f);
		//if (Input.GetKeyDown(KeyCode.M))	FadeManager.Instance.LoadScene ("Movie", 0.3f);
		//if (Input.GetKeyDown(KeyCode.E))	UnityEditor.EditorApplication.isPlaying = false; //Android用？
	 }

	 void InGame()
	 {
		FadeManager.Instance.LoadScene ("StageSelect", 0.3f);
	 }
}

