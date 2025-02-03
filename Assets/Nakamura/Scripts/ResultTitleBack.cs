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
    [SerializeField, Scene, Label("�J�ڐ�V�[��")]
    private string sceneName;

    //����̂ݗp�t���O
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
            //SE�Đ�
            SeManager.Instance.PlaySE(1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            await FadeManager.Inctance.FadeOut();
            //�J��
            SceneManager.LoadScene(sceneName);
        }
    }
}
