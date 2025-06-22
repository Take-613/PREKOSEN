using UnityEngine;

public class DestroyWithPar : MonoBehaviour
{
    // OnTriggerEnter2D �́AIs Trigger ���I���� Collider2D �Ƒ��� Collider2D ���ڐG�����Ƃ��ɌĂяo�����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �Փ˂������肪�uPlayer�v�^�O�����I�u�W�F�N�g���m�F
        // �v���C���[�I�u�W�F�N�g�ɁuPlayer�v�^�O���t���Ă��邱�Ƃ��m�F���Ă�������
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�ƏՓ�");

            // �e�I�u�W�F�N�g�i3D�A�C�e���{�́j���Ɣj�󂷂�
            // transform.parent �͐e��Transform�R���|�[�l���g��Ԃ�
            // �e�����[�g�I�u�W�F�N�g�̏ꍇ�Atransform.parent �� null �ɂȂ�̂Œ���
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject); // �eGameObject��j��
            }
            else
            {
                // �e�����Ȃ��ꍇ�́A����2D�Փ˔���I�u�W�F�N�g���g��j��
                // �i�������A���̃P�[�X�ł͒ʏ�e������͂��Ȃ̂Ńf�o�b�O�p�j
                Destroy(gameObject);
            }
        }
    }
}