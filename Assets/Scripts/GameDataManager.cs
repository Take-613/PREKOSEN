using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public string SelectedStageName { get; private set; }
    public string SelectedStageSceneName { get; private set; }
    public int SelectedStageClearValue { get; private set; } // ������������int�^�ɕύX������

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("GameDataManager: Awake - DontDestroyOnLoad��ݒ肵�܂����B");
    }

    // SetSelectedStage ���\�b�h�̈������C��
    // clearCondition �̑���� clearValue ���󂯎��
    public void SetSelectedStage(string stageName, string sceneName, int clearValue) // ������������int�^�ɕύX������
    {
        SelectedStageName = stageName;
        SelectedStageSceneName = sceneName;
        SelectedStageClearValue = clearValue; // ���l�^�̃N���A������ݒ�
        Debug.Log($"GameDataManager: �X�e�[�W���I������܂���: {SelectedStageName} (�V�[��: {SelectedStageSceneName}), �ڕW�l: {SelectedStageClearValue}");
    }

    // �f�[�^���Z�b�g�������l�^�����Z�b�g
    public void ResetData()
    {
        SelectedStageName = null;
        SelectedStageSceneName = null;
        SelectedStageClearValue = 0; // ���l�^�Ȃ̂�0�Ń��Z�b�g
        Debug.Log("GameDataManager: �f�[�^�����Z�b�g����܂����B");
    }
}