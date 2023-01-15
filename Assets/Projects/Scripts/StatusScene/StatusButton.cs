using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要
using UnityEngine.UI;

public class StatusButton : MonoBehaviour
{
    public float m_speed; // 移動の速さ
    public float m_shotSpeed; // 弾の移動の速さ
    public float m_shotAngleRange; // 複数の弾を発射する時の角度
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）
    public int m_hpMax; // HP の最大値
    public int m_gold; // 所持ゴールド
    [SerializeField]
    public TextMeshProUGUI m_goldText; // Gold表示用

    public TextMeshProUGUI m_hpMaxText; // hp表示用

    [SerializeField]
    public Text text; // 

    private int count;

    public void OnBoostSpeed()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;


        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(0.05 + 0.025 * count);

            if ((Mathf.Approximately(sm.m_speed, boostPerPiece)))
            {
                if (sm.m_gold >= 100 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.m_speed += (float)0.025;
                    sm.m_gold -= (int)(100 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.m_speedGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDで自機スピード強化";
                    break;
                }
                else
                {
                    // GOLD不足
                    break;
                }
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_speedGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.m_gold;
    }

    public void OnBoostShotSpeed()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(0.1 + 0.05 * count);


            if ((Mathf.Approximately(sm.m_shotSpeed, boostPerPiece)))
            {
                if (sm.m_gold >= 100 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.m_shotSpeed += (float)0.05;
                    sm.m_gold -= (int)(100 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.m_shotSpeedGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショットスピード強化";
                    break;
                }
                else
                {
                    // GOLD不足
                    break;
                }
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_shotSpeedGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.m_gold;
    }

    public void OnBoostShotCount()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

        while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(1 + 1 * count);

            if ((Mathf.Approximately(sm.m_shotCount, boostPerPiece)))
            {
                if (sm.m_gold >= 200 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.m_shotCount += 1;
                    sm.m_gold -= (int)(200 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.m_shotCountGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDでショット数強化";
                    break;
                }
                else
                {
                    // GOLD不足
                    break;
                }
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_shotCountGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.m_gold;
    }

    public void OnShotInterval()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

             while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(2 - 0.3 * count);

            if ((Mathf.Approximately(sm.m_shotInterval, boostPerPiece)))
            {
                if (sm.m_gold >= 100 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.m_shotInterval -= (float)0.3;
                    sm.m_gold -= (int)(100 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.m_shotIntervalGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショット間隔短縮";
                    break;
                }
                else
                {
                    // GOLD不足
                    break;
                }
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_shotIntervalGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.m_gold;
    }

    public void OnBoosthpMax()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // 繰り返しカウント
        count = 0;

           while (count < 6)
        {
            // 1レベル当たりの上昇量
            float boostPerPiece = (float)(3 + 1 * count);

            if ((Mathf.Approximately(sm.m_hpMax, boostPerPiece)))
            {
                if (sm.m_gold >= 200 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.m_hpMax += 1;
                    sm.m_gold -= (int)(200 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    count++;
                    ss.m_hpMaxGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDで最大HP強化";
                    break;
                }
                else
                {
                    // GOLD不足
                    break;
                }
            }
            count++;
        }

        // 最大レベル
        if (count >= 6)
        {
            ss.m_hpMaxGoldText.text = "最大強化です";
        }

        // 最大HP表示
        m_hpMaxText.text = "MAX HP:" + sm.m_hpMax;

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.m_gold;
    }

    public void OnGameSceneClick()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        m_speed = sm.m_speed;
        m_shotSpeed = sm.m_shotSpeed;
        m_shotCount = sm.m_shotCount;
        m_shotInterval = sm.m_shotInterval;
        m_hpMax = sm.m_hpMax;
        m_gold = sm.m_gold;

        StatusScene();
    }

    void StatusScene()
    {

        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("GameScene");
    }

    public void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {

        // GameSceneのPlayerコンポーネント
        var Player = GameObject.Find("Player").GetComponent<Player>();

        Player.m_speed = m_speed;
        Player.m_shotSpeed = m_shotSpeed;
        Player.m_shotAngleRange = m_shotAngleRange;
        Player.m_shotCount = m_shotCount;
        Player.m_shotInterval = m_shotInterval;

        Player.m_hpMax = m_hpMax;
        Player.m_gold = m_gold;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

}
