using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
 
	[SerializeField]
	private GameObject pauseUIPrefab;
	private GameObject pauseUIInstance;
	[SerializeField] AudioSource audioSource;

	// 階層指定
	[SerializeField]
    public Transform UIParent;

	void start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
        
	}

	void Update ()
	{
        
        if (Input.GetKeyDown("q") || Input.GetKeyDown("joystick button 7")) //Menuボタン(横三本線)        変更点(前田)
        {
            // 時間停止
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab, UIParent) as GameObject;
                Time.timeScale = 0f;
                //audioSource.Pause();
            }
            // 再開
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
                //audioSource.UnPause();
            }
        }

	}
}