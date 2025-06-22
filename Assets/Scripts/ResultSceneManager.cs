using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager���g�p���邽�߂ɕK�v

public class ResultSceneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // ���ʂ�\������TextMeshProUGUI
    private void Start()
    {
        // GameDataManager�̃C���X�^���X�����݂��邩�m�F
        if (GameDataManager.Instance == null)
        {
            Debug.LogError("ResultSceneManager: GameDataManager�̃C���X�^���X��������܂���I");
            scoreText.text = "�G���[";
            return;
        }
        // �X�R�A��\��
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameDataManager.Instance.PlayerScore.ToString();
        }
        else
        {
            Debug.LogError("ResultSceneManager: scoreText���ݒ肳��Ă��܂���I");
        }
    }

    // �Q�[���X�^�[�g�{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void ReturnToTitle()
    {
        // "TitleScene"�Ƃ������O�̃V�[���ɑJ�ڂ���
        // ���ۂ̃Q�[���V�[�����ɍ��킹�ĕύX���Ă�������
        SceneManager.LoadScene("TitleScene");
    }
}