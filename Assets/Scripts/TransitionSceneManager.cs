using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionSceneManager : MonoBehaviour
{
    public TextMeshProUGUI stageNameText;           // �X�e�[�W����\������TextMeshProUGUI
    public TextMeshProUGUI clearConditionText;      // �N���A������\������TextMeshProUGUI
    public float displayDuration = 3.0f;            // ���G��\�����鎞��

    void Start()
    {
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("TransitionSceneManager: GameDataManager�̃C���X�^���X��������܂���I");
            stageNameText.text = "�G���[";
            if (clearConditionText != null) clearConditionText.text = "�f�[�^�Ȃ�";
            return;
        }

        // �X�e�[�W����\��
        if (stageNameText != null)
        {
            if (!string.IsNullOrEmpty(GameDataManager.Instance.SelectedStageName))
            {
                stageNameText.text = GameDataManager.Instance.SelectedStageName;
            }
            else
            {
                stageNameText.text = "�X�e�[�W���Ȃ�";
            }
        }

        // �������������C��������
        // ���l�^�̃N���A�������u�ڕW�l�F<���l>�v�̌`���ŕ\��
        if (clearConditionText != null)
        {
            // SelectedStageClearValue �� int �^�Ȃ̂ŁA���̂܂ܕ�����ɕϊ����ĕ\��
            clearConditionText.text = "Target Value: " + GameDataManager.Instance.SelectedStageClearValue.ToString();
        }
        // �����������܂ŏC��������

        // ���ۂ̃X�e�[�W�V�[���ւ̑J�ڂ��J�n
        if (!string.IsNullOrEmpty(GameDataManager.Instance.SelectedStageSceneName))
        {
            StartCoroutine(LoadStageAfterDelay(GameDataManager.Instance.SelectedStageSceneName, displayDuration));
        }
        else
        {
            Debug.LogError("TransitionSceneManager: �J�ڂ���X�e�[�W�V�[�������w�肳��Ă��܂���I");
        }
    }

    private IEnumerator LoadStageAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}