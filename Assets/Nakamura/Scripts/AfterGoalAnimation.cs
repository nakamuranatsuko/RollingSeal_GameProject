using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AfterGoalAnimation : MonoBehaviour
{
    [SerializeField,Label("�o�[�`�����J����")]
    private GameObject virtualCameraObj;

    //�v���C���[��Rigidbody
    private GameObject player1;
    private GameObject player2;

    private bool isFirst;

    void Start()
    {
        //�T���ē����
        player1 = GameObject.Find("Player0");
        player2 = GameObject.Find("Player1");

        isFirst = false;
    }

    void Update()
    {
        //2�C���S�[��������
        if (GameManager.IsAftertGoal == true)
        {
            //�O�����ɐi��
            player1.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
            player2.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
        }

        //
        if(GameManager.IsResultGoal == true)
        {
            if(isFirst == false)
            {
                //�o�[�`�����J������false�ɂ���
                virtualCameraObj.SetActive(false);
                isFirst = true;
            }
            //�O�����ɐi��
            player1.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
            player2.GetComponent<Rigidbody>().AddForce(Vector3.forward * 150);
        }
    }
}
