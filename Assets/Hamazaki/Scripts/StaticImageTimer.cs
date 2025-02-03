using UnityEngine;
using UnityEngine.UI;

public class StaticImageTimer : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites; // 0����9�܂ł̃C���[�W
    [SerializeField] private Image[] minuteImages;   // ����\������Image�R���|�[�l���g�i2���p�j
    [SerializeField] private Image colonImage;       // �R�����i:�j��\������Image�R���|�[�l���g
    [SerializeField] private Image[] secondImages;   // �b��\������Image�R���|�[�l���g�i2���p�j

    //[SerializeField] private int minutes = 5;        // �������i��: 5���j
    //[SerializeField] private int seconds = 49;       // �����b�i��: 49�b�j

    void Start()
    {
        //�ύX����(����)
        // �������ƕb��\��
        UpdateNumberImages((int)TimerManager.Min, minuteImages);
        UpdateNumberImages((int)TimerManager.Sec, secondImages);
    }

    private void UpdateNumberImages(int number, Image[] digitImages)
    {
        // ���l�𕶎��񉻂��Č����Ƃɕ����i���2���ŕ\���j
        string numberStr = number.ToString("D2"); // "D2" ��2���\��������

        // �e���ɑΉ�����C���[�W���X�V
        for (int i = 0; i < digitImages.Length; i++)
        {
            int digit = int.Parse(numberStr[i].ToString());
            digitImages[i].sprite = numberSprites[digit];
            digitImages[i].enabled = true;
        }
    }
}
