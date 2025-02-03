using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonPress : MonoBehaviour
{
    public Button leftButton;  // �R���g���[���[1�p�{�^��
    public Button rightButton; // �R���g���[���[2�p�{�^��

    private bool controller1Ready = false;
    private bool controller2Ready = false;

    void Update()
    {
        // �R���g���[���[1��East�{�^���������ꂽ
        if (Input.GetButtonDown("Controller1_East"))
        {
            controller1Ready = true;
            Debug.Log("Player 1 is ready!");
            leftButton.interactable = false;  // �{�^���𖳌����i�����ꂽ���Ƃɂ���j
        }

        // �R���g���[���[2��East�{�^���������ꂽ
        if (Input.GetButtonDown("Controller2_East"))
        {
            controller2Ready = true;
            Debug.Log("Player 2 is ready!");
            rightButton.interactable = false;  // �{�^���𖳌����i�����ꂽ���Ƃɂ���j
        }

        // �����̃v���C���[���������������ꍇ
        if (controller1Ready && controller2Ready)
        {
            // �����̃v���C���[�����������̏ꍇ�A�Q�[���J�n�Ȃǂ̏������s��
            Debug.Log("Both players are ready!");
            // �Q�[���J�n�̏�����ǉ�����ꍇ�A�����ɃR�[�h��ǉ�
        }
    }
}
