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
/// �v���C���[�̓��ގ��̊Ǘ��N���X
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

    // �v���C���[���Q�[����Join���邽�߂�InputAction
    [SerializeField] private InputAction playerJoinInputAction = default;

    // �ő�Q���l��
    [SerializeField, Label("�ő�v���C���[��")] private int maxPlayerCount = default;

    // Join�ς݂̃f�o�C�X���
    private InputDevice[] joinedDevices = default;
    // ���݂̃v���C���[��
    private int currentPlayerCount = 0;

    //���͂��ꂽ���
    private CharacterType characterType;
    public static List<InputDevice> PlayerInfosDevice = new List<InputDevice>();
    public static List<CharacterType> PlayerInfosCharacterType = new List<CharacterType>();

    [SerializeField, Scene, Label("�J�ڐ�V�[��")]
    private string sceneName;

    [SerializeField, Label("�t�F�C�h�C���[�W")]
    private GameObject fadeImage;

    [SerializeField, Label("�L�[�{�[�h�Ή��X�^�[�g�p")] private InputAction keyboardStartJoinInputAction = default;
    //�L�[�{�[�h�Ή��p�t���O
    public static bool IsKeyboard = false;

    //private StringBuilder Builder = new StringBuilder();
    //private List<int> deviceID = new List<int>();
    //private int isFirstDevice;
    //public static List<CharacterType> PlayerInfosCharacterTypeFirst = new List<CharacterType>();

    private void Awake()
    {
        // �ő�Q���\���Ŕz���������
        joinedDevices = new InputDevice[maxPlayerCount];

        // InputAction��L�������A�R�[���o�b�N��ݒ�
        playerJoinInputAction.Enable();
        playerJoinInputAction.performed += OnJoin;
        keyboardStartJoinInputAction.Enable();
        keyboardStartJoinInputAction.performed += OnKeyboardStart;
        //������
        PlayerInfosDevice.Clear();
        PlayerInfosCharacterType.Clear();
        //������
        IsKeyboard = false;
    }

    private void Start()
    {
        //BGM�Đ�
        BgmManager.Instance.PlayBGM(1,0.15f);
    }

    void Update()
    {
        // Gamepad.all�Ńf�o�C�XID�Ŕ��ʂł���
        //�ڑ����Ă���ԁAScene�����ړ����Ă��f�o�C�XID�͕ς��Ȃ�
        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{
        //    var gamepad = Gamepad.all[i];

        //    Builder.Clear();
        //    Builder.AppendLine($"deviceId:{gamepad.deviceId}");
        //    deviceID[i] = gamepad.deviceId;

        //    //�{�^������������
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
    /// Join�{�^���������ꂽ�����Ƃ��ɌĂ΂�鏈��
    /// </summary>
    private async void OnJoin(InputAction.CallbackContext context)
    {
        if (fadeImage.activeSelf) return;

        // �v���C���[�����ő吔�ɒB���Ă�����A�������I��
        if (currentPlayerCount >= maxPlayerCount)
        {

            return;
        }

        // Join�v�����̃f�o�C�X�����ɎQ���ς݂̂Ƃ��A�������I��
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                return;
            }
        }

        //SE�Đ�
        SeManager.Instance.PlaySE(1);

        //�v���C���[�̔ԍ��ɉ����Đ�������I�u�W�F�N�g��ς���
        if (currentPlayerCount == 0)
        {
            characterType = CharacterType.Character1;
        }
        if (currentPlayerCount == 1)
        {
            characterType = CharacterType.Character2;
        }

        //����ۑ�
        PlayerInfosDevice.Add(context.control.device);
        PlayerInfosCharacterType.Add(characterType);
        
        Debug.Log(context.control.device + " " + characterType.ToString());

        // Join�����f�o�C�X����ۑ�
        joinedDevices[currentPlayerCount] = context.control.device;

        currentPlayerCount++;

        //���������������A��ʑJ�ڂ���
        if (currentPlayerCount >= maxPlayerCount)
        {
            //�J�E���g���N���A����
            currentPlayerCount = 0;
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await FadeManager.Inctance.FadeOut();
            //�J��
            SceneManager.LoadScene(sceneName);
        }
    }


    /// <summary>
    /// �L�[�{�[�h�ŃX�^�[�g����
    /// </summary>
    /// <param name="context"></param>
    private async void OnKeyboardStart(InputAction.CallbackContext context)
    {
        if (fadeImage.activeSelf) return;
        //SE�Đ�
        SeManager.Instance.PlaySE(1);
        //��ʑJ��
        IsKeyboard = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await FadeManager.Inctance.FadeOut();
        SceneManager.LoadScene(sceneName);
    }
}