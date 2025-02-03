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
    [SerializeField, Label("�����L�����o�X�p")]
    private GameObject explanationCanvas;
    [SerializeField, Label("�����摜1��")]
    private GameObject explanation1;
    [SerializeField, Label("����2��")]
    private GameObject explanation2;

    //����̂ݗp�t���O
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
            //�؂�ւ�
            explanation1.SetActive(false);
            explanation2.SetActive(true);
            //SE�Đ�
            SeManager.Instance.PlaySE(1);
        }
        if (explanationNum == 1)
        {
            //SE�Đ�
            SeManager.Instance.PlaySE(1);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            //���g�̃L�����o�X���\���ɂ���
            explanationCanvas.SetActive(false);
        }

        //������������������������
        explanationNum++;
    }
}
