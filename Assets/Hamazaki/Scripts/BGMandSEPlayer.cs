using UnityEngine;

public class BGMandSEPlayer : MonoBehaviour
{
    void Start()
    {
        // BGM�̃C���f�b�N�X�ibgmList�̏��ԁj�A�{�����[���A���[�v�ݒ�
        BgmManager.Instance.PlayBGM(3, 0.15f, true); // BGM���X�g��0�Ԗڂ��Đ�

        SeManager.Instance.PlaySE(8, 0.15f, false);
    }
}
