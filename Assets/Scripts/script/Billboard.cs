using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // メインカメラをキャッシュしておく
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // 自分自身の向きをカメラの向きに合わせる
        transform.forward = -mainCamera.transform.forward;

        // または、常にカメラの方向を「向く」ようにする場合
        // transform.LookAt(mainCamera.transform);
        // transform.Rotate(0, 180, 0); // LookAtの仕様で反転する場合があるので調整
    }
}