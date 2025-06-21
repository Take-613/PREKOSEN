using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionSceneManager : MonoBehaviour
{
    public TextMeshProUGUI stageNameText;           // ステージ名を表示するTextMeshProUGUI
    public TextMeshProUGUI clearConditionText;      // クリア条件を表示するTextMeshProUGUI
    public float displayDuration = 3.0f;            // 扉絵を表示する時間

    void Start()
    {
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("TransitionSceneManager: GameDataManagerのインスタンスが見つかりません！");
            stageNameText.text = "エラー";
            if (clearConditionText != null) clearConditionText.text = "データなし";
            return;
        }

        // ステージ名を表示
        if (stageNameText != null)
        {
            if (!string.IsNullOrEmpty(GameDataManager.Instance.SelectedStageName))
            {
                stageNameText.text = GameDataManager.Instance.SelectedStageName;
            }
            else
            {
                stageNameText.text = "ステージ情報なし";
            }
        }

        // ★★★ここを修正★★★
        // 数値型のクリア条件を「目標値：<数値>」の形式で表示
        if (clearConditionText != null)
        {
            // SelectedStageClearValue は int 型なので、そのまま文字列に変換して表示
            clearConditionText.text = "Target Value: " + GameDataManager.Instance.SelectedStageClearValue.ToString();
        }
        // ★★★ここまで修正★★★

        // 実際のステージシーンへの遷移を開始
        if (!string.IsNullOrEmpty(GameDataManager.Instance.SelectedStageSceneName))
        {
            StartCoroutine(LoadStageAfterDelay(GameDataManager.Instance.SelectedStageSceneName, displayDuration));
        }
        else
        {
            Debug.LogError("TransitionSceneManager: 遷移するステージシーン名が指定されていません！");
        }
    }

    private IEnumerator LoadStageAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}