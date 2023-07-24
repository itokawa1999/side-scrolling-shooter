using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 10.0f; // �v���C���[�̈ړ����x
    public float jumpForce = 1.0f; // �W�����v��
    public float maxJumpHeight = 5.0f; // �ő�W�����v����
    private bool isJumping = false; // �W�����v�����ǂ����𔻒f����ϐ�
    private Rigidbody2D rb;

    // �X�N���[�����W�����[���h���W�ɕϊ�������ʂ̒[
    private Vector2 min, max;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ��ʂ̒[�����[���h���W�ɕϊ�
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // �v���C���[�̈ʒu����ʓ��ɐ���
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min.x, max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, min.y, max.y);
        transform.position = clampedPosition;

        // �W�����v�̏���
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
