using TMPro;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public TextMeshProUGUI rankText;

    void Start()
    {
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("ResultSceneManager: GameDataManagerのインスタンスが見つかりません！");
            rankText.text = "エラー";
            return;
        }

        if (GameDataManager.Instance.goalflag == false)
        {
            rankText.text = "D";
            return;
        }

        // Rankを表示
        switch (Mathf.Abs(GameDataManager.Instance.PlayerScore - GameDataManager.Instance.SelectedStageClearValue))
        {
            case 2:
                rankText.text = "S";
                break;
            case 15:
                rankText.text = "A";
                break;
            case 50:
                rankText.text = "B";
                break;
            default:
                rankText.text = "C";
                break;
        }
    }
}
