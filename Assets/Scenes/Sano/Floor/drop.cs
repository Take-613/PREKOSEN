using UnityEngine;
using UnityEngine.SceneManagement;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーが落下ゾーンに触れた");
            // ゲームオーバー処理や再スタート処理
            SceneManager.LoadScene("ResultScene");
        }
    }
}
