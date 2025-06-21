using UnityEngine;

// オブジェクトを常にカメラの方向に向けるコンポーネント
public class LookAtCamera : MonoBehaviour
{
    private Camera targetCamera;
    
    void Start()
    {
        // メインカメラを自動取得
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        
        if (targetCamera == null)
        {
            targetCamera = FindObjectOfType<Camera>();
        }
    }
    
    void Update()
    {
        if (targetCamera != null)
        {
            // カメラの方向を向く
            Vector3 direction = targetCamera.transform.position - transform.position;
            direction.y = 0; // Y軸の回転は無視（水平方向のみ）
            
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
