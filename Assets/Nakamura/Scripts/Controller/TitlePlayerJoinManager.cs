using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Text;

public enum CharacterType
{
    Character1,
    Character2,
}

/// <summary>
/// プレイヤーの入退室の管理クラス
/// </summary>
public class TitlePlayerJoinManager : MonoBehaviour
{
    private static TitlePlayerJoinManager _instance;
    public static TitlePlayerJoinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance
                    = GameObject.FindObjectOfType<TitlePlayerJoinManager>();
            }
            return _instance;
        }
    }

    // プレイヤーがゲームにJoinするためのInputAction
    [SerializeField] private InputAction playerJoinInputAction = default;

    // 最大参加人数
    [SerializeField, Label("最大プレイヤー数")] private int maxPlayerCount = default;

    // Join済みのデバイス情報
    private InputDevice[] joinedDevices = default;
    // 現在のプレイヤー数
    private int currentPlayerCount = 0;

    //入力された情報
    private CharacterType characterType;
    public static List<InputDevice> PlayerInfosDevice = new List<InputDevice>();
    public static List<CharacterType> PlayerInfosCharacterType = new List<CharacterType>();

    [SerializeField, Scene, Label("遷移先シーン")]
    private string sceneName;

    [SerializeField, Label("フェイドイメージ")]
    private GameObject fadeImage;

    [SerializeField, Label("キーボード対応スタート用")] private InputAction keyboardStartJoinInputAction = default;
    //キーボード対応用フラグ
    public static bool IsKeyboard = false;

    //private StringBuilder Builder = new StringBuilder();
    //private List<int> deviceID = new List<int>();
    //private int isFirstDevice;
    //public static List<CharacterType> PlayerInfosCharacterTypeFirst = new List<CharacterType>();

    private void Awake()
    {
        // 最大参加可能数で配列を初期化
        joinedDevices = new InputDevice[maxPlayerCount];

        // InputActionを有効化し、コールバックを設定
        playerJoinInputAction.Enable();
        playerJoinInputAction.performed += OnJoin;
        keyboardStartJoinInputAction.Enable();
        keyboardStartJoinInputAction.performed += OnKeyboardStart;
        //初期化
        PlayerInfosDevice.Clear();
        PlayerInfosCharacterType.Clear();
        //初期化
        IsKeyboard = false;
    }

    private void Start()
    {
        //BGM再生
        BgmManager.Instance.PlayBGM(1,0.15f);
    }

    void Update()
    {
        // Gamepad.allでデバイスIDで判別できる
        //接続している間、Scene内を移動してもデバイスIDは変わらない
        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{
        //    var gamepad = Gamepad.all[i];

        //    Builder.Clear();
        //    Builder.AppendLine($"deviceId:{gamepad.deviceId}");
        //    deviceID[i] = gamepad.deviceId;

        //    //ボタンを押したら
        //    if (gamepad.aButton.isPressed && isFirstDevice < Gamepad.all.Count)
        //    {
        //        if (isFirstDevice < Gamepad.all.Count)
        //        {
        //            isFirstDevice++;

        //            if (i == 0)
        //            {
        //                characterType = CharacterType.Character1;
        //            }
        //            if (i == 1)
        //            {
        //                characterType = CharacterType.Character2;
        //            }

        //            PlayerInfosCharacterTypeFirst.Add(characterType);
        //        }
        //        else
        //        {
        //            PlayerInfosCharacterType.Add(characterType);
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Joinボタンが押されたしたときに呼ばれる処理
    /// </summary>
    private async void OnJoin(InputAction.CallbackContext context)
    {
        if (fadeImage.activeSelf) return;

        // プレイヤー数が最大数に達していたら、処理を終了
        if (currentPlayerCount >= maxPlayerCount)
        {

            return;
        }

        // Join要求元のデバイスが既に参加済みのとき、処理を終了
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                return;
            }
        }

        //SE再生
        SeManager.Instance.PlaySE(1);

        //プレイヤーの番号に応じて生成するオブジェクトを変える
        if (currentPlayerCount == 0)
        {
            characterType = CharacterType.Character1;
        }
        if (currentPlayerCount == 1)
        {
            characterType = CharacterType.Character2;
        }

        //情報を保存
        PlayerInfosDevice.Add(context.control.device);
        PlayerInfosCharacterType.Add(characterType);
        
        Debug.Log(context.control.device + " " + characterType.ToString());

        // Joinしたデバイス情報を保存
        joinedDevices[currentPlayerCount] = context.control.device;

        currentPlayerCount++;

        //数字が超えた時、画面遷移する
        if (currentPlayerCount >= maxPlayerCount)
        {
            //カウントをクリアする
            currentPlayerCount = 0;
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await FadeManager.Inctance.FadeOut();
            //遷移
            SceneManager.LoadScene(sceneName);
        }
    }


    /// <summary>
    /// キーボードでスタートする
    /// </summary>
    /// <param name="context"></param>
    private async void OnKeyboardStart(InputAction.CallbackContext context)
    {
        if (fadeImage.activeSelf) return;
        //SE再生
        SeManager.Instance.PlaySE(1);
        //画面遷移
        IsKeyboard = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await FadeManager.Inctance.FadeOut();
        SceneManager.LoadScene(sceneName);
    }
}