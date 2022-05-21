using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFlag : MonoBehaviour
{
	 [SerializeField]
		private GameObject GameOverUIPrefab;
		private GameObject GameOverUIInstance;

		// 階層指定
		[SerializeField]
		Transform GameOverUIParent;

		void Start()
		{
       
		}

		void Update()
		{

			if (Input.GetKeyDown(KeyCode.V))
			{
				Delete();
			}
		}

		public void Delete()
		{
			Debug.Log("ゲームオーバー");
			GameOverUIInstance = GameObject.Instantiate(GameOverUIPrefab, GameOverUIParent) as GameObject;
		}
}
