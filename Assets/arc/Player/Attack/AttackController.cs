using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // 発射モード（単発、連射、散弾）
    public enum FireMode { Single, Auto, Scatter };
    public FireMode fireMode = FireMode.Single; // 発射モードの初期設定

    public GameObject projectilePrefab; // 発射する弾のプレハブ
    public float projectileSpeed = 10f; // 弾の速度
    public int scatterCount = 10; // 散弾の発射数
    public float autoFireRate = 0.2f; // 連射モードの発射間隔（秒）
    private Vector2 direction; // 弾の発射方向
    private float nextFireTime = 0; // 次の発射可能時間

    // フレームごとの更新処理
    void Update()
    {
        // マウスカーソルの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウスカーソルの方向を取得
        direction = (Vector2)(mousePosition - transform.position);
        direction.Normalize();

        // 発射モードをスクロールで変更
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            fireMode = (FireMode)(((int)fireMode + (int)scroll + 3) % 3);
            Debug.Log("発射モード: " + fireMode);
        }

        // 発射モードに応じて弾を発射
        switch (fireMode)
        {
            case FireMode.Single: // 単発モード
                if (Input.GetButtonDown("Fire1"))
                {
                    FireSingle();
                }
                break;

            case FireMode.Auto: // 連射モード
                if (Input.GetButton("Fire1") && Time.time > nextFireTime)
                {
                    FireAuto();
                }
                break;

            case FireMode.Scatter: // 散弾モード
                if (Input.GetButtonDown("Fire1"))
                {
                    FireScatter();
                }
                break;
        }
    }

    // 単発モードの発射処理
    private void FireSingle()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    // 連射モードの発射処理
    private void FireAuto()
    {
        nextFireTime = Time.time + autoFireRate;
        FireSingle();
    }

    private void FireScatter()
    {
        float initialAngle = -20; // 初期の角度（度）
        float increment = 40f / (scatterCount - 1); // 各弾ごとの増分角度（度）

        for (int i = 0; i < scatterCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            float scatterAngle = initialAngle + increment * i;
            Vector2 scatterDirection = Quaternion.Euler(0, 0, scatterAngle) * direction;
            rb.velocity = scatterDirection * projectileSpeed;
        }
    }
}
