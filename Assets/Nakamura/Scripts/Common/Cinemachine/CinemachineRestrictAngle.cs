using UnityEngine;

namespace Cinemachine
{
    [AddComponentMenu("")] // Hide in menu
    [ExecuteAlways]
    [SaveDuringPlay]
    public class CinemachineRestrictAngle : CinemachineExtension
    {
        /// <summary>
        /// field �� public �ɂ��邱�Ƃ� SaveDuringPlay ���\�ɂȂ�
        /// </summary>
        [Header("�K�p�i�K")]
        public CinemachineCore.Stage m_ApplyAfter = CinemachineCore.Stage.Aim;

        [Header("���Պp 臒l")]
        [Range(0f, 90f)]
        public float lowAngleThreshold = 80f;

        [Header("�A�I���p 臒l")]
        [Range(0f, 90f)]
        public float highAngleThreshold = 80f;

        /// <summary>
        /// �J�����p�����[�^�X�V��ɌĂяo����� Callback�B�����Ō��ʂ����������B
        /// </summary>
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != m_ApplyAfter) return;

            // �J������ X ����]�𐧌�����
            var eulerAngles = state.RawOrientation.eulerAngles;
            eulerAngles.x = Mathf.Clamp(eulerAngles.x, -highAngleThreshold, lowAngleThreshold);
            state.RawOrientation = Quaternion.Euler(eulerAngles);
        }
    }
}
