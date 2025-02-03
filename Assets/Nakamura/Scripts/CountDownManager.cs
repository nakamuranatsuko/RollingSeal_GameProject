using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class CountDownManager : MonoBehaviour
{
    [SerializeField, Label("�J�E���g�_�E���̐���")]
    private List<Sprite> countDownNumImages = new List<Sprite>();
    [SerializeField, Label("�J�E���g�_�E���̏ꏊ")]
    private Image countDownImage;
    [SerializeField, Label("�X�^�[�g�摜")]
    private GameObject startImage;
    [SerializeField, Label("�J�E���g�_�E���̃e���|")]
    private int waitTime = 1;
    [SerializeField, Label("�J�E���g�_�E���L�����o�X")]
    private GameObject countDownCanvas;
    [SerializeField, Label("�t�F�[�h�L�����o�X�p")]
    private CanvasGroup fadeCanvas;
    [SerializeField, Label("�����L�����o�X�p")]
    private GameObject explanationCanvas;
    //�J�E���g�_�E���t���O(���̂�)
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
    /// �J�E���g�_�E��
    /// </summary>
    private async void CountDown()
    {
        for(int i = 0;i < countDownNumImages.Count;i++)
        {
            //SE�Đ�
            SeManager.Instance.PlaySE(2);
            countDownImage.sprite = countDownNumImages[i];
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        }

        countDownImage.gameObject.SetActive(false);
        startImage.SetActive(true);
        //SE�Đ�
        SeManager.Instance.PlaySE(3);
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime));

        countDownCanvas.SetActive(false);
    }
}
