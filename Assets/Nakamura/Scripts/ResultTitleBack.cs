using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultTitleBack : MonoBehaviour
{
    [SerializeField] private InputAction StartLoadSceneAction = default;
    [SerializeField, Scene, Label("遷移先シーン")]
    private string sceneName;

    //初回のみ用フラグ
    private bool isFirst;

    void Start()
    {
        StartLoadSceneAction.Enable();
        StartLoadSceneAction.performed += OnStartLoadScene;
        isFirst = false;
    }

    private async void OnStartLoadScene(InputAction.CallbackContext context)
    {
        if(isFirst == false)
        {
            isFirst = true;
            //SE再生
            SeManager.Instance.PlaySE(1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            await FadeManager.Inctance.FadeOut();
            //遷移
            SceneManager.LoadScene(sceneName);
        }
    }
}
