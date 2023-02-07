using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要

// buttonクリック時して―タスを更新するclass
public class StatusScene : MonoBehaviour
{

    // Buttonテキスト
    public TextMeshProUGUI PlayerGoldText; // Gold表示用
    public TextMeshProUGUI PlayerHpMaxText; // hp表示用
    public TextMeshProUGUI PlayerShotSpeedButtonGoldText; // speedShotGold表示用
    public TextMeshProUGUI PlayerShotIntervalButtonGoldText; // shotIntervalGold表示用
    public TextMeshProUGUI PlayerSpeedButtonGoldText; // speedGold表示用
    public TextMeshProUGUI PlayerHpMaxButtonGoldText; // hpGold表示用
    public TextMeshProUGUI PlayerShotCountButtonGoldText; // shotCountGold表示用

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
        PlayerGoldText.text = "GOLD:" + sm.playerGold;
        PlayerHpMaxText.text = "MAX HP:" + sm.playerHpMax;
    }
}
