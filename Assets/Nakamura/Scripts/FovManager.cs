using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using NaughtyAttributes;
using Cinemachine;

public class FovManager : MonoBehaviour
{
    //private float fogNum = 10000;
    //private float fogEndNum = 0;
    private bool isFog;
    [SerializeField, Label("バーチャルカメラ")]
    private CinemachineVirtualCamera vCam;
    [SerializeField, Label("カメラ")]
    private Camera cam;
    private float fov = 60;
    private float minFov = 45;

    void Start()
    {
        //RenderSettings.fogStartDistance = 3;
        //RenderSettings.fogEndDistance = fogNum;
        //isFog = false;
        vCam.m_Lens.FieldOfView = fov;
    }

    async void Update()
    {
        if(GameManager.IsAftertGoal == true && isFog == false)
        {
            isFog = true;
        }

        if(isFog == true)
        {
            await Fog();
        }
    }

    public async UniTask Fog()
    {
        //while (fogNum > fogEndNum)
        //{
        //    fogNum -= Time.deltaTime * 30;
        //    if (fogNum <= fogEndNum) fogNum = fogEndNum;
        //    RenderSettings.fogEndDistance = fogNum + 1;
        //    await UniTask.Yield();
        //}

        //if (fogNum <= fogEndNum)
        //{
        //    fogNum = fogEndNum;
        //    RenderSettings.fogEndDistance = fogNum + 1;
        //}

        //バーチャルカメラ
        while (fov > minFov)
        {
            fov -= Time.deltaTime * 0.08f;
            if (fov <= minFov) fov = minFov;
            vCam.m_Lens.FieldOfView = fov;
            cam.fieldOfView = fov;
            await UniTask.Yield();
        }

        if (fov <= minFov)
        {
            vCam.m_Lens.FieldOfView = minFov;
            cam.fieldOfView = minFov;
        }
    }
}
