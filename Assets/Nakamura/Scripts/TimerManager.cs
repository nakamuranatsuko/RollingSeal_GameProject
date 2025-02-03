using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField, Label("タイマーの数字")]
    private List<Sprite> timerNumImages = new List<Sprite>();
    [SerializeField, Foldout("タイマー"), Label("分の左")]
    private Image minute_L;
    [SerializeField, Foldout("タイマー"), Label("分の右")]
    private Image minute_R;
    [SerializeField, Foldout("タイマー"), Label("秒の左")]
    private Image seconds_L;
    [SerializeField, Foldout("タイマー"), Label("秒の右")]
    private Image seconds_R;
    [SerializeField, Label("カウントダウンキャンバス")]
    private GameObject countDownCanvas;

    //分、秒
    public static float Min, Sec;

    private void Start()
    {
        Sec = 0;
        Min = 0;
    }

    void Update()
    {
        if (countDownCanvas.activeSelf) return;

        //タイマースタート
        Sec += Time.deltaTime;

        //秒が60秒より上いったら
        if (Sec > 60)
        {
            //秒を0にする
            Sec = 0;
            //分を1プラスする
            Min++;
        }
        //分が60を越したら
        if (Min >= 60)
        {
            //分を0にする
            Min = 0;
        }

        //タイマーをUIに反映
        minute_L.sprite = timerNumImages[GetNumDigit((int)Min, 2)];
        minute_R.sprite = timerNumImages[GetNumDigit((int)Min, 1)];
        seconds_L.sprite = timerNumImages[GetNumDigit((int)Sec, 2)];
        seconds_R.sprite = timerNumImages[GetNumDigit((int)Sec, 1)];
    }

    /// <summary>
    /// 桁指定する関数
    /// </summary>
    /// <param name="num">指定対象の数字</param>
    /// <param name="digit">指定する桁(1から)</param>
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
