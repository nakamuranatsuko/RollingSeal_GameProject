using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class AzarashiShiroAnimation : MonoBehaviour
{
    [SerializeField,Label("待機時間")]
    private float waitTime = 1f;
    [SerializeField, Label("移動時間")]
    private float moveTime = 1f;
    [SerializeField, Label("全体的に遅らせる時間")]
    private float delayTime = 2f;

    [SerializeField, Dropdown("ease"),Label("イージング")]
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

    [SerializeField, Label("移動先のX座標")]
    private float moveX;
    [SerializeField, Label("移動先のY座標")]
    private float moveY;

    void Start()
    {
        //現在位置を保持
        Vector3 currentPos = this.transform.position;

        var sequence = DOTween.Sequence();
        //移動先
        var move = transform.DOMove(new Vector3(moveX, moveY, currentPos.z), moveTime).SetEase(easing);
        //元の位置
        var turnBack = transform.DOMove(currentPos, moveTime).SetEase(easing);

        //移動
        sequence.Append(move);
        // 処理が終了後待機
        sequence.AppendInterval(waitTime);
        //元の位置に戻る
        sequence.Append(turnBack);
        // 処理が終了後待機
        sequence.AppendInterval(waitTime);

        //ループ
        sequence.SetLoops(-1, LoopType.Yoyo);
        //全体的に遅らせる
        sequence.SetDelay(delayTime);
    }
}
