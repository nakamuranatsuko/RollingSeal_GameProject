using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class ManpukuGaugeManager : MonoBehaviour
{
    private static ManpukuGaugeManager _instance;
    public static ManpukuGaugeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance
                    = GameObject.FindObjectOfType<ManpukuGaugeManager>();
            }
            return _instance;
        }
    }

    [SerializeField, Label("�����Q�[�W")]
    private Slider manpukuGauge;

    public bool FishGauge;
    public bool GoldFishGauge;
    public bool JumpGauge;
    public bool ManpukuGaugeMax;

    [SerializeField, Label("�����Q�[�W�摜")]
    private List<GameObject> gaugeImages = new List<GameObject>();
    [SerializeField, Label("�����Q�[�W�G�t�F�N�g")]
    private List<ParticleSystem> gaugeEff = new List<ParticleSystem>();

    private void Start()
    {
        manpukuGauge.value = manpukuGauge.minValue;
        FishGauge = false;
        GoldFishGauge = false;
        JumpGauge = false;
        ManpukuGaugeMax = false;

        for(int i = 0;i < 10;i++)
        {
            gaugeEff[i].Stop();
        }
    }

    private void Update()
    {
        //����H�ׂ���
        if(FishGauge == true)
        {
            //�\������
            gaugeImages[(int)manpukuGauge.value].SetActive(true);
            //�G�t�F�N�g�Đ�
            gaugeEff[(int)manpukuGauge.value].Play();
            //1���₷
            manpukuGauge.value += 1;
            if (manpukuGauge.value > manpukuGauge.maxValue) manpukuGauge.value = manpukuGauge.maxValue;

            FishGauge = false;
        }

        //���̋���H�ׂ���
        if (GoldFishGauge == true)
        {
            //�\������
            gaugeImages[(int)manpukuGauge.value].SetActive(true);
            //�G�t�F�N�g�Đ�
            gaugeEff[(int)manpukuGauge.value].Play();

            if ((int)manpukuGauge.value < 9)
            {
                gaugeImages[(int)manpukuGauge.value + 1].SetActive(true);
                //�G�t�F�N�g�Đ�
                gaugeEff[(int)manpukuGauge.value + 1].Play();

            }
            if ((int)manpukuGauge.value < 8)
            {
                gaugeImages[(int)manpukuGauge.value + 2].SetActive(true);
                //�G�t�F�N�g�Đ�
                gaugeEff[(int)manpukuGauge.value + 2].Play();
            }
            //3���₷
            manpukuGauge.value += 3;
            if (manpukuGauge.value > manpukuGauge.maxValue) manpukuGauge.value = manpukuGauge.maxValue;

            GoldFishGauge = false;
        }

        //�W�����v��������
        if (JumpGauge == true)
        {
            if((int)manpukuGauge.value != 0)
                //��\���ɂ���
                gaugeImages[(int)manpukuGauge.value-1].SetActive(false);
            //1���炷
            manpukuGauge.value -= 1;
            if (manpukuGauge.value < manpukuGauge.minValue) manpukuGauge.value = manpukuGauge.minValue;

            JumpGauge = false;
        }

        //�����Q�[�W���ő�܂ōs������
        if (manpukuGauge.value == manpukuGauge.maxValue)
        {
            //SE
            SeManager.Instance.PlaySE(12);
            ManpukuGaugeMax = true;
        }

        //�����Q�[�W���ő�ɂȂ�����
        if(ManpukuGaugeMax == true)
        {
            //�l�����X��0�ɖ߂�
            manpukuGauge.value -= Time.deltaTime;
            if (manpukuGauge.value < manpukuGauge.minValue) manpukuGauge.value = manpukuGauge.minValue;

            //�����Q�[�W��9�܂Ŗ߂��Ă�����
            if (manpukuGauge.value <= 9)
            {
                //��\���ɂ���
                gaugeImages[9].SetActive(false);
                manpukuGauge.value = 9;
                ManpukuGaugeMax = false;
            }
        }
    }
}
