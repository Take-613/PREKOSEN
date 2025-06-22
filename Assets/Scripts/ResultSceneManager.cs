using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerを使用するために必要

public class ResultSceneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // 結果を表示するTextMeshProUGUI
    private void Start()
    {
        // GameDataManagerのインスタンスが存在するか確認
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("ResultSceneManager: GameDataManagerのインスタンスが見つかりません！");
            scoreText.text = "エラー";
            return;
        }
        // スコアを表示
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameDataManager.Instance.PlayerScore.ToString();
        }
        else
        {
            Debug.LogError("ResultSceneManager: scoreTextが設定されていません！");
        }
    }

    // ゲームスタートボタンが押されたときに呼び出されるメソッド
    public void ReturnToTitle()
    {
        // "TitleScene"という名前のシーンに遷移する
        // 実際のゲームシーン名に合わせて変更してください
        SceneManager.LoadScene("TitleScene");
    }
}