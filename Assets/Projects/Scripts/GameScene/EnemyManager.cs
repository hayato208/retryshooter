using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の出現を制御するコンポーネント
public class EnemyManager : MonoBehaviour
{
    public Enemy[] m_enemyPrefabs; // 敵のプレハブを管理する配列
    public float m_interval; // 出現間隔（秒）
    private float m_timer; // 出現タイミングを管理するタイマー
    public float m_intervalFrom; // 出現間隔（秒）（ゲームの経過時間が 0 の時）
    public float m_intervalTo; // 出現間隔（秒）（ゲームの経過時間が m_elapsedTimeMax の時）
    public float m_elapsedTimeMax; // 経過時間の最大値
    public float m_elapsedTime; // 経過時間

    public int elapsedCount; // 出現カウント

    public float m_enemyInterval = 20f; // エネミーの出現短縮タイミング

    public int m_enemyLoopCount = 3; // 1回の出現で出現するエネミー数

    private void Start()
    {
        m_interval -= (float)(elapsedCount * 0.2);
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // エネミーの出現時間計測
        m_elapsedTime += Time.deltaTime;

        // 出現タイミングを管理するタイマーを更新する
        m_timer += Time.deltaTime;

        // エネミーの出現頻度を増加
        if (m_interval > 1 && m_elapsedTime >= m_enemyInterval)
        {
            m_interval -= 0.20f;
            m_elapsedTime = 0;
        }

        // まだ敵が出現するタイミングではない場合、
        // このフレームの処理はここで終える
        if (m_timer < m_interval) return;

        // 出現タイミングを管理するタイマーをリセットする
        m_timer = 0;

        // 出現数カウントをリセット
        m_enemyLoopCount=0;

        while (m_enemyLoopCount <= 3)
        {
            var enemyIndex = Random.Range(0, 3);

            // 出現する敵をランダムに決定する
            // 出現タイミング2秒を境に出現するエネミーが変化する
            if (m_interval <= 2)
            {
                enemyIndex = Random.Range(0, m_enemyPrefabs.Length);
            }

            // 出現する敵のプレハブを配列から取得する
            var enemyPrefab = m_enemyPrefabs[enemyIndex];

            // 敵のゲームオブジェクトを生成する
            var enemy = Instantiate(enemyPrefab);

            // 敵を画面外のどの位置に出現させるかランダムに決定する
            var respawnType = (RESPAWN_TYPE)Random.Range(
                0, (int)RESPAWN_TYPE.SIZEOF);

            // 敵を初期化する
            enemy.Init(respawnType);
            m_enemyLoopCount++;
        }
    }
}