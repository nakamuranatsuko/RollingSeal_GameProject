using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using NaughtyAttributes;

public class FollowParents: MonoBehaviour
{
    [SerializeField,Label("追従させたいオブジェクトの名前")]
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
            //名前が一致しているものを登録する
            souceTransform = GameObject.Find(souceTransformObjName).transform;
            //Constarintの有効無効を設定する
            SetObjectConstraintSource(this.souceTransform);
            // Constraintの参照元を追加
            myPositionConstraint.AddSource(this.myConstraintSource);
            // オフセットを0
            myPositionConstraint.translationOffset = Vector3.zero;
            // 有効にする
            myPositionConstraint.enabled = true;
            isFirst = true;
        }
    }

    /// <summary>
    /// ConstraintComponentの親オブジェクトを動的に追加する関数
    /// </summary>
    /// <param name="parent">Contraintの親オブジェクト</param>
    private void SetObjectConstraintSource(Transform parent)
    {
        //Constraintの参照元を設定
        this.myConstraintSource.sourceTransform = parent;
        //影響度を完全支配にする(Addの場合は0になる)
        this.myConstraintSource.weight = 1;
    }
}
