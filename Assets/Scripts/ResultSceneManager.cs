using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager���g�p���邽�߂ɕK�v

public class ResultSceneManager : MonoBehaviour
{
    // �Q�[���X�^�[�g�{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void ReturnToTitle()
    {
        // "TitleScene"�Ƃ������O�̃V�[���ɑJ�ڂ���
        // ���ۂ̃Q�[���V�[�����ɍ��킹�ĕύX���Ă�������
        SceneManager.LoadScene("TitleScene");
    }
}