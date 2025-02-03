using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの入退室の管理クラス（アウトゲーム）
/// </summary>
public class PlayerJoinManager : MonoBehaviour
{
    // プレイヤーがゲームにJoinするためのInputAction
    [SerializeField] private InputAction playerJoinInputAction = default;

    // プレイヤー1用のPrefab
    [SerializeField] private GameObject azarashiGomaPrefab = default;

    // プレイヤー2用のPrefab
    [SerializeField] private GameObject azarashiShiroPrefab = default;

    // 最大参加人数
    [SerializeField] private int maxPlayerCount = 2;

    // Join済みのデバイス情報
    private InputDevice[] joinedDevices;
    // 現在のプレイヤー数
    private int currentPlayerCount = 0;

    private void Awake()
    {
        // 最大参加可能数で配列を初期化
        joinedDevices = new InputDevice[maxPlayerCount];

        // InputActionを有効化し、コールバックを設定
        playerJoinInputAction.Enable();
        playerJoinInputAction.performed += OnJoin;
    }

    private void OnDestroy()
    {
        playerJoinInputAction.Disable();
        playerJoinInputAction.performed -= OnJoin;
    }

    /// <summary>
    /// デバイスによってJoin要求が発火したときに呼ばれる処理
    /// </summary>
    private void OnJoin(InputAction.CallbackContext context)
    {
        // プレイヤー数が最大数に達していたら、処理を終了
        if (currentPlayerCount >= maxPlayerCount)
        {
            Debug.Log("すでに最大プレイヤー数に達しています。");
            return;
        }

        // Join要求元のデバイスが既に参加済みのとき、処理を終了
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                Debug.Log("このデバイスはすでに参加済みです。");
                return;
            }
        }

        // プレイヤーPrefabを選択
        GameObject selectedPrefab = currentPlayerCount == 0 ? azarashiGomaPrefab : azarashiShiroPrefab;

        // PlayerInputを所持した仮想のプレイヤーをインスタンス化
        PlayerInput.Instantiate(
            prefab: selectedPrefab,
            playerIndex: currentPlayerCount,
            controlScheme: "Gamepad",
            pairWithDevice: context.control.device
        );

        // Joinしたデバイス情報を保存
        joinedDevices[currentPlayerCount] = context.control.device;

        Debug.Log($"プレイヤー{currentPlayerCount + 1}が参加しました。");

        currentPlayerCount++;
    }
}
