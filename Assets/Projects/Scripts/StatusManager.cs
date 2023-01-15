using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要

public class StatusManager : MonoBehaviour
{
    // Playerステータス
    public float m_speed; // 移動の速さ
    public float m_shotSpeed; // 弾の移動の速さ
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）
    public int m_hpMax; // HP の最大値
    public int m_gold; // 所持ゴールド

    // Playerステータスここまで

    // 戦闘時間
    private int minute; // 戦闘時間：分
    private float seconds; // 戦闘時間：秒

    // 戦闘時間ここまで
    private int count;

    [SerializeField]
    public TextMeshProUGUI m_goldText; // Gold表示用
    public TextMeshProUGUI m_hpMaxText; // hp表示用
    void Start()
    {
        // 最大HP表示
        m_hpMaxText.text = "MAX HP:" + m_hpMax;

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + m_gold;

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

            if ((Mathf.Approximately(m_speed, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.m_speedGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDで自機スピード強化";
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
                    ss.m_speedGoldText.text = "最大強化です";
                    break;
                }
            }
        }
        return;
    }

    public void PrintBoostShotSpeed()
    {
        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(0.1 + 0.05 * count);

            var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

            if ((Mathf.Approximately(m_shotSpeed, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.m_shotSpeedGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショットスピード強化";
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
                    ss.m_shotSpeedGoldText.text = "最大強化です";
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

            if ((Mathf.Approximately(m_shotCount, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.m_shotCountGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDでショット数強化";
                break;
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_shotCountGoldText.text = "最大強化です";
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

            if ((Mathf.Approximately(m_shotInterval, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.m_shotIntervalGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショット間隔短縮";
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
                    ss.m_shotIntervalGoldText.text = "最大強化です";
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

            if ((Mathf.Approximately(m_hpMax, boostPerPiece)))
            {
                // buttonの消費GOLD更新
                ss.m_hpMaxGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDで最大HP強化";
                break;
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_hpMaxGoldText.text = "最大強化です";
        }
    }

    public void StatusScene()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("StatusScene");
    }

    public void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var sm = GameObject.Find("Player").GetComponent<Player>();
        sm.m_hpMax = m_hpMax;

        // ScoreManager登録
        // var sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        // new GameObject("GameObject").AddComponent<ScoreManager>().m_hpMax = m_hpMax;

        SceneManager.sceneLoaded -= GameSceneLoaded;

        /*
                // データを渡す処理
                sm.m_hpMax = m_hpMax;

                // イベントから削除
                SceneManager.sceneLoaded -= GameSceneLoaded;
        */
    }
}
