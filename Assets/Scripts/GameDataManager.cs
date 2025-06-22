using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public string SelectedStageName { get; private set; }
    public string SelectedStageSceneName { get; private set; }
    public int SelectedStageClearValue { get; private set; } // ������������int�^�ɕύX������
    public int PlayerScore { get; private set; } // �v���C���[�̃X�R�A��ǉ�
    
    public bool goalflag { get; private set; } //�S�[���������ǂ���

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

    public void SetPlayerScore(int score)
    {
        PlayerScore = score;
        Debug.Log($"GameDataManager: �v���C���[�̃X�R�A���ݒ肳��܂���: {PlayerScore}");
    }

    // �f�[�^���Z�b�g�������l�^�����Z�b�g
    public void ResetData()
    {
        SelectedStageName = null;
        SelectedStageSceneName = null;
        SelectedStageClearValue = 0; // ���l�^�Ȃ̂�0�Ń��Z�b�g
        Debug.Log("GameDataManager: �f�[�^�����Z�b�g����܂����B");
    }

    public void SetGoalFlag(bool flag)
    {
        goalflag = flag;
    }
    
}