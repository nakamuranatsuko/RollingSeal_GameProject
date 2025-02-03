using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using NaughtyAttributes;

public class FollowParents: MonoBehaviour
{
    [SerializeField,Label("�Ǐ]���������I�u�W�F�N�g�̖��O")]
    private string souceTransformObjName;
    private Transform souceTransform;
    private ConstraintSource myConstraintSource;
    private PositionConstraint myPositionConstraint;
    private bool isFirst;

    private void Start()
    {
        isFirst = false;
        myPositionConstraint = GetComponent<PositionConstraint>();
    }

    private void Update()
    {
        if(isFirst == false)
        {
            //���O����v���Ă�����̂�o�^����
            souceTransform = GameObject.Find(souceTransformObjName).transform;
            //Constarint�̗L��������ݒ肷��
            SetObjectConstraintSource(this.souceTransform);
            // Constraint�̎Q�ƌ���ǉ�
            myPositionConstraint.AddSource(this.myConstraintSource);
            // �I�t�Z�b�g��0
            myPositionConstraint.translationOffset = Vector3.zero;
            // �L���ɂ���
            myPositionConstraint.enabled = true;
            isFirst = true;
        }
    }

    /// <summary>
    /// ConstraintComponent�̐e�I�u�W�F�N�g�𓮓I�ɒǉ�����֐�
    /// </summary>
    /// <param name="parent">Contraint�̐e�I�u�W�F�N�g</param>
    private void SetObjectConstraintSource(Transform parent)
    {
        //Constraint�̎Q�ƌ���ݒ�
        this.myConstraintSource.sourceTransform = parent;
        //�e���x�����S�x�z�ɂ���(Add�̏ꍇ��0�ɂȂ�)
        this.myConstraintSource.weight = 1;
    }
}
