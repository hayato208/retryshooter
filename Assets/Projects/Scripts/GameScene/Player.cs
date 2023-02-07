using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUIを使うのに必要
using UnityEngine.SceneManagement; // Sceneの切り替えに必要

// プレイヤーを制御するコンポーネント
public class Player : MonoBehaviour
{
    public Shot m_shotPrefab; // 弾のプレハブ

    // Playerステータス
    private float playerShotSpeed; // Playerの弾の速度
    private float playerShotInterval; // Playerの弾の発射間隔
    private float playerSpeed; // Playerの移動の速さ
    private int playerHpMax; // Playerの最大HP
    private int playerShotCount; // Playerの弾の発射数
    private float playerShotAngleRange; // Playerが複数の弾を発射する際の角度
    private int playerGold; // Playerの所持ゴールド
    private int playerHp; // 現在HP
    // Playerステータスここまで

    private float playerShotTimer; // 弾の発射タイミングを管理するタイマー

    public AudioClip m_damageClip; // ダメージを受けた時に再生する SE

    public static Player m_instance; // プレイヤーのインスタンスを管理する static 変数
    public Explosion m_explosionPrefab; // 爆発エフェクトのプレハブ
    [SerializeField]
    private TextMeshProUGUI PlayerGoldText; // ゴールド表示用
    [SerializeField]
    private TextMeshProUGUI PlayerHpMaxText; // HP表示用

    // ゲーム開始時に呼び出される関数
    private void Awake()
    {
        // 他のクラスからプレイヤーを参照できるように
        // static 変数にインスタンス情報を格納する
        m_instance = this;
    }

    void Start()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // StatusManagerデータ格納
        playerShotSpeed = sm.playerShotSpeed;
        playerShotInterval = sm.playerShotInterval;
        playerSpeed = sm.playerSpeed;
        playerHpMax = sm.playerHpMax;
        playerShotCount = sm.playerShotCount;
        playerShotAngleRange = sm.playerShotAngleRange;

        playerHp = sm.playerHpMax;
        playerGold = sm.playerGold;

        // HP,GOLD表示
        PlayerHpMaxText.text = "HP:" + playerHp;
        PlayerGoldText.text = "GOLD:" + playerGold;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        // ゲームを 60 FPS 固定にする
        Application.targetFrameRate = 60;

        // 矢印キーの入力情報を取得する
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // 矢印キーが押されている方向にプレイヤーを移動する
        var velocity = new Vector3(h, v) * sm.playerSpeed;
        transform.localPosition += velocity;

        // プレイヤーが画面外に出ないように位置を制限する
        transform.localPosition = Utils.ClampPosition(transform.localPosition);

        // プレイヤーのスクリーン座標を計算する
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから見たマウスカーソルの方向を計算する
        var direction = Input.mousePosition - screenPos;

        // マウスカーソルが存在する方向の角度を取得する
        var angle = Utils.GetAngle(Vector3.zero, direction);

        // プレイヤーがマウスカーソルの方向を見るようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 弾の発射タイミングを管理するタイマーを更新する
        playerShotTimer += Time.deltaTime;

        // まだ弾の発射タイミングではない場合は、ここで処理を終える
        if (playerShotTimer < playerShotInterval) return;

        // 弾の発射タイミングを管理するタイマーをリセットする
        playerShotTimer = 0;

        // 弾を発射する
        ShootNWay(angle, sm.playerShotAngleRange, sm.playerShotSpeed, playerShotCount);
    }

    // 弾を発射する関数
    private void ShootNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        // 弾を複数発射する場合
        if (1 < count)
        {
            // 発射する回数分ループする
            for (int i = 0; i < count; ++i)
            {
                // 弾の発射角度を計算する
                var angle = angleBase +
                    angleRange * ((float)i / (count - 1) - 0.5f);

                // 発射する弾を生成する
                var shot = Instantiate(m_shotPrefab, pos, rot);

                // 弾を発射する方向と速さを設定する
                shot.Init(angle, speed);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            var shot = Instantiate(m_shotPrefab, pos, rot);

            // 弾を発射する方向と速さを設定する
            shot.Init(angleBase, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 弾と衝突した場合
        if (collision.name.Contains("EnemyBullet"))
        {
            // ダメージを受けた時の SE を再生する
            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(m_damageClip);

            // 弾が当たった場所に爆発エフェクトを生成する
            Instantiate(
                m_explosionPrefab,
                collision.transform.localPosition,
                Quaternion.identity);

            // 弾を削除する
            Destroy(collision.gameObject);

            // 弾のダメージは1固定
            PlayerDamage(1);
        }
    }

    // ダメージを受ける関数
    // 敵とぶつかった時に呼び出される
    public void RamDamage(int damage)
    {
        // ダメージを受けた時の SE を再生する
        var audioSource = FindObjectOfType<AudioSource>();
        audioSource.PlayOneShot(m_damageClip);

        PlayerDamage(damage);
    }

    public void PlayerDamage(int damage)
    {
        // プレイヤーの HP を減らす
        playerHp -= damage;

        // HPを表示
        PlayerHpMaxText.text = "HP:" + playerHp;

        // HP がまだある場合、ここで処理を終える
        if (0 < playerHp) return;

        // プレイヤーが死亡したので非表示にする
        // 本来であれば、ここでゲームオーバー演出を再生したりする
        // gameObject.SetActive(false);

        if (0 == playerHp)
        {
            // ScoreManagerコンポーネント取得
            StatusScene();
        }
    }

    void StatusScene()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene("StatusScene");
    }

    public void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var sm = GameObject.Find("StatusManager").GetComponent<StatusManager>();

        sm.playerShotSpeed = playerShotSpeed;
        sm.playerShotInterval = playerShotInterval;
        sm.playerSpeed = playerSpeed;
        sm.playerHpMax = playerHpMax;
        sm.playerShotCount = playerShotCount;
        sm.playerGold = playerGold;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    public void AddGold(int gold)
    {
        // ゴールドを増やす
        playerGold += gold;

        // ゴールドを表示
        PlayerGoldText.text = "GOLD:" + playerGold;

        if (playerGold >= 10000)
        {
            // Type == Time の場合
            int scoreTime = TimeManager.minute * 60 + (int)TimeManager.seconds;

            // Type == Number の場合
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(scoreTime);

            // ゲーム停止
            Time.timeScale = 0f;
        }
    }

    public void LevelUp(int gold)
    {
        playerGold = playerGold - gold;

    }
}