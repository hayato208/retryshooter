using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要

public class StatusManager : MonoBehaviour
{
    //Playerステータス
    public float playerShotSpeed; // Playerの弾の速度
    public float playerShotInterval; // Playerの弾の発射間隔
    public float playerSpeed; // Playerの移動の速さ
    public int playerHpMax; // Playerの最大HP
    public int playerShotCount; // Playerの弾の発射数
    public float playerShotAngleRange; // Playerが複数の弾を発射する際の角度
    public int playerGold; // Playerの所持ゴールド

    //Playerステータスここまで

    // Player初期能力値
    public float playerIntialShotSpeed { get; set; }
    public float playerIntialShotInterval { get; set; }
    public float playerIntialSpeed { get; set; }
    public int playerIntialHpMax { get; set; }
    public int playerIntialShotCount { get; set; }

    // Player初期能力値ここまで

    // Playerステータス上昇量
    public float playerShotSpeedEnhancement;
    public float playerShotIntervalEnhancement;
    public float playerSpeedEnhancement;
    public int playerHpMaxEnhancement;
    public int playerShotCountEnhancement;
    // Playerステータス上昇量ここまで

    // Playerステータス最大値
    public int playerShotSpeedLevelMax;
    public int playerShotIntervalLevelMax;
    public int playerSpeedLevelMax;
    public int playerHpMaxLevelMax;
    public int playerShotCountLevelMax;

    //PlayerステータスGOLD上昇量
    public float playerShotSpeedEnhancementGold;
    public float playerShotIntervalEnhancementGold;
    public int playerSpeedEnhancementGold;
    public int playerHpMaxEnhancementGold;
    public int playerShotCountEnhancementGold;
    // PlayerステータスGOLD上昇量ここまで


    private int count;

    void Awake()
    {
        // Playerステータスの初期値格納
        playerIntialSpeed = playerSpeed;
        playerIntialShotSpeed = playerShotSpeed;
        playerIntialShotCount = playerShotCount;
        playerIntialShotInterval = playerShotInterval;
        playerIntialHpMax = playerHpMax;
    }
    void Start()
    {
        /*
       // 最大HP表示
       m_hpMaxText.text = "MAX HP:" + playerHpMax;

       // 所持GOLD表示
       m_goldText.text = "GOLD:" + playerGold;

       // speedButtonテキスト更新
       PrintBoostSpeed();

       // shotspeedButtonテキスト更新
       PrintBoostShotSpeed();

       // hotCountButtonテキスト更新
       PrintShotCount();

       // shotIntervalButtonテキスト更新
       PrintShotInterval();

       // hpMaxButtonテキスト更新
       PrintHpMaxCount();
       */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintBoostSpeed()
    {
        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(0.05 + 0.025 * count);

            var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

            if ((Mathf.Approximately(playerSpeed, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.PlayerSpeedButtonGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDで自機スピード強化";
                break;
            }
            else
            {
                // いわゆるレベルの代わり
                count++;
                if (count >= 6)
                {
                    // 最大レベル
                    // 無限ループ防止
                    ss.PlayerSpeedButtonGoldText.text = "最大強化です";
                    break;
                }
            }
        }
        return;
    }

    public void PrintShotCount()
    {
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(1 + 1 * count);

            if ((Mathf.Approximately(playerShotCount, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.PlayerShotCountButtonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDでショット数強化";
                break;
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.PlayerShotCountButtonGoldText.text = "最大強化です";
        }
    }

    public void PrintShotInterval()
    {
        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(2 - 0.3 * count);

            var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

            if ((Mathf.Approximately(playerShotInterval, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.PlayerShotIntervalButtonGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショット間隔短縮";
                break;
            }
            else
            {
                // いわゆるレベルの代わり
                count++;
                if (count >= 6)
                {
                    // 最大レベル
                    // 無限ループ防止
                    ss.PlayerShotIntervalButtonGoldText.text = "最大強化です";
                    break;
                }
            }
        }
    }

    public void PrintHpMaxCount()
    {
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(3 + 1 * count);

            if ((Mathf.Approximately(playerHpMax, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.PlayerHpMaxButtonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDで最大HP強化";
                break;
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.PlayerHpMaxButtonGoldText.text = "最大強化です";
        }
    }

    /*
    public void StatusScene()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("StatusScene");
    }

    public void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var sm = GameObject.Find("Player").GetComponent<Player>();
        sm.playerHpMax = playerHpMax;

        // ScoreManager登録
        // var sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        // new GameObject("GameObject").AddComponent<ScoreManager>().m_hpMax = m_hpMax;

        SceneManager.sceneLoaded -= GameSceneLoaded;

        /*
                // データを渡す処理
                sm.m_hpMax = m_hpMax;

                // イベントから削除
                SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    */
}
