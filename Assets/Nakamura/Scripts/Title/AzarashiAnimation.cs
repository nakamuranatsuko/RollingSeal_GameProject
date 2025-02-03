using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class AzarashiShiroAnimation : MonoBehaviour
{
    [SerializeField,Label("�ҋ@����")]
    private float waitTime = 1f;
    [SerializeField, Label("�ړ�����")]
    private float moveTime = 1f;
    [SerializeField, Label("�S�̓I�ɒx�点�鎞��")]
    private float delayTime = 2f;

    [SerializeField, Dropdown("ease"),Label("�C�[�W���O")]
    private Ease easing;
    private List<Ease> ease
    {
        get
        {
            return new List<Ease>() {
            Ease.InOutBack,
            Ease.InBounce,
            Ease.OutBounce
        };
        }
    }

    [SerializeField, Label("�ړ����X���W")]
    private float moveX;
    [SerializeField, Label("�ړ����Y���W")]
    private float moveY;

    void Start()
    {
        //���݈ʒu��ێ�
        Vector3 currentPos = this.transform.position;

        var sequence = DOTween.Sequence();
        //�ړ���
        var move = transform.DOMove(new Vector3(moveX, moveY, currentPos.z), moveTime).SetEase(easing);
        //���̈ʒu
        var turnBack = transform.DOMove(currentPos, moveTime).SetEase(easing);

        //�ړ�
        sequence.Append(move);
        // �������I����ҋ@
        sequence.AppendInterval(waitTime);
        //���̈ʒu�ɖ߂�
        sequence.Append(turnBack);
        // �������I����ҋ@
        sequence.AppendInterval(waitTime);

        //���[�v
        sequence.SetLoops(-1, LoopType.Yoyo);
        //�S�̓I�ɒx�点��
        sequence.SetDelay(delayTime);
    }
}
