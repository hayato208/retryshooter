using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要

public class TimeManager : MonoBehaviour
{
    // 作業量低減のためstaticで保持
    [SerializeField]
    public static int minute; // 戦闘時間：分
    [SerializeField]
    public static float seconds; // 戦闘時間：秒
    // 前のUpdateの時の秒数
    private float oldSeconds;
    [SerializeField]
    private TextMeshProUGUI m_TimeManager; // Time表示用
    [SerializeField]
    private TextMeshProUGUI m_gameClearText; // ゲームクリア表示用
    private GameObject m_gameClear; // ゲームクリア

    // Start is called before the first frame update
    void Start()
    {
        /*
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
        */
        //timerText = GetComponentInChildren<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // ゲームクリア
        var player = GameObject.Find("Player").GetComponent<Player>();
        if (player.m_gold >= 10000)
        {
            // クリア画面
            m_gameClearText.text = "GAME CLEAR\nTIME " + m_TimeManager.text;

            // Type == Number の場合
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(minute + seconds);

            // ゲーム停止
            Time.timeScale = 0f;
        }
        */

        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }
        // 値が変わった時だけテキストUIを更新
        if ((int)seconds != (int)oldSeconds)
        {
            //m_TimeManager.text = minute.ToString() + seconds.ToString();
            m_TimeManager.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
    }
}
