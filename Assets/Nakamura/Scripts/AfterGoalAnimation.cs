using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AfterGoalAnimation : MonoBehaviour
{
    [SerializeField,Label("バーチャルカメラ")]
    private GameObject virtualCameraObj;

    //プレイヤーのRigidbody
    private GameObject player1;
    private GameObject player2;

    private bool isFirst;

    void Start()
    {
        //探して入れる
        player1 = GameObject.Find("Player0");
        player2 = GameObject.Find("Player1");

        isFirst = false;
    }

    void Update()
    {
        //2匹がゴールしたら
        if (GameManager.IsAftertGoal == true)
        {
            //前方向に進む
            player1.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
            player2.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
        }

        //
        if(GameManager.IsResultGoal == true)
        {
            if(isFirst == false)
            {
                //バーチャルカメラをfalseにする
                virtualCameraObj.SetActive(false);
                isFirst = true;
            }
            //前方向に進む
            player1.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
            player2.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
        }
    }
}
