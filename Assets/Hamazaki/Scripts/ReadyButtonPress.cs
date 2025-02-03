using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonPress : MonoBehaviour
{
    public Button leftButton;  // コントローラー1用ボタン
    public Button rightButton; // コントローラー2用ボタン

    private bool controller1Ready = false;
    private bool controller2Ready = false;

    void Update()
    {
        // コントローラー1でEastボタンが押された
        if (Input.GetButtonDown("Controller1_East"))
        {
            controller1Ready = true;
            Debug.Log("Player 1 is ready!");
            leftButton.interactable = false;  // ボタンを無効化（押されたことにする）
        }

        // コントローラー2でEastボタンが押された
        if (Input.GetButtonDown("Controller2_East"))
        {
            controller2Ready = true;
            Debug.Log("Player 2 is ready!");
            rightButton.interactable = false;  // ボタンを無効化（押されたことにする）
        }

        // 両方のプレイヤーが準備完了した場合
        if (controller1Ready && controller2Ready)
        {
            // 両方のプレイヤーが準備完了の場合、ゲーム開始などの処理を行う
            Debug.Log("Both players are ready!");
            // ゲーム開始の処理を追加する場合、ここにコードを追加
        }
    }
}
