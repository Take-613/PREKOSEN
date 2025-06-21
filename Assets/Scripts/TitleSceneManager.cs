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
        Debug.Log("TitleSceneManager: �X�e�[�W�I����ʂֈڍs���܂��B");
    }

    // SelectStage ���\�b�h���C�����ăN���A�����𐔒l�Ƃ��Ď󂯎��
    // ��: "�X�e�[�W1,Stage1Scene,10"
    public void SelectStage(string stageInfo)
    {
        string[] parts = stageInfo.Split(',');
        if (parts.Length == 3)
        {
            string displayStageName = parts[0];
            string actualSceneName = parts[1];

            int clearValue;
            // �������������int�^�ɕϊ�������
            if (int.TryParse(parts[2], out clearValue)) // ���l�ϊ������݂�
            {
                if (GameDataManager.Instance != null)
                {
                    // GameDataManager�ɑI�����ꂽ�X�e�[�W���Ɛ��l�^�̃N���A������ݒ�
                    GameDataManager.Instance.SetSelectedStage(displayStageName, actualSceneName, clearValue);
                    Debug.Log($"TitleSceneManager: {displayStageName} �ƖڕW�l '{clearValue}' ��GameDataManager�ɐݒ肵�܂����B");
                }
                else
                {
                    Debug.LogError("TitleSceneManager: GameDataManager�̃C���X�^���X��������܂���I");
                }

                SceneManager.LoadScene("TransitionScene");
            }
            else
            {
                Debug.LogError($"TitleSceneManager: �N���A�����̐��l�ϊ��Ɏ��s���܂���: {parts[2]}");
            }
        }
        else
        {
            Debug.LogError("TitleSceneManager: �X�e�[�W���̌`�����s���ł��B���҂����`��: '�X�e�[�W��,�V�[����,�ڕW���l'");
        }
    }

    public void OnClickBackButton()
    {
        if (stageSelectUI != null) stageSelectUI.SetActive(false);
        if (titleScreenUI != null) titleScreenUI.SetActive(true);
        Debug.Log("TitleSceneManager: �^�C�g����ʂ֖߂�܂��B");
    }
}