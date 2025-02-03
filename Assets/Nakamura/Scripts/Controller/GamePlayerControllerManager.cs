using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;

public class GamePlayerControllerManager : MonoBehaviour
{
    [SerializeField] private GameObject character1 = default;
    [SerializeField] private GameObject character2 = default;

    [SerializeField,Label("あざらしのカメラターゲットグループ")]
    private CinemachineTargetGroup targetGroup;

    private void Start()
    {
        CreateCharacter();
    }

    //キャラクターを生成
    private void CreateCharacter()
    {
        if(TitlePlayerJoinManager.IsKeyboard == false)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject character = default;

                switch (TitlePlayerJoinManager.PlayerInfosCharacterType[i])
                {
                    case CharacterType.Character1:
                        character = character1;
                        break;

                    case CharacterType.Character2:
                        character = character2;
                        break;
                }

                var player = PlayerInput.Instantiate(
                prefab: character,
                playerIndex: i,
                pairWithDevice: TitlePlayerJoinManager.PlayerInfosDevice[i]
                );

                //情報をターゲットグループに入れる
                player.gameObject.name = "Player" + i;
                targetGroup.AddMember(player.transform, 1, 2);
            }
        }
        else
        {
            //キーボードで始めたら

            var player1 = PlayerInput.Instantiate(
            prefab: character1,playerIndex: 0);
            var player2 = PlayerInput.Instantiate(
            prefab: character2,playerIndex: 1);

            //情報をターゲットグループに入れる
            player1.gameObject.name = "Player" + 0;
            targetGroup.AddMember(player1.transform, 1, 2);
            player2.gameObject.name = "Player" + 1;
            targetGroup.AddMember(player2.transform, 1, 2);
        }
    }
}
