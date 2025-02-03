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

    [SerializeField,Label("�����炵�̃J�����^�[�Q�b�g�O���[�v")]
    private CinemachineTargetGroup targetGroup;

    private void Start()
    {
        CreateCharacter();
    }

    //�L�����N�^�[�𐶐�
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

                //�����^�[�Q�b�g�O���[�v�ɓ����
                player.gameObject.name = "Player" + i;
                targetGroup.AddMember(player.transform, 1, 2);
            }
        }
        else
        {
            //�L�[�{�[�h�Ŏn�߂���

            var player1 = PlayerInput.Instantiate(
            prefab: character1,playerIndex: 0);
            var player2 = PlayerInput.Instantiate(
            prefab: character2,playerIndex: 1);

            //�����^�[�Q�b�g�O���[�v�ɓ����
            player1.gameObject.name = "Player" + 0;
            targetGroup.AddMember(player1.transform, 1, 2);
            player2.gameObject.name = "Player" + 1;
            targetGroup.AddMember(player2.transform, 1, 2);
        }
    }
}
