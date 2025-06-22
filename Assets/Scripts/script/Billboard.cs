using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // ���C���J�������L���b�V�����Ă���
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // �������g�̌������J�����̌����ɍ��킹��
        transform.forward = -mainCamera.transform.forward;

        // �܂��́A��ɃJ�����̕������u�����v�悤�ɂ���ꍇ
        // transform.LookAt(mainCamera.transform);
        // transform.Rotate(0, 180, 0); // LookAt�̎d�l�Ŕ��]����ꍇ������̂Œ���
    }
}