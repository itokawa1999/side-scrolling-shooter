using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // オブジェクトがカメラの視野から外れた時に呼び出される
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
