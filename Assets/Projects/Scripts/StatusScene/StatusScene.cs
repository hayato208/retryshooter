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
    public int m_hpMax; // HP の最大値
    public int m_gold; // 所持ゴールド

    // Playerステータスここまで

    // Buttonテキスト
    public int m_speedGold; // 所持ゴールド

    private int count;


    [SerializeField]
    /*
    public TextMeshProUGUI m_goldText; // Gold表示用
    public TextMeshProUGUI m_hpMaxText; // hp表示用
    */
    public TextMeshProUGUI m_speedGoldText; // speedGold表示用
    public TextMeshProUGUI m_shotSpeedGoldText; // speedShotGold表示用
    public TextMeshProUGUI m_shotCountGoldText; // shotCountGold表示用
    public TextMeshProUGUI m_shotIntervalGoldText; // shotIntervalGold表示用
    public TextMeshProUGUI m_hpMaxGoldText; // hpGold表示用

    void Start()
    {
    }

    private void Awake()
    {
        // m_hpMaxText.text="HP:"+m_hpMax;
    }

    public void StatusRelod()
    {

    }
}
