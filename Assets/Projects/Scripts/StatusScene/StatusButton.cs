using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要
using UnityEngine.UI;

public class StatusButton : MonoBehaviour
{
    private float playerShotSpeed; // Playerの弾の速度
    private float playerShotInterval; // Playerの弾の発射間隔
    private float playerSpeed; // Playerの移動の速さ
    private int playerHpMax; // Playerの最大HP
    private int playerShotCount; // Playerの弾の発射数
    private float playerShotAngleRange; // Playerが複数の弾を発射する際の角度
    private int playerGold; // Playerの所持ゴールド
    private int level; // Playerレベル（強化時一時的に値を保持しておくためのもの、staticによる代用可能）

    void Start()
    {
        // ShotSpeedボタン表示更新
        PlayerShotSpeedLevel();
        PlayerShotSpeedEnhancementDistplay();

        // Speedボタン表示更新
        PlayerSpeedLevel();
        PlayerSpeedEnhancementDistplay();

        // ShotIntervalボタン表示更新
        PlayerShotIntervalLevel();
        PlayerShotIntervalEnhancementDistplay();

        // HpMaxボタン表示更新
        PlayerHpMaxLevel();
        PlayerHpMaxEnhancementDistplay();

        // ShotCountボタン表示更新
        PlayerShotCountLevel();
        PlayerShotCountEnhancementDistplay();
    }

    /// <summary>
    /// PlayerShotSpeedボタンクリック時の強化
    /// </summary>
    public void OnPlayerShotSpeedEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();
        // StatusSceneコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // ShotSpeed強化
        PlayerShotSpeedLevel();
        if (level >= sm.playerShotSpeedLevelMax)
        {
            return;
        }
        PlayerShotSpeedEnhance();
        PlayerShotSpeedEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }

    /// <summary>
    /// PlayerShotIntervalボタンクリック時の強化
    /// </summary>
    public void OnPlayerShotIntervalEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();
        // StatusSceneコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // ShotInterval強化
        PlayerShotIntervalLevel();
        if (level >= sm.playerShotIntervalLevelMax)
        {
            //最大レベル
            return;
        }
        PlayerShotIntervalEnhance();
        PlayerShotIntervalEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }

    /// <summary>
    /// PlayerSpeedボタンクリック時の強化
    /// </summary>
    public void OnPlayerSpeedEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();
        // StatusSceneコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // Speed強化
        PlayerSpeedLevel();
        if (level >= sm.playerSpeedLevelMax)
        {
            // 最大レベル
            return;
        }
        PlayerSpeedEnhance();
        PlayerSpeedEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }


    /// <summary>
    /// PlayerHpMaxボタンクリック時の強化
    /// </summary>
    public void OnPlayerHpMaxEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();
        // StatusSceneコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // HpMax強化
        PlayerHpMaxLevel();
        if (level >= sm.playerHpMaxLevelMax)
        {
            // 最大レベル
            return;
        }
        PlayerHpMaxEnhance();
        PlayerHpMaxEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }

    /// <summary>
    /// PlayerShotCountボタンクリック時の強化
    /// </summary>
    public void OnPlayerShotCountEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();
        // StatusSceneコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // ShotCount強化
        PlayerShotCountLevel();
        if (level >= sm.playerShotCountLevelMax)
        {
            // 最大レベル
            return;
        }
        PlayerShotCountEnhance();
        PlayerShotCountEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }


    // ShotSpeed
    /// <summary>
    /// PlayerShotSpeedLevel算出
    /// </summary>
    public void PlayerShotSpeedLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベル算出
        level = (int)Mathf.Round((sm.playerShotSpeed - sm.playerIntialShotSpeed) / sm.playerShotSpeedEnhancement);
    }

    /// <summary>
    /// PlayerShotSpeedレベルアップ
    /// </summary>
    public void PlayerShotSpeedEnhance()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベルアップ
        if (sm.playerGold >= sm.playerShotSpeedEnhancementGold * Mathf.Pow(2, level))
        {
            sm.playerShotSpeed += sm.playerShotSpeedEnhancement;
            sm.playerGold -= (int)(sm.playerShotSpeedEnhancementGold * Mathf.Pow(2, level));

            // レベル更新
            PlayerShotSpeedLevel();
        }
    }
    /// <summary>
    /// PlayerShotSpeedButtonの金額更新
    /// </summary>
    public void PlayerShotSpeedEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

                // 最大レベル
        if (level >= sm.playerShotSpeedLevelMax)
        {
            ss.PlayerShotSpeedButtonGoldText.text = "最大強化です";
            return;
        }

        ss.PlayerShotSpeedButtonGoldText.text = (int)(sm.playerShotSpeedEnhancementGold * Mathf.Pow(2, level)) + "GOLDでショットスピード強化";
    }

    // ShotInterval
    /// <summary>
    /// PlayerShotIntervalLevel算出
    /// </summary>
    public void PlayerShotIntervalLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        level = (int)Mathf.Round((sm.playerShotInterval - sm.playerIntialShotInterval) / sm.playerShotIntervalEnhancement);
    }

    /// <summary>
    /// PlayerShotIntervalレベルアップ
    /// </summary>
    public void PlayerShotIntervalEnhance()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベルアップ
        if (sm.playerGold >= sm.playerShotIntervalEnhancementGold * Mathf.Pow(2, level))
        {
            sm.playerShotInterval += sm.playerShotIntervalEnhancement;
            sm.playerGold -= (int)(sm.playerShotIntervalEnhancementGold * Mathf.Pow(2, level));

            // レベル更新
            PlayerShotIntervalLevel();
        }
    }


    /// <summary>
    /// PlayerShotIntervalButtonの金額更新
    /// </summary>
    public void PlayerShotIntervalEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // 最大レベル
        if (level >= sm.playerShotIntervalLevelMax)
        {
            ss.PlayerShotIntervalButtonGoldText.text = "最大強化です";
            return;
        }

        ss.PlayerShotIntervalButtonGoldText.text = (int)(sm.playerShotIntervalEnhancementGold * Mathf.Pow(2, level)) + "GOLDでショット間隔短縮";
    }



    //PlayerSpeed
    /// <summary>
    /// PlayerSpeedLevel算出
    /// </summary>
    public void PlayerSpeedLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベル算出
        level = (int)Mathf.Round((sm.playerSpeed - sm.playerIntialSpeed) / sm.playerSpeedEnhancement);
    }

    /// <summary>
    /// PlayerSpeedレベルアップ
    /// </summary>
    public void PlayerSpeedEnhance()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベルアップ
        if (sm.playerGold >= sm.playerSpeedEnhancementGold * Mathf.Pow(2, level))
        {
            sm.playerSpeed += sm.playerSpeedEnhancement;
            sm.playerGold -= (int)(sm.playerSpeedEnhancementGold * Mathf.Pow(2, level));

            // レベル更新
            PlayerSpeedLevel();
        }
    }
    /// <summary>
    /// PlayerSpeedButtonの金額更新
    /// </summary>
    public void PlayerSpeedEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // 最大レベル
        if (level >= sm.playerSpeedLevelMax)
        {
            ss.PlayerSpeedButtonGoldText.text = "最大強化です";
            return;
        }

        ss.PlayerSpeedButtonGoldText.text = (int)(sm.playerSpeedEnhancementGold * Mathf.Pow(2, level)) + "GOLDで自機スピード強化";
    }

    //PlayerHpmax
    /// <summary>
    /// PlayerHpMaxLevel算出
    /// </summary>
    public void PlayerHpMaxLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベル算出
        level = (int)Mathf.Round((sm.playerHpMax - sm.playerIntialHpMax) / sm.playerHpMaxEnhancement);
    }

    /// <summary>
    /// PlayerHpMaxレベルアップ
    /// </summary>
    public void PlayerHpMaxEnhance()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベルアップ
        if (sm.playerGold >= sm.playerHpMaxEnhancementGold * Mathf.Pow(2, level))
        {
            sm.playerHpMax += sm.playerHpMaxEnhancement;
            sm.playerGold -= (int)(sm.playerHpMaxEnhancementGold * Mathf.Pow(2, level));

            // レベル更新
            PlayerHpMaxLevel();
        }
    }
    /// <summary>
    /// PlayerHpMaxButtonの金額更新
    /// </summary>
    public void PlayerHpMaxEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // 最大レベル
        if (level >= sm.playerHpMaxLevelMax)
        {
            ss.PlayerHpMaxButtonGoldText.text = "最大強化です";
            return;
        }

        ss.PlayerHpMaxButtonGoldText.text = (int)(sm.playerHpMaxEnhancementGold * Mathf.Pow(2, level)) + "GOLDで最大HP強化";
    }

    //PlayerShotCount
    /// <summary>
    /// PlayerShotCountLevel算出
    /// </summary>
    public void PlayerShotCountLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベル算出
        level = (int)Mathf.Round((sm.playerShotCount - sm.playerIntialShotCount) / sm.playerShotCountEnhancement);
    }

    /// <summary>
    /// PlayerShotCountレベルアップ
    /// </summary>
    public void PlayerShotCountEnhance()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベルアップ
        if (sm.playerGold >= sm.playerShotCountEnhancementGold * Mathf.Pow(2, level))
        {
            sm.playerShotCount += sm.playerShotCountEnhancement;
            sm.playerGold -= (int)(sm.playerShotCountEnhancementGold * Mathf.Pow(2, level));

            // レベル更新
            PlayerShotCountLevel();
        }
    }
    /// <summary>
    /// PlayerShotCountButtonの金額更新
    /// </summary>
    public void PlayerShotCountEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

                // 最大レベル
        if (level >= sm.playerShotCountLevelMax)
        {
            ss.PlayerShotCountButtonGoldText.text = "最大強化です";
            return;
        }

        ss.PlayerShotCountButtonGoldText.text = (int)(sm.playerShotCountEnhancementGold * Mathf.Pow(2, level)) + "GOLDでショット数強化";
    }






    /*
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


                if ((Mathf.Approximately(sm.playerShotSpeed, boostPerPiece)))
                {
                    if (sm.playerGold >= 100 * Mathf.Pow(2, count))
                    {
                        // レベルアップ
                        sm.playerShotSpeed += (float)0.05;
                        sm.playerGold -= (int)(100 * Mathf.Pow(2, count));

                        // buttonの消費GOLD更新
                        // なぜかcount余分にインクリメントする必要がある
                        count++;
                        ss.PlayerShotSpeedGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショットスピード強化";
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
                ss.PlayerShotSpeedGoldText.text = "最大強化です";
            }

            // 所持GOLD表示
            m_goldText.text = "GOLD:" + sm.playerGold;
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

            if ((Mathf.Approximately(sm.playerShotCount, boostPerPiece)))
            {
                if (sm.playerGold >= 200 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.playerShotCount += 1;
                    sm.playerGold -= (int)(200 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.PlayerShotCountButtonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDでショット数強化";
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
            ss.PlayerShotCountButtonGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.playerGold;
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

            if ((Mathf.Approximately(sm.playerShotInterval, boostPerPiece)))
            {
                if (sm.playerGold >= 100 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.playerShotInterval -= (float)0.3;
                    sm.playerGold -= (int)(100 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    // なぜかcount余分にインクリメントする必要がある
                    count++;
                    ss.PlayerShotIntervalButtonGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショット間隔短縮";
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
            ss.PlayerShotIntervalButtonGoldText.text = "最大強化です";
        }

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.playerGold;
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

            if ((Mathf.Approximately(sm.playerHpMax, boostPerPiece)))
            {
                if (sm.playerGold >= 200 * Mathf.Pow(2, count))
                {
                    // レベルアップ
                    sm.playerHpMax += 1;
                    sm.playerGold -= (int)(200 * Mathf.Pow(2, count));

                    // buttonの消費GOLD更新
                    count++;
                    ss.PlayerHpMaxButtonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDで最大HP強化";
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
            ss.PlayerHpMaxButtonGoldText.text = "最大強化です";
        }

        // 最大HP表示
        m_hpMaxText.text = "MAX HP:" + sm.playerHpMax;

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.playerGold;
    }
        */


    public void OnGameSceneClick()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // GameSceneLoadedでStatusManagerのデータを渡せないので、一旦値を保存
        // fix_20230202_GameSceneLoadedでStatusManagerのデータを直接渡したい
        playerShotSpeed = sm.playerShotSpeed;
        playerShotInterval = sm.playerShotInterval;
        playerSpeed = sm.playerSpeed;
        playerHpMax = sm.playerHpMax;
        playerShotCount = sm.playerShotCount;
        playerShotAngleRange = sm.playerShotAngleRange;
        playerGold = sm.playerGold;

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
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusManagerにPlayerステータスを渡す
        sm.playerShotSpeed = playerShotSpeed;
        sm.playerShotInterval = playerShotInterval;
        sm.playerSpeed = playerSpeed;
        sm.playerHpMax = playerHpMax;
        sm.playerShotCount = playerShotCount;
        sm.playerGold = playerGold;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

}
