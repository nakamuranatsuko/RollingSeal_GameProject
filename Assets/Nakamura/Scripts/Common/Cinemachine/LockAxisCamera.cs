using UnityEngine;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
[ExecuteAlways]
public class LockAxisCamera : CinemachineExtension
{
    public bool x_islocked, y_islocked, z_islocked;
    public bool x_isrotate, y_isrotate, z_isrotate;
    public Vector3 lockPosition;
    public Vector3 lockRotate;

    /// <summary>
    /// field �� public �ɂ��邱�Ƃ� SaveDuringPlay ���\�ɂȂ�
    /// </summary>
    [Header("�K�p�i�K")]
    public CinemachineCore.Stage m_ApplyAfter = CinemachineCore.Stage.Aim;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;
            if (x_islocked) newPos.x = lockPosition.x;
            if (y_islocked) newPos.y = lockPosition.y;
            if (z_islocked) newPos.z = lockPosition.z;
            state.RawPosition = newPos;
        }

        if (stage == m_ApplyAfter)
        {
            var newRotate = state.RawOrientation.eulerAngles;
            if (x_isrotate) newRotate.x = lockRotate.x;
            if (y_isrotate) newRotate.y = lockRotate.y;
            if (z_isrotate) newRotate.z = lockRotate.z;
            state.RawOrientation = Quaternion.Euler(newRotate);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(LockAxisCamera))]
public class LockAxisCameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var lockAxisCamera = target as LockAxisCamera;
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            EditorGUILayout.LabelField("�Œ肷�鎲");
            lockAxisCamera.x_islocked = EditorGUILayout.Toggle("X", lockAxisCamera.x_islocked);
            lockAxisCamera.y_islocked = EditorGUILayout.Toggle("Y", lockAxisCamera.y_islocked);
            lockAxisCamera.z_islocked = EditorGUILayout.Toggle("Z", lockAxisCamera.z_islocked);
        }
        EditorGUILayout.LabelField("�Œ肷����W");
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            lockAxisCamera.lockPosition.x = EditorGUILayout.FloatField("X", lockAxisCamera.lockPosition.x);
            lockAxisCamera.lockPosition.y = EditorGUILayout.FloatField("Y", lockAxisCamera.lockPosition.y);
            lockAxisCamera.lockPosition.z = EditorGUILayout.FloatField("Z", lockAxisCamera.lockPosition.z);
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("\n");
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            EditorGUILayout.LabelField("�Œ肷���]");
            lockAxisCamera.x_isrotate = EditorGUILayout.Toggle("X", lockAxisCamera.x_isrotate);
            lockAxisCamera.y_isrotate = EditorGUILayout.Toggle("Y", lockAxisCamera.y_isrotate);
            lockAxisCamera.z_isrotate = EditorGUILayout.Toggle("Z", lockAxisCamera.z_isrotate);
        }
        EditorGUILayout.LabelField("�Œ肷���]");
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUIUtility.labelWidth = 10;
            lockAxisCamera.lockRotate.x = EditorGUILayout.FloatField("X", lockAxisCamera.lockRotate.x);
            lockAxisCamera.lockRotate.y = EditorGUILayout.FloatField("Y", lockAxisCamera.lockRotate.y);
            lockAxisCamera.lockRotate.z = EditorGUILayout.FloatField("Z", lockAxisCamera.lockRotate.z);
        }
    }
}
#endif
