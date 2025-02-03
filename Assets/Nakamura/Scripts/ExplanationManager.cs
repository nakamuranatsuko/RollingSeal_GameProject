using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExplanationManager : MonoBehaviour
{
    [SerializeField] private InputAction explanationAction = default;
    [SerializeField, Label("説明キャンバス用")]
    private GameObject explanationCanvas;
    [SerializeField, Label("説明画像1つ目")]
    private GameObject explanation1;
    [SerializeField, Label("説明2つ目")]
    private GameObject explanation2;

    //初回のみ用フラグ
    private bool isFirst;
    private int explanationNum;

    void Start()
    {
        explanationAction.Enable();
        explanationAction.performed += OnExplanationLoad;
        explanationCanvas.SetActive(true);
        explanation1.SetActive(true);
        explanation2.SetActive(false);
        explanationNum = 0;
    }

    private async void OnExplanationLoad(InputAction.CallbackContext context)
    {
        if (explanationNum == 0)
        {
            //切り替え
            explanation1.SetActive(false);
            explanation2.SetActive(true);
            //SE再生
            SeManager.Instance.PlaySE(1);
        }
        if (explanationNum == 1)
        {
            //SE再生
            SeManager.Instance.PlaySE(1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            //自身のキャンバスを非表示にする
            explanationCanvas.SetActive(false);
        }

        //押した分だけ数字が増える
        explanationNum++;
    }
}
