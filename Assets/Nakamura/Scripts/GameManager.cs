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

    //�S�[���������ǂ���
    public bool IsGoal;
    public static bool IsResultGoal;
    public int goalNum;
    [SerializeField, Scene, Label("���U���g�V�[��")]
    private string resultSceneName;

    [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup = null;

    private List<PlayerInput> _playerInputs = new List<PlayerInput>();
    private int _playerIndex = 0;

    [ReadOnly, Label("���Ƃ����ɗ������p�t���O")]
    public static bool IsPitfalls = false;
    public static bool IsResultFell;
    private int pitfalls = 0;

    [ReadOnly, Label("��C�ڂ��S�[�����Ă����C�ڂ�������x�̎��ԓ��ɃS�[�����Ȃ������p�t���O")]
    public bool IsOne;
    public static bool IsResultOne;
    private float oneAzarashiTime;

    void Start()
    {
        //BGM�Đ�
        BgmManager.Instance.PlayBGM(2, 0.15f);

        IsResultGoal = false;
        IsResultFell = false;
        IsResultOne = false;

        goalNum = 0;
        pitfalls = 0;
    }

    async void Update()
    {
        //�S�[��������
        if(IsGoal == true)
        {
            IsGoal = false;
            goalNum++;
            //SE�Đ�
            SeManager.Instance.PlaySE(10);
        }

        //��C�ڂ��S�[��������^�C�}�[�J�n
        if (goalNum == 1)
        {
            oneAzarashiTime += Time.deltaTime;
        }
        //�^�C�}�[�����Ԉȏ�ɂȂ������C�p���U���g��
        if (oneAzarashiTime >= 4)
        {
            oneAzarashiTime = 0;
            IsOne = true;
        }

        //��C�S�[��������
        if (goalNum == 2)
        {
            goalNum = 0;
            IsResultGoal = true;
            //SE�Đ�
            SeManager.Instance.PlaySE(7);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            await FadeManager.Inctance.FadeOut();
            await SceneManager.LoadSceneAsync(resultSceneName);
        }

        //���Ƃ����ɗ�����A�������͂�����x���������ʑJ��
        if(IsPitfalls == true)
        {
            IsPitfalls = false;
            IsResultFell = true;
            if(pitfalls == 0)
            {
                pitfalls++;
                //SE�Đ�
                SeManager.Instance.PlaySE(13);
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                await FadeManager.Inctance.FadeOut();
                await SceneManager.LoadSceneAsync(resultSceneName);
            }
        }

        //��C�ڂ��S�[�����Ă����C�ڂ�������x�̎��ԓ��ɃS�[�����Ȃ��������ʑJ��
        if (IsOne == true)
        {
            IsOne = false;
            IsResultOne = true;
            //SE�Đ�
            SeManager.Instance.PlaySE(10);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await FadeManager.Inctance.FadeOut();
            await SceneManager.LoadSceneAsync(resultSceneName);
        }

    }
    // �v���C���[�������Ɏ󂯎��ʒm
    public void OnPlayerJoined(PlayerInput player_input)
    {
        foreach (var device in player_input.devices)
        {
            if (_playerInputs.Contains(player_input)) continue;
            player_input.gameObject.name = "Player" + _playerIndex++;
            _cinemachineTargetGroup.AddMember(player_input.transform, 1, 2);
            _playerInputs.Add(player_input);
            Debug.Log($"�v���C���[{player_input.user.index}������");
        }
    }
}
