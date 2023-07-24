using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // ���˃��[�h�i�P���A�A�ˁA�U�e�j
    public enum FireMode { Single, Auto, Scatter };
    public FireMode fireMode = FireMode.Single; // ���˃��[�h�̏����ݒ�

    public GameObject projectilePrefab; // ���˂���e�̃v���n�u
    public float projectileSpeed = 10f; // �e�̑��x
    public int scatterCount = 10; // �U�e�̔��ː�
    public float autoFireRate = 0.2f; // �A�˃��[�h�̔��ˊԊu�i�b�j
    private Vector2 direction; // �e�̔��˕���
    private float nextFireTime = 0; // ���̔��ˉ\����

    // �t���[�����Ƃ̍X�V����
    void Update()
    {
        // �}�E�X�J�[�\���̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �}�E�X�J�[�\���̕������擾
        direction = (Vector2)(mousePosition - transform.position);
        direction.Normalize();

        // ���˃��[�h���X�N���[���ŕύX
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            fireMode = (FireMode)(((int)fireMode + (int)scroll + 3) % 3);
            Debug.Log("���˃��[�h: " + fireMode);
        }

        // ���˃��[�h�ɉ����Ēe�𔭎�
        switch (fireMode)
        {
            case FireMode.Single: // �P�����[�h
                if (Input.GetButtonDown("Fire1"))
                {
                    FireSingle();
                }
                break;

            case FireMode.Auto: // �A�˃��[�h
                if (Input.GetButton("Fire1") && Time.time > nextFireTime)
                {
                    FireAuto();
                }
                break;

            case FireMode.Scatter: // �U�e���[�h
                if (Input.GetButtonDown("Fire1"))
                {
                    FireScatter();
                }
                break;
        }
    }

    // �P�����[�h�̔��ˏ���
    private void FireSingle()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    // �A�˃��[�h�̔��ˏ���
    private void FireAuto()
    {
        nextFireTime = Time.time + autoFireRate;
        FireSingle();
    }

    private void FireScatter()
    {
        float initialAngle = -20; // �����̊p�x�i�x�j
        float increment = 40f / (scatterCount - 1); // �e�e���Ƃ̑����p�x�i�x�j

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
