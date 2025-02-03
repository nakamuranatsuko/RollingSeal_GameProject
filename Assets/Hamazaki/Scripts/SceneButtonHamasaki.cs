using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    // �C���X�y�N�^�[����V�[�������w��ł���
    public string sceneName = "TitleSceneHamasakiKari"; // �f�t�H���g�� TitleSceneHamasakiKari �ɐݒ�

    // �{�^���������ꂽ�Ƃ��ɌĂяo�����֐�
    public void OnButtonPress()
    {
        // �V�[�������w�肳��Ă���ꍇ�A���̃V�[���֑J��
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("�V�[�������w�肳��Ă��܂���I");
        }
    }
}
