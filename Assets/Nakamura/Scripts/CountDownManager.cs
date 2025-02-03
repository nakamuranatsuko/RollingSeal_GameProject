using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class CountDownManager : MonoBehaviour
{
    [SerializeField, Label("カウントダウンの数字")]
    private List<Sprite> countDownNumImages = new List<Sprite>();
    [SerializeField, Label("カウントダウンの場所")]
    private Image countDownImage;
    [SerializeField, Label("スタート画像")]
    private GameObject startImage;
    [SerializeField, Label("カウントダウンのテンポ")]
    private int waitTime = 1;
    [SerializeField, Label("カウントダウンキャンバス")]
    private GameObject countDownCanvas;
    [SerializeField, Label("フェードキャンバス用")]
    private CanvasGroup fadeCanvas;
    [SerializeField, Label("説明キャンバス用")]
    private GameObject explanationCanvas;
    //カウントダウンフラグ(一回のみ)
    private bool isCountDown = false;

    private void Start()
    {
        countDownCanvas.SetActive(true);
        countDownImage.gameObject.SetActive(true);
        startImage.SetActive(false);
        isCountDown = false;
    }

    private void Update()
    {
        if (fadeCanvas.alpha != 0) return;
        if (explanationCanvas.activeSelf) return;

        if (isCountDown == false)
        {
            CountDown();
            isCountDown = true;
        }
    }

    /// <summary>
    /// カウントダウン
    /// </summary>
    private async void CountDown()
    {
        for(int i = 0;i < countDownNumImages.Count;i++)
        {
            //SE再生
            SeManager.Instance.PlaySE(2);
            countDownImage.sprite = countDownNumImages[i];
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        }

        countDownImage.gameObject.SetActive(false);
        startImage.SetActive(true);
        //SE再生
        SeManager.Instance.PlaySE(3);
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

        countDownCanvas.SetActive(false);
    }
}
