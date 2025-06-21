using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public string SelectedStageName { get; private set; }
    public string SelectedStageSceneName { get; private set; }
    public int SelectedStageClearValue { get; private set; } // ★★★ここをint型に変更★★★

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("GameDataManager: Awake - DontDestroyOnLoadを設定しました。");
    }

    // SetSelectedStage メソッドの引数を修正
    // clearCondition の代わりに clearValue を受け取る
    public void SetSelectedStage(string stageName, string sceneName, int clearValue) // ★★★引数をint型に変更★★★
    {
        SelectedStageName = stageName;
        SelectedStageSceneName = sceneName;
        SelectedStageClearValue = clearValue; // 数値型のクリア条件を設定
        Debug.Log($"GameDataManager: ステージが選択されました: {SelectedStageName} (シーン: {SelectedStageSceneName}), 目標値: {SelectedStageClearValue}");
    }

    // データリセット時も数値型をリセット
    public void ResetData()
    {
        SelectedStageName = null;
        SelectedStageSceneName = null;
        SelectedStageClearValue = 0; // 数値型なので0でリセット
        Debug.Log("GameDataManager: データがリセットされました。");
    }
}