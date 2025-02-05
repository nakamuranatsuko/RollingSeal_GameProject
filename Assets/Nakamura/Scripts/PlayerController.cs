using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Label("移動速度")]
    private float speed = 1f;
    [SerializeField, Label("ジャンプパワー")]
    private float jumpPower = 1f;
    [SerializeField, Tag, Label("ジャンプを解除するタグ")]
    private string jumpResetTag;
    [SerializeField, Tag, Label("ゴール用タグ")]
    private string goalTag;
    [SerializeField, Tag, Label("落とし穴用タグ")]
    private string pitfallsTag;
    //一回だけ許可する用
    private bool isFirstGoal = false;
    [SerializeField, Label("カウントダウンキャンバス")]
    private GameObject countDownCanvas;
    [SerializeField, Tag, Label("魚用タグ")]
    private string fishTag;
    [SerializeField, Tag, Label("金の魚用タグ")]
    private string goldFishTag;
    [SerializeField, Tag, Label("氷用タグ")]
    private string iceTag;
    [SerializeField, Tag, Label("ゴール後アニメーション用タグ")]
    private string afterGoalTag;
    //一回だけ許可する用
    private bool isFirstAfterGoal = false;
    //ダッシュ、超ダッシュ状態制限時間用
    private float fishTime;
    private bool isfishTime;

    //移動速度と方向
    private Vector3 velociry = Vector3.zero;
    //ジャンプ中フラグ
    private bool isJump = true;
    //物理演算用
    private Rigidbody playerRB;

    //落ちた時用フラグ
    private bool isFirstPitfalls;

    //プレイヤー保存用
    private GameObject player0;
    private GameObject player1;

    //自身のアニメーター
    private Animator playerAnimator;

    [SerializeField, Foldout("エフェクト"), Label("移動した時")]
    private ParticleSystem moveEff;
    [SerializeField, Foldout("エフェクト"), Label("魚を取った時")]
    private ParticleSystem fishEff;
    [SerializeField, Foldout("エフェクト"), Label("満腹ゲージ最大の時")]
    private ParticleSystem manpukuEff;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isFirstGoal = false;
        isFirstPitfalls = false;
        this.GetComponent<SphereCollider>().isTrigger = false;
        fishTime = 0;
        isfishTime = false;
        isFirstAfterGoal = false;

        //自身のアニメーターを入れる
        playerAnimator = this.GetComponent<Animator>();

        //自身の名前が対象と同じだったら
        if(this.name == "Player0")
        {
            //保存する
            player0 = this.gameObject;
            player1 = GameObject.Find("Player1");
        }
        if (this.name == "Player1")
        {
            //保存する
            player0 = GameObject.Find("Player0");
            player1 = this.gameObject;
        }
    }

    void FixedUpdate()
    {
        //距離減衰
        //player0のポジションを取得
        Vector3 player0Pos = player0.transform.position;
        //player1のポジションを取得
        Vector3 player1Pos = player1.transform.position;
        //player0とplayer1の距離を取得
        float dis = Vector3.Distance(player0Pos, player1Pos);
        //離れすぎたら速度が遅くなる
        if(dis >= 10) speed = 1;
        //満腹ゲージが最大になったら速度を落とす
        if (ManpukuGaugeManager.Instance.ManpukuGaugeMax == true)
        {
            //速度が遅くなる
            speed = 1;
            //エフェクト再生
            manpukuEff.Play();
        }
        else
        {
            //距離が離れすぎてないかつ時間がfalseなら、元の速度に戻る
            if (dis < 10 && isfishTime == false)speed = 7;
        }

        //ゴールしてないなら動ける
        if (isFirstGoal == false)
        {
            //移動速度を初期化
            Vector3 playerVelocity = Vector3.zero;
            //落下の速度を継承
            playerVelocity.y = playerRB.velocity.y;
            //移動方向を反映
            playerVelocity.x = velociry.x * speed;
            playerVelocity.z = velociry.y * speed;
            //ジャンプ中Y軸以外を制限する
            if (isJump)
            {
                playerVelocity.x = velociry.x * 2;
                playerVelocity.z = velociry.y * 2;
            }
            //最終的な速度を物理挙動に反映
            playerRB.velocity = playerVelocity;
        }
        else
        {
            playerRB.velocity = Vector3.zero;
            //エフェクト停止
            //moveEff.Stop();
        }

            //自身が地面より下(地面から落ちた)なら
            if (playerRB.position.y <= -6 && isFirstPitfalls == false)
        {
            isFirstPitfalls = true;
            GameManager.IsPitfalls = true;
        }

        //魚、金の魚に触れたら計測開始
        if(isfishTime == true)
        {
            fishTime += Time.deltaTime;
        }
        else
        {
            //エフェクト停止
            moveEff.Stop();
        }

        //時間が経ったらスピードを戻す
        if (fishTime > 2)
        {
            speed = 7;
            fishTime = 0;
            isfishTime = false;
        }
    }

    /// <summary>
    /// インプットシステムから呼び出される関数
    /// </summary>
    /// <param name="callBackContext">呼び出した状態</param>
    public void OnMove(InputAction.CallbackContext callBackContext)
    {
        if (countDownCanvas.activeSelf) return;
        //コントローラー情報
        velociry = callBackContext.ReadValue<Vector2>();
        //アニメーション
        playerAnimator.SetBool("Move", true);
        playerAnimator.SetBool("Idle", false);
    }

    public void OnJump(InputAction.CallbackContext callBackContext)
    {
        if (countDownCanvas.activeSelf) return;
        //二段ジャンプを阻止
        if (isJump) return;
        if (callBackContext.performed)
        {
            //満腹ゲージが最大なら効果が起きない
            if (ManpukuGaugeManager.Instance.ManpukuGaugeMax == false)
            {
                //満腹ゲージを減らす
                ManpukuGaugeManager.Instance.JumpGauge = true;
            }
            //SE再生
            SeManager.Instance.PlaySE(4);
            isJump = true;
            playerRB.AddForce(Vector3.up * jumpPower,ForceMode.Impulse);
            //アニメーション
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面に付いたらジャンプを許可する(二段ジャンプを阻止)
        if (collision.gameObject.CompareTag(jumpResetTag))
        {
            isJump = false;
        }

        //ゴールしたらtrueにする
        if(collision.gameObject.CompareTag(goalTag) && isFirstGoal == false)
        {
            isFirstGoal = true;
            GameManager.Instance.IsGoal = true;
        }

        //魚に触れたら
        if (collision.gameObject.CompareTag(fishTag))
        {
            //満腹ゲージが最大なら触れても効果が起きない
            if (ManpukuGaugeManager.Instance.ManpukuGaugeMax == false)
            {
                //満腹ゲージを増やす
                ManpukuGaugeManager.Instance.FishGauge = true;
                //ダッシュ状態になる
                speed = 10;
                isfishTime = true;
                //エフェクト再生
                moveEff.Play();
            }
            //エフェクト再生
            fishEff.Play();
            //SE再生
            SeManager.Instance.PlaySE(14);
            collision.gameObject.SetActive(false);
        }

        //金の魚に触れたら
        if (collision.gameObject.CompareTag(goldFishTag))
        {
            //満腹ゲージが最大なら触れても効果が起きない
            if (ManpukuGaugeManager.Instance.ManpukuGaugeMax == false)
            {
                //満腹ゲージを増やす
                ManpukuGaugeManager.Instance.GoldFishGauge = true;
                //超ダッシュ状態になる
                speed = 12;
                isfishTime = true;
                //エフェクト再生
                moveEff.Play();
            }
            //エフェクト再生
            fishEff.Play();
            //SE再生
            SeManager.Instance.PlaySE(14);
            collision.gameObject.SetActive(false);
        }

        //氷に触れたら
        if (collision.gameObject.CompareTag(iceTag))
        {
            //SE再生
            SeManager.Instance.PlaySE(6);
            //氷を壊す(非表示)
            collision.gameObject.SetActive(false);
        }

        //ゴール後の特定の地面に触れたら
        if(collision.gameObject.CompareTag(afterGoalTag) && isFirstAfterGoal == false)
        {
            isFirstAfterGoal = true;
            GameManager.IsResultGoal = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //落とし穴に触れたら
        if (other.gameObject.CompareTag(pitfallsTag))
        {
            this.GetComponent<SphereCollider>().isTrigger = true;
            GameManager.IsPitfalls = true;
            //エフェクト停止
            moveEff.Stop();
        }
    }
}
