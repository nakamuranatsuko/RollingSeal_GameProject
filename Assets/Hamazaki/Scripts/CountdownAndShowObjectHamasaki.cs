using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownAndShowObjectHamasaki : MonoBehaviour // �N���X�����t�@�C�����ƈ�v������
{
    public Button startButton;         // �J�E���g�_�E�����J�n����{�^��
    public Text countdownText;         // �J�E���g�_�E���\���p�̃e�L�X�gUI
    public GameObject objectToShow;    // �J�E���g�_�E����ɕ\������I�u�W�F�N�g
    public float countdownTime = 3f;   // �J�E���g�_�E������
    public float objectVisibleDuration = 1f; // �I�u�W�F�N�g��\�����鎞��

    void Start()
    {
        objectToShow.SetActive(false);   // ���߂ɃI�u�W�F�N�g���\���ɂ���
        countdownText.text = "";         // �J�E���g�_�E���\������ɂ���
        startButton.onClick.AddListener(StartCountdown); // �{�^���̃N���b�N�C�x���g��ݒ�
    }

    void StartCountdown()
    {
        startButton.gameObject.SetActive(false); // �{�^�����\���ɂ���
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        float currentTime = countdownTime;

        // �J�E���g�_�E������
        while (currentTime > 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // �J�E���g�_�E���̐���������\��
            yield return new WaitForSeconds(1f);  // 1�b�҂�
            currentTime--;
        }

        // 0�b�̎��_�ł͉����\�����Ȃ�
        countdownText.text = "";

        // �I�u�W�F�N�g��\������
        objectToShow.SetActive(true);

        // �w�肵�����Ԃ����I�u�W�F�N�g��\������
        yield return new WaitForSeconds(objectVisibleDuration);

        // �I�u�W�F�N�g���\���ɂ���
        objectToShow.SetActive(false);
    }
}
