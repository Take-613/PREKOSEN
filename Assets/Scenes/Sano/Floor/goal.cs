using UnityEngine;

public class goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ゴール！");
            // クリア処理（シーン遷移やUI表示など）を書く
            // 例：SceneManager.LoadScene("NextLevel");
        }
    }
}
