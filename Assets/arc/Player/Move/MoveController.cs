using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 10.0f; // プレイヤーの移動速度
    public float jumpForce = 1.0f; // ジャンプ力
    public float maxJumpHeight = 5.0f; // 最大ジャンプ高さ
    private bool isJumping = false; // ジャンプ中かどうかを判断する変数
    private Rigidbody2D rb;

    // スクリーン座標をワールド座標に変換した画面の端
    private Vector2 min, max;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 画面の端をワールド座標に変換
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // プレイヤーの位置を画面内に制限
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min.x, max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, min.y, max.y);
        transform.position = clampedPosition;

        // ジャンプの処理
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
}
