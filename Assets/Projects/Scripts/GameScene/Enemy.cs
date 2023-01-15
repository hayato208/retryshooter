using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の出現位置の種類
public enum RESPAWN_TYPE
{
    UP, // 上
    RIGHT, // 右
    DOWN, // 下
    LEFT, // 左
    SIZEOF, // 敵の出現位置の数
}

// 敵を制御するコンポーネント
public class Enemy : MonoBehaviour
{
    public Vector2 m_respawnPosInside; // 敵の出現位置（内側）
    public Vector2 m_respawnPosOutside; // 敵の出現位置（外側）
    public float m_speed; // 移動する速さ
    public int m_hpMax; // HP の最大値
    public int m_gold; // この敵を倒した時に獲得できるゴールド
    public int m_damage; // この敵がプレイヤーに与えるダメージ
    public Explosion m_explosionPrefab; // 爆発エフェクトのプレハブ
    public bool m_isFollow; // プレイヤーを追尾する場合 true
    public bool m_isShot; // プレイヤーを攻撃する場合 true
    public EnemyBullet m_EnemyshotPrefab; // 弾のプレハブ
    public float m_shotSpeed; // 弾の移動の速さ
    public float m_shotAngleRange; // 複数の弾を発射する時の角度
    public float m_shotTimer; // 弾の発射タイミングを管理するタイマー
    public int m_shotCount; // 弾の発射数
    public float m_shotInterval; // 弾の発射間隔（秒）
    private int m_hp; // HP
    private Vector3 m_direction; // 進行方向

    // 敵が生成された時に呼び出される関数
    private void Start()
    {
        // HP を初期化する
        m_hp = m_hpMax;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        if (m_isShot)
        {
            // プレイヤーの現在位置へ向かうベクトルを作成する
            var angle = Utils.GetAngle(
                transform.localPosition,
                Player.m_instance.transform.localPosition);

            // プレイヤーが存在する方向を向く
            var angles = transform.localEulerAngles;
            angles.z = angle - 90;
            transform.localEulerAngles = angles;

            // 弾の発射タイミングを管理するタイマーを更新する
            m_shotTimer += Time.deltaTime;

            // 弾の発射
            if (m_shotTimer > m_shotInterval)
            {
                // 弾の発射タイミングを管理するタイマーをリセットする
                m_shotTimer = 0;

                // 弾を発射する
                ShootNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);
            }
        }

        // プレイヤーを追尾する場合
        if (m_isFollow)
        {
            // プレイヤーの現在位置へ向かうベクトルを作成する
            var angle = Utils.GetAngle(
                transform.localPosition,
                Player.m_instance.transform.localPosition);
            var direction = Utils.GetDirection(angle);

            // プレイヤーが存在する方向に移動する
            transform.localPosition += direction * m_speed;

            // プレイヤーが存在する方向を向く
            var angles = transform.localEulerAngles;
            angles.z = angle - 90;
            transform.localEulerAngles = angles;

            return;
        }
        // まっすぐ移動する
        transform.localPosition += m_direction * m_speed;
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
                var shot = Instantiate(m_EnemyshotPrefab, pos, rot);

                // 弾を発射する方向と速さを設定する
                shot.Init(angle, speed);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            var shot = Instantiate(m_EnemyshotPrefab, pos, rot);

            // 弾を発射する方向と速さを設定する
            shot.Init(angleBase, speed);
        }
    }

    // 敵が出現する時に初期化する関数
    public void Init(RESPAWN_TYPE respawnType)
    {
        var pos = Vector3.zero;

        // 指定された出現位置の種類に応じて、
        // 出現位置と進行方向を決定する
        switch (respawnType)
        {
            // 出現位置が上の場合
            case RESPAWN_TYPE.UP:
                pos.x = Random.Range(
                    -m_respawnPosInside.x, m_respawnPosInside.x);
                pos.y = m_respawnPosOutside.y;
                m_direction = Vector2.down;
                break;

            // 出現位置が右の場合
            case RESPAWN_TYPE.RIGHT:
                pos.x = m_respawnPosOutside.x;
                pos.y = Random.Range(
                    -m_respawnPosInside.y, m_respawnPosInside.y);
                m_direction = Vector2.left;
                break;

            // 出現位置が下の場合
            case RESPAWN_TYPE.DOWN:
                pos.x = Random.Range(
                    -m_respawnPosInside.x, m_respawnPosInside.x);
                pos.y = -m_respawnPosOutside.y;
                m_direction = Vector2.up;
                break;

            // 出現位置が左の場合
            case RESPAWN_TYPE.LEFT:
                pos.x = -m_respawnPosOutside.x;
                pos.y = Random.Range(
                    -m_respawnPosInside.y, m_respawnPosInside.y);
                m_direction = Vector2.right;
                break;
        }

        // 位置を反映する
        transform.localPosition = pos;
    }

    // 他のオブジェクトと衝突した時に呼び出される関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーと衝突した場合
        if (collision.name.Contains("Player"))
        {
            // プレイヤーにダメージを与える
            var player = collision.GetComponent<Player>();
            player.RamDamage(m_damage);

            // エネミーにダメージを与える
            m_hp--;

            // 敵の HP がまだ残っている場合はここで処理を終える
            if (0 < m_hp) return;

            // ゴールドを取得する
            player.AddGold(m_gold);

            // 敵を削除する
            Destroy(gameObject);
            return;
        }
        // 弾と衝突した場合
        if (collision.name.Contains("Shot"))
        {
            // 弾が当たった場所に爆発エフェクトを生成する
            Instantiate(
                m_explosionPrefab,
                collision.transform.localPosition,
                Quaternion.identity);

            // 弾を削除する
            Destroy(collision.gameObject);

            // 敵の HP を減らす
            m_hp--;

            // 敵の HP がまだ残っている場合はここで処理を終える
            if (0 < m_hp) return;

            // Playerコンポーネント取得
            var player = GameObject.Find("Player").GetComponent<Player>();

            // ゴールドを取得する
            player.AddGold(m_gold);

            // 敵を削除する
            Destroy(gameObject);
        }
    }
}