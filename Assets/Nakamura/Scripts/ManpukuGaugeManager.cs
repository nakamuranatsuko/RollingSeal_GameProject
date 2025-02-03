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

    [SerializeField, Label("満腹ゲージ")]
    private Slider manpukuGauge;

    public bool FishGauge;
    public bool GoldFishGauge;
    public bool JumpGauge;
    public bool ManpukuGaugeMax;

    [SerializeField, Label("満腹ゲージ画像")]
    private List<GameObject> gaugeImages = new List<GameObject>();
    [SerializeField, Label("満腹ゲージエフェクト")]
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
        //魚を食べたら
        if(FishGauge == true)
        {
            //表示する
            gaugeImages[(int)manpukuGauge.value].SetActive(true);
            //エフェクト再生
            gaugeEff[(int)manpukuGauge.value].Play();
            //1増やす
            manpukuGauge.value += 1;
            if (manpukuGauge.value > manpukuGauge.maxValue) manpukuGauge.value = manpukuGauge.maxValue;

            FishGauge = false;
        }

        //金の魚を食べたら
        if (GoldFishGauge == true)
        {
            //表示する
            gaugeImages[(int)manpukuGauge.value].SetActive(true);
            //エフェクト再生
            gaugeEff[(int)manpukuGauge.value].Play();

            if ((int)manpukuGauge.value < 9)
            {
                gaugeImages[(int)manpukuGauge.value + 1].SetActive(true);
                //エフェクト再生
                gaugeEff[(int)manpukuGauge.value + 1].Play();

            }
            if ((int)manpukuGauge.value < 8)
            {
                gaugeImages[(int)manpukuGauge.value + 2].SetActive(true);
                //エフェクト再生
                gaugeEff[(int)manpukuGauge.value + 2].Play();
            }
            //3増やす
            manpukuGauge.value += 3;
            if (manpukuGauge.value > manpukuGauge.maxValue) manpukuGauge.value = manpukuGauge.maxValue;

            GoldFishGauge = false;
        }

        //ジャンプをしたら
        if (JumpGauge == true)
        {
            if((int)manpukuGauge.value != 0)
                //非表示にする
                gaugeImages[(int)manpukuGauge.value-1].SetActive(false);
            //1減らす
            manpukuGauge.value -= 1;
            if (manpukuGauge.value < manpukuGauge.minValue) manpukuGauge.value = manpukuGauge.minValue;

            JumpGauge = false;
        }

        //満腹ゲージが最大まで行ったら
        if (manpukuGauge.value == manpukuGauge.maxValue)
        {
            //SE
            SeManager.Instance.PlaySE(12);
            ManpukuGaugeMax = true;
        }

        //満腹ゲージが最大になったら
        if(ManpukuGaugeMax == true)
        {
            //値を徐々に0に戻す
            manpukuGauge.value -= Time.deltaTime;
            if (manpukuGauge.value < manpukuGauge.minValue) manpukuGauge.value = manpukuGauge.minValue;

            //満腹ゲージが9まで戻ってきたら
            if (manpukuGauge.value <= 9)
            {
                //非表示にする
                gaugeImages[9].SetActive(false);
                manpukuGauge.value = 9;
                ManpukuGaugeMax = false;
            }
        }
    }
}
