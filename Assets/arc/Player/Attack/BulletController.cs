using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // �I�u�W�F�N�g���J�����̎��삩��O�ꂽ���ɌĂяo�����
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
