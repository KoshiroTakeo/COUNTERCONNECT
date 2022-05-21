using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    // Textオブジェクト
    public GameObject score_object;

    [SerializeField] GameObject Score;
    GameObject Timerobj;

	// メビウスゲージ非表示用
	[SerializeField] 
	private GameObject MebiusGauge;

	// 経験値バー非表示用
	[SerializeField] 
	private GameObject ExpGauge;

    // ウェーブUI非表示用
    [SerializeField]
    public GameObject WaveUIInfo;

    // タイマーテキスト非表示用
    [SerializeField]
    public GameObject Timetext;

    PlayerFollower ScoreScript;
    TimerScript TimeScript;

	// リザルト時の背景(プレハブ)アタッチ用
    [SerializeField]
    private GameObject ResultImagePrefab;
    private GameObject ResultImageInstance;

    // 階層指定
    [SerializeField]
    Transform ResultParent;

	// Update内で１度だけ回す用
    bool one;

    // スコア変数
	public int score = 0;
	public int ResultminuteTime = 0;
    public float ResultTime = 0f;
    Text scoreText;

    // リザルト用スコア
    //private Text textScore;
    public GameObject textResultScore;
    private GameObject ResultScoreInstance;
    public GameObject textResultTime;
    private GameObject ResultTimeInstance;
    private TextMeshProUGUI textClear;
   // private Text StartText;

    void Start()
    {

		// スコア
		scoreText = GameObject.Find("Score").GetComponent<Text>();

        // スコアを共有
        //textScore = GameObject.Find("TitleScore").GetComponent<Text>();
        //textResultScore = GameObject.Find("ResultScore").GetComponent<Text>();
        //textResultTime = GameObject.Find("ResultTime").GetComponent<Text>();
		textClear = GameObject.Find("GameClear").GetComponent<TextMeshProUGUI>();
        //StartText = GameObject.Find("StartText").GetComponent<Text>();

        Score = GameObject.FindWithTag("MainCamera");
        Timerobj = GameObject.FindWithTag("Timer");

        ScoreScript = Score.GetComponent<PlayerFollower>();
        TimeScript = Timerobj.GetComponent<TimerScript>();

        // リザルトスコア非表示
        //textScore.enabled = false;
        //textResultScore.enabled = false;
		//textResultTime.enabled = false;
		textClear.enabled = false;
        //StartText.enabled = false;

        one = true;
    }

    void Update()
    {
        
        Text score_text = score_object.GetComponent<Text>();

        if ((Input.GetKey(KeyCode.Z))) score++;


        scoreText.GetComponent<Text>().text = "Score:" + score.ToString("D4"); 
                                                            
        bool bGameClear = ScoreScript.gameClear;
		bool bClear = ScoreScript.bClearText;

		ResultTime = TimeScript.ResultSeconds;
		ResultminuteTime = TimeScript.Resultminute;

		// スコア表示
		score_text.text = "Score:" + score;

		// クリア時テキスト表示
		if(bClear)
		{
            
			textClear.enabled = true;
			MebiusGauge.SetActive(false);
			ExpGauge.SetActive(false);
            WaveUIInfo.SetActive(false);
            Timetext.SetActive(false);


            if (textClear.enabled) 
			{
				textClear.text = "GameClear";
			}
			score_text.enabled = false;
		}

        if (bGameClear)
        {
            
            // クリアテキストをフェードアウト
            textClear.DOFade(0.0f, 0.5f);

            // １度だけ呼ぶ
            if (one)
            {
				// 1秒後にスコア表示
                Invoke("ResultScore", 1.0f);
                Invoke("PlessSpace", 2.0f);
                one = false;
            }

            //セレクトステージに戻る*********************************
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                FadeManager.Instance.LoadScene("StageSelect", 0.3f);
            }
            //*******************************************************
        }

    }

    public void ResultScore()
    {
        ResultImageInstance = GameObject.Instantiate(ResultImagePrefab, ResultParent) as GameObject;
        ResultScoreInstance = GameObject.Instantiate(textResultScore, ResultParent) as GameObject;
        ResultTimeInstance = GameObject.Instantiate(textResultTime, ResultParent) as GameObject;

        // リザルトスコア表示
        //textResultScore.enabled = true;
        //textResultTime.enabled = true;
        //textScore.enabled = true;

        //if (textScore.enabled) textScore.text = "Clear";

        // クリア時のスコア表示
        //if (textResultScore.enabled)
        textResultScore.gameObject.GetComponent<Text>().text = "" + score;

		// クリア時のタイム表示
        //if (textResultTime.enabled)
        textResultTime.gameObject.GetComponent<Text>().text =  ResultminuteTime.ToString("00")+ ":" +((int) ResultTime).ToString ("00");

        //一時停止する
        Time.timeScale = 0f;


    }

	public void AddScore(int EnemyScore)
    {
        this.score += EnemyScore;
    }

    //void PlessSpace()
    //{
    //    StartText.enabled = true;
    //    if (StartText.enabled) StartText.text = "Space:ステージセレクト";
    //}
}   
