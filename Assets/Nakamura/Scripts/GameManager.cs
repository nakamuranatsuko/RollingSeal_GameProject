using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;
using System;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance
                    = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    //ゴールしたかどうか
    public bool IsGoal;
    public static bool IsResultGoal;
    public int goalNum;
    [SerializeField, Scene, Label("リザルトシーン")]
    private string resultSceneName;

    [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup = null;

    private List<PlayerInput> _playerInputs = new List<PlayerInput>();
    private int _playerIndex = 0;

    [ReadOnly, Label("落とし穴に落ちた用フラグ")]
    public static bool IsPitfalls = false;
    public static bool IsResultFell;
    private int pitfalls = 0;

    [ReadOnly, Label("一匹目がゴールしてから二匹目がある程度の時間内にゴールしなかった用フラグ")]
    public bool IsOne;
    public static bool IsResultOne;
    private float oneAzarashiTime;

    void Start()
    {
        //BGM再生
        BgmManager.Instance.PlayBGM(2, 0.15f);

        IsResultGoal = false;
        IsResultFell = false;
        IsResultOne = false;

        goalNum = 0;
        pitfalls = 0;
    }

    async void Update()
    {
        //ゴールしたら
        if(IsGoal == true)
        {
            IsGoal = false;
            goalNum++;
            //SE再生
            SeManager.Instance.PlaySE(10);
        }

        //一匹目がゴールしたらタイマー開始
        if (goalNum == 1)
        {
            oneAzarashiTime += Time.deltaTime;
        }
        //タイマーが時間以上になったら一匹用リザルトへ
        if (oneAzarashiTime >= 4)
        {
            oneAzarashiTime = 0;
            IsOne = true;
        }

        //二匹ゴールしたら
        if (goalNum == 2)
        {
            goalNum = 0;
            IsResultGoal = true;
            //SE再生
            SeManager.Instance.PlaySE(7);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            await FadeManager.Inctance.FadeOut();
            await SceneManager.LoadSceneAsync(resultSceneName);
        }

        //落とし穴に落ちる、もしくはある程度落ちたら画面遷移
        if(IsPitfalls == true)
        {
            IsPitfalls = false;
            IsResultFell = true;
            if(pitfalls == 0)
            {
                pitfalls++;
                //SE再生
                SeManager.Instance.PlaySE(13);
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                await FadeManager.Inctance.FadeOut();
                await SceneManager.LoadSceneAsync(resultSceneName);
            }
        }

        //一匹目がゴールしてから二匹目がある程度の時間内にゴールしなかったら画面遷移
        if (IsOne == true)
        {
            IsOne = false;
            IsResultOne = true;
            //SE再生
            SeManager.Instance.PlaySE(10);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await FadeManager.Inctance.FadeOut();
            await SceneManager.LoadSceneAsync(resultSceneName);
        }

    }
    // プレイヤー入室時に受け取る通知
    public void OnPlayerJoined(PlayerInput player_input)
    {
        foreach (var device in player_input.devices)
        {
            if (_playerInputs.Contains(player_input)) continue;
            player_input.gameObject.name = "Player" + _playerIndex++;
            _cinemachineTargetGroup.AddMember(player_input.transform, 1, 2);
            _playerInputs.Add(player_input);
            Debug.Log($"プレイヤー{player_input.user.index}が入室");
        }
    }
}
