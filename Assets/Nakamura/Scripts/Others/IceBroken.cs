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
        //氷が非表示になったら
        if(iceObj.activeSelf == false && isIce == false)
        {
            //エフェクト再生
            this.GetComponent<ParticleSystem>(). Play();
            isIce = true;
        }
        else
        {
            //エフェクト停止
            this.GetComponent<ParticleSystem>().Stop();
        }
    }
}
