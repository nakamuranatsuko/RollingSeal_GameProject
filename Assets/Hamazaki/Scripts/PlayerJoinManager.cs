using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̓��ގ��̊Ǘ��N���X�i�A�E�g�Q�[���j
/// </summary>
public class PlayerJoinManager : MonoBehaviour
{
    // �v���C���[���Q�[����Join���邽�߂�InputAction
    [SerializeField] private InputAction playerJoinInputAction = default;

    // �v���C���[1�p��Prefab
    [SerializeField] private GameObject azarashiGomaPrefab = default;

    // �v���C���[2�p��Prefab
    [SerializeField] private GameObject azarashiShiroPrefab = default;

    // �ő�Q���l��
    [SerializeField] private int maxPlayerCount = 2;

    // Join�ς݂̃f�o�C�X���
    private InputDevice[] joinedDevices;
    // ���݂̃v���C���[��
    private int currentPlayerCount = 0;

    private void Awake()
    {
        // �ő�Q���\���Ŕz���������
        joinedDevices = new InputDevice[maxPlayerCount];

        // InputAction��L�������A�R�[���o�b�N��ݒ�
        playerJoinInputAction.Enable();
        playerJoinInputAction.performed += OnJoin;
    }

    private void OnDestroy()
    {
        playerJoinInputAction.Disable();
        playerJoinInputAction.performed -= OnJoin;
    }

    /// <summary>
    /// �f�o�C�X�ɂ����Join�v�������΂����Ƃ��ɌĂ΂�鏈��
    /// </summary>
    private void OnJoin(InputAction.CallbackContext context)
    {
        // �v���C���[�����ő吔�ɒB���Ă�����A�������I��
        if (currentPlayerCount >= maxPlayerCount)
        {
            Debug.Log("���łɍő�v���C���[���ɒB���Ă��܂��B");
            return;
        }

        // Join�v�����̃f�o�C�X�����ɎQ���ς݂̂Ƃ��A�������I��
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                Debug.Log("���̃f�o�C�X�͂��łɎQ���ς݂ł��B");
                return;
            }
        }

        // �v���C���[Prefab��I��
        GameObject selectedPrefab = currentPlayerCount == 0 ? azarashiGomaPrefab : azarashiShiroPrefab;

        // PlayerInput�������������z�̃v���C���[���C���X�^���X��
        PlayerInput.Instantiate(
            prefab: selectedPrefab,
            playerIndex: currentPlayerCount,
            controlScheme: "Gamepad",
            pairWithDevice: context.control.device
        );

        // Join�����f�o�C�X����ۑ�
        joinedDevices[currentPlayerCount] = context.control.device;

        Debug.Log($"�v���C���[{currentPlayerCount + 1}���Q�����܂����B");

        currentPlayerCount++;
    }
}
