using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerを使用するために必要

public class ResultSceneManager : MonoBehaviour
{
    // ゲームスタートボタンが押されたときに呼び出されるメソッド
    public void ReturnToTitle()
    {
        // "TitleScene"という名前のシーンに遷移する
        // 実際のゲームシーン名に合わせて変更してください
        SceneManager.LoadScene("TitleScene");
    }
}