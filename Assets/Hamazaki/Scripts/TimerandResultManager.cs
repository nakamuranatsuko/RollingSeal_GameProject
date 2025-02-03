using UnityEngine;

public class TimerandResultManager : MonoBehaviour
{
    public static TimerandResultManager Instance { get; private set; }

    // �^�C�}�[���
    public int minutes = 5;
    public int seconds = 49;

    // ���U���g���
    public static int SelectedCategoryIndex = 0; // 0:Result1, 1:Result2, 2:Result3
    public static string SelectedRating = "A";    // "A", "B", "C", "D"

    // �v���n�u�ȂǂɃA�^�b�`����ꍇ
    public RatingObjectSwitcher ratingObjectSwitcher;

    [SerializeField]
    private GameObject effectObj;

    private void Awake()
    {
        //�ύX����(����)
        //2�C�Ƃ��S�[��������
        if (GameManager.IsResultGoal == true)
        {
            //���U���g�̃A�U���V�̕\�L�͓�C����(���C)
            SelectedCategoryIndex = 0;
            //���ԕ]��
            if (TimerManager.Min == 0 && TimerManager.Sec <= 30) SelectedRating = "A";
            if (TimerManager.Min == 1 || TimerManager.Sec >  30) SelectedRating = "B";
            if (TimerManager.Min >= 2) SelectedRating = "C";
            GameManager.IsResultGoal = false;
            //�G�t�F�N�g�𗬂�
            effectObj.SetActive(true);
            //SE�Đ�
            SeManager.Instance.PlaySE(16);
        }

        //���b�󂯂Ă���C�ڂ��S�[�����Ȃ�������
        if (GameManager.IsResultOne == true)
        {
            //���U���g�̃A�U���V�̕\�L�͈�C�̂�
            SelectedCategoryIndex = 1;
            //���ԕ]��
            SelectedRating = "D";
            GameManager.IsResultOne = false;
            //�G�t�F�N�g���~�߂�
            effectObj.SetActive(false);
        }

        //�ǂ��炩����������
        if (GameManager.IsResultFell == true)
        {
            //���U���g�̃A�U���V�̕\�L�͓�C����(�Ԃ�Ă�)
            SelectedCategoryIndex = 2;
            //���ԕ]��
            SelectedRating = "D";
            GameManager.IsResultFell = false;
            //�G�t�F�N�g���~�߂�
            effectObj.SetActive(false);
            //SE�Đ�
            SeManager.Instance.PlaySE(15);
        }
    }

    // �^�C�}�[��ݒ肷�郁�\�b�h
    public void SetTime(int newMinutes, int newSeconds)
    {
        minutes = newMinutes;
        seconds = newSeconds;
    }

    // ���U���g�̑I����ݒ肷�郁�\�b�h
    public void SetCategoryAndRating(int categoryIndex, string rating)
    {
        SelectedCategoryIndex = categoryIndex;
        SelectedRating = rating;
    }
}
