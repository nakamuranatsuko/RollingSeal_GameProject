using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField, Label("�^�C�}�[�̐���")]
    private List<Sprite> timerNumImages = new List<Sprite>();
    [SerializeField, Foldout("�^�C�}�["), Label("���̍�")]
    private Image minute_L;
    [SerializeField, Foldout("�^�C�}�["), Label("���̉E")]
    private Image minute_R;
    [SerializeField, Foldout("�^�C�}�["), Label("�b�̍�")]
    private Image seconds_L;
    [SerializeField, Foldout("�^�C�}�["), Label("�b�̉E")]
    private Image seconds_R;
    [SerializeField, Label("�J�E���g�_�E���L�����o�X")]
    private GameObject countDownCanvas;

    //���A�b
    public static float Min, Sec;

    private void Start()
    {
        Sec = 0;
        Min = 0;
    }

    void Update()
    {
        if (countDownCanvas.activeSelf) return;

        //�^�C�}�[�X�^�[�g
        Sec += Time.deltaTime;

        //�b��60�b���ア������
        if (Sec > 60)
        {
            //�b��0�ɂ���
            Sec = 0;
            //����1�v���X����
            Min++;
        }
        //����60���z������
        if (Min >= 60)
        {
            //����0�ɂ���
            Min = 0;
        }

        //�^�C�}�[��UI�ɔ��f
        minute_L.sprite = timerNumImages[GetNumDigit((int)Min, 2)];
        minute_R.sprite = timerNumImages[GetNumDigit((int)Min, 1)];
        seconds_L.sprite = timerNumImages[GetNumDigit((int)Sec, 2)];
        seconds_R.sprite = timerNumImages[GetNumDigit((int)Sec, 1)];
    }

    /// <summary>
    /// ���w�肷��֐�
    /// </summary>
    /// <param name="num">�w��Ώۂ̐���</param>
    /// <param name="digit">�w�肷�錅(1����)</param>
    /// <returns></returns>
    private int GetNumDigit(int num, int digit)
    {
        int res = 0;
        int pow_dig = (int)Mathf.Pow(10, digit);
        if (digit == 1)
            res = num - (num / pow_dig) * pow_dig;
        else
            res = (num - (num / pow_dig) * pow_dig) / (int)Mathf.Pow(10, (digit - 1));

        return res;
    }
}
