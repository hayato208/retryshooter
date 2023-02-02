using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要
using UnityEngine.UI;

public class StatusButton : MonoBehaviour
{
    public float playerSpeed; // 移動の速さ
    public float playerShotSpeed; // 弾の移動の速さ
    public float playerShotAngleRange; // 複数の弾を発射する時の角度
    public int playerShotCount; // 弾の発射数
    public float playerShotInterval; // 弾の発射間隔（秒）
    public int playerHpMax; // HP の最大値
    public int playerHp; // HP
    public int playerGold; // 所持ゴールド
    [SerializeField]
    public TextMeshProUGUI m_goldText; // Gold表示用

    public TextMeshProUGUI m_hpMaxText; // hp表示用

    [SerializeField]
    public Text text; // 

    private int count;

    private int level;

    void Start()
    {
        // Speedボタン表示更新
        PlayerSpeedLevel();
        PlayerSpeedEnhancementDistplay();

        // ShotSpeedボタン表示更新
        PlayerShotSpeedLevel();
        PlayerShotSpeedEnhancementDistplay();
    }

    /// <summary>
    /// PlayerSpeedボタンクリック時の強化
    /// </summary>
    public void OnPlayerSpeedEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // Speed強化
        PlayerSpeedLevel();
        PlayerSpeedEnhance();
        PlayerSpeedEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }

    /// <summary>
    /// PlayerShotSpeedボタンクリック時の強化
    /// </summary>
    public void OnPlayerShotSpeedEnhancementButton()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // ShotSpeed強化
        PlayerShotSpeedLevel();
        PlayerShotSpeedEnhance();
        PlayerShotSpeedEnhancementDistplay();

        // GOLD更新
        ss.PlayerStatusUpdate();
    }

    /// <summary>
    /// PlayerSpeedLevel算出
    /// </summary>
    public void PlayerSpeedLevel()
    {
        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // レベル算出
        level = (int)((sm.playerSpeed - sm.playerIntialSpeed) / sm.playerSpeedEnhancement);
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

        ss.PlayerSpeedButtonGoldText.text = (int)(sm.playerSpeedEnhancementGold * Mathf.Pow(2, level)) + "GOLDで自機スピード強化";
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
        level = (int)((sm.playerShotSpeed - sm.playerIntialShotSpeed) / sm.playerShotSpeedEnhancement);
    }

    /// <summary>
    /// PlayerSpeedレベルアップ
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
    /// PlayerSpeedButtonの金額更新
    /// </summary>
    public void PlayerShotSpeedEnhancementDistplay()
    {
        // StatusSceneコンポーネント取得
        var ss = GameObject.Find("StatusScene").GetComponent<StatusScene>();

        // StatusManagerコンポーネント取得
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        ss.PlayerShotSpeedButtonGoldText.text = (int)(sm.playerShotSpeedEnhancementGold * Mathf.Pow(2, level)) + "GOLDでショットスピード強化";

        Debug.Log("shotspeed");
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
        */

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
                    ss.PlayerShotCountButonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDでショット数強化";
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
            ss.PlayerShotCountButonGoldText.text = "最大強化です";
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
                    ss.PlayerShotIntervalButonGoldText.text = (int)(100 * Mathf.Pow(2, count)) + "GOLDでショット間隔短縮";
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
            ss.PlayerShotIntervalButonGoldText.text = "最大強化です";
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
                    ss.PlayerHpMaxButonGoldText.text = (int)(200 * Mathf.Pow(2, count)) + "GOLDで最大HP強化";
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
            ss.PlayerHpMaxButonGoldText.text = "最大強化です";
        }

        // 最大HP表示
        m_hpMaxText.text = "MAX HP:" + sm.playerHpMax;

        // 所持GOLD表示
        m_goldText.text = "GOLD:" + sm.playerGold;
    }

    public void OnGameSceneClick()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // GameSceneLoadedでStatusManagerのデータを渡せないので、一旦値を保存
        // fix_20230202_GameSceneLoadedでStatusManagerのデータを直接渡したい
        playerSpeed = sm.playerSpeed;
        playerShotSpeed = sm.playerShotSpeed;
        playerShotAngleRange = sm.playerShotAngleRange;
        playerShotCount = sm.playerShotCount;
        playerShotInterval = sm.playerShotInterval;
        playerHpMax = sm.playerHpMax;
        playerHp = sm.playerHp;
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
        // var Player = GameObject.Find("Player").GetComponent<Player>();
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();
        Debug.Log("StatusPlayerGold" + sm.playerGold);

// StatusManagerにPlayerステータスを渡す
        sm.playerSpeed = playerSpeed;
        sm.playerShotSpeed = playerShotSpeed;
        sm.playerGold = playerGold;

        /*
        Player.m_speed = m_speed;
        Player.m_shotSpeed = m_shotSpeed;
        Player.m_shotAngleRange = m_shotAngleRange;
        Player.m_shotCount = m_shotCount;
        Player.m_shotInterval = m_shotInterval;

        Player.m_hpMax = m_hpMax;
        Player.playerGold = sm.playerGold;
        */

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

}
