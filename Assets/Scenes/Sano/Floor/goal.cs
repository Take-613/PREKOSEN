using UnityEngine;

public class goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�S�[���I");
            // �N���A�����i�V�[���J�ڂ�UI�\���Ȃǁj������
            // ��FSceneManager.LoadScene("NextLevel");
        }
    }
}
