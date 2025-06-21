using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject titleScreenUI;
    public GameObject stageSelectUI;

    void Start()
    {
        if (titleScreenUI != null) titleScreenUI.SetActive(true);
        if (stageSelectUI != null) stageSelectUI.SetActive(false);

        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.ResetData();
        }
    }

    public void OnClickStartButton()
    {
        if (titleScreenUI != null) titleScreenUI.SetActive(false);
        if (stageSelectUI != null) stageSelectUI.SetActive(true);
        Debug.Log("TitleSceneManager: ステージ選択画面へ移行します。");
    }

    // SelectStage メソッドを修正してクリア条件を数値として受け取る
    // 例: "ステージ1,Stage1Scene,10"
    public void SelectStage(string stageInfo)
    {
        string[] parts = stageInfo.Split(',');
        if (parts.Length == 3)
        {
            string displayStageName = parts[0];
            string actualSceneName = parts[1];

            int clearValue;
            // ★★★文字列をint型に変換★★★
            if (int.TryParse(parts[2], out clearValue)) // 数値変換を試みる
            {
                if (GameDataManager.Instance != null)
                {
                    // GameDataManagerに選択されたステージ情報と数値型のクリア条件を設定
                    GameDataManager.Instance.SetSelectedStage(displayStageName, actualSceneName, clearValue);
                    Debug.Log($"TitleSceneManager: {displayStageName} と目標値 '{clearValue}' をGameDataManagerに設定しました。");
                }
                else
                {
                    Debug.LogError("TitleSceneManager: GameDataManagerのインスタンスが見つかりません！");
                }

                SceneManager.LoadScene("TransitionScene");
            }
            else
            {
                Debug.LogError($"TitleSceneManager: クリア条件の数値変換に失敗しました: {parts[2]}");
            }
        }
        else
        {
            Debug.LogError("TitleSceneManager: ステージ情報の形式が不正です。期待される形式: 'ステージ名,シーン名,目標数値'");
        }
    }

    public void OnClickBackButton()
    {
        if (stageSelectUI != null) stageSelectUI.SetActive(false);
        if (titleScreenUI != null) titleScreenUI.SetActive(true);
        Debug.Log("TitleSceneManager: タイトル画面へ戻ります。");
    }
}