using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要

// buttonクリック時して―タスを更新するclass
public class StatusScene : MonoBehaviour
{    // Playerステータス
    public float m_speed; // 移動の速さ
    public float m_shotSpeed; // 弾の移動の速さ
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）
    public int playerHpMax; // HP の最大値
    public int playerGold; // 所持ゴールド

    // Playerステータスここまで

    // Buttonテキスト
    public int m_speedGold; // 所持ゴールド

    private int count;


    [SerializeField]
    public TextMeshProUGUI m_goldText; // Gold表示用
    public TextMeshProUGUI m_hpMaxText; // hp表示用
    public TextMeshProUGUI PlayerSpeedButtonGoldText; // speedGold表示用
    public TextMeshProUGUI PlayerShotSpeedButtonGoldText; // speedShotGold表示用
    public TextMeshProUGUI PlayerShotCountButonGoldText; // shotCountGold表示用
    public TextMeshProUGUI PlayerShotIntervalButonGoldText; // shotIntervalGold表示用
    public TextMeshProUGUI PlayerHpMaxButonGoldText; // hpGold表示用

    void Start()
    {
        // HP,GOLD更新
        PlayerStatusUpdate();
    }

/// <summary>
/// HP,GOLD更新
/// </summary>
    public void PlayerStatusUpdate()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // HP,GOLD更新
        m_goldText.text = "GOLD:" + sm.playerGold;
        m_hpMaxText.text = "HP:" + sm.playerHpMax;
    }
}
