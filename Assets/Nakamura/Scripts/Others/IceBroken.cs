using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBroken : MonoBehaviour
{
    [SerializeField]
    private GameObject iceObj;

    private bool isIce;

    private void Start()
    {
        isIce = false;
    }

    private void Update()
    {
        //�X����\���ɂȂ�����
        if(iceObj.activeSelf == false && isIce == false)
        {
            //�G�t�F�N�g�Đ�
            this.GetComponent<ParticleSystem>(). Play();
            isIce = true;
        }
        else
        {
            //�G�t�F�N�g��~
            this.GetComponent<ParticleSystem>().Stop();
        }
    }
}
