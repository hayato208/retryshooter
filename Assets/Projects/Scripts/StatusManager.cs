using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要

public class StatusManager : MonoBehaviour
{
    public int playerGold; // Playerの所持ゴールド

    //Playerステータス
    public float playerShotSpeed; // Playerの弾の速度
    public float playerShotInterval; // Playerの弾の発射間隔
    public float playerSpeed; // Playerの移動の速さ
    public int playerHpMax; // Playerの最大HP
    public int playerShotCount; // Playerの弾の発射数
    public float playerShotAngleRange; // Playerが複数の弾を発射する際の角度

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
}
