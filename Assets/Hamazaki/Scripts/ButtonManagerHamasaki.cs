using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagerHamasaki : MonoBehaviour
{
    public Button button1;                // �{�^��1
    public Button button2;                // �{�^��2
    public Button newButton;              // �V�����\������{�^��
    public GameObject objectToShow1;      // �{�^��1����������ɏo��������I�u�W�F�N�g
    public GameObject objectToShow2;      // �{�^��2����������ɏo��������I�u�W�F�N�g
    public string nextSceneName;          // �J�ڐ�̃V�[����

    public Vector3 targetPosition1;       // �I�u�W�F�N�g1�̈ړ���
    public Vector3 targetPosition2;       // �I�u�W�F�N�g2�̈ړ���
    public float moveSpeed = 1f;          // �ړ����x

    private bool button1Pressed = false;
    private bool button2Pressed = false;

    void Start()
    {
        // �����ݒ�F�I�u�W�F�N�g�ƐV�����{�^�����\���ɂ��Ă���
        objectToShow1.SetActive(false);
        objectToShow2.SetActive(false);
        newButton.gameObject.SetActive(false);

        // �{�^���̃��X�i�[��ݒ�
        button1.onClick.AddListener(OnButton1Clicked);
        button2.onClick.AddListener(OnButton2Clicked);
        newButton.onClick.AddListener(OnNewButtonClicked);
    }

    void OnButton1Clicked()
    {
        button1.gameObject.SetActive(false);   // �{�^��1���\��
        objectToShow1.SetActive(true);         // �I�u�W�F�N�g1��\��
        button1Pressed = true;
        CheckIfBothButtonsPressed();
    }

    void OnButton2Clicked()
    {
        button2.gameObject.SetActive(false);   // �{�^��2���\��
        objectToShow2.SetActive(true);         // �I�u�W�F�N�g2��\��
        button2Pressed = true;
        CheckIfBothButtonsPressed();
    }

    void CheckIfBothButtonsPressed()
    {
        if (button1Pressed && button2Pressed)
        {
            // �V�����{�^����\��
            newButton.gameObject.SetActive(true);
        }
    }

    void OnNewButtonClicked()
    {
        // �V�����{�^���������ꂽ�Ƃ��ɃV�[����J��
        SceneManager.LoadScene(nextSceneName);
    }

    void Update()
    {
        // �I�u�W�F�N�g1���\������Ă���ꍇ�A�ړ�������
        if (button1Pressed && objectToShow1.activeSelf)
        {
            objectToShow1.transform.position = Vector3.MoveTowards(
                objectToShow1.transform.position,
                targetPosition1,
                moveSpeed * Time.deltaTime);
        }

        // �I�u�W�F�N�g2���\������Ă���ꍇ�A�ړ�������
        if (button2Pressed && objectToShow2.activeSelf)
        {
            objectToShow2.transform.position = Vector3.MoveTowards(
                objectToShow2.transform.position,
                targetPosition2,
                moveSpeed * Time.deltaTime);
        }
    }
}
