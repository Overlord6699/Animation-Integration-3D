using UnityEngine;

public class RootBoneSaver : MonoBehaviour
{
    [SerializeField][Header("Главная кость(pelvis):")]
    private Transform _rootBone;

    private Vector3 _localPosition;
    private Quaternion _localRotation;
    private Vector3 _boneOffset;
    private Quaternion _boneRotationoffset;
    
    private void OnEnable()
    {     
        ProcessRoot();
    }

    private void ProcessRoot()
    {
        _localPosition = _rootBone.localPosition;
        _localRotation = _rootBone.localRotation;

        CalculatePositionOffset();
        CalculateRotationOffset();
    }

    private void CalculateRotationOffset()
    {
        _boneRotationoffset = Quaternion.Inverse(_rootBone.rotation) * transform.rotation;
    }

    private void CalculatePositionOffset()
    {
        var offset = transform.position - _rootBone.position;
        _boneOffset = _rootBone.InverseTransformVector(offset);
    }

    private void LateUpdate()
    {
        transform.position = _rootBone.position + _rootBone.TransformVector(_boneOffset);
        transform.rotation = _rootBone.rotation * _boneRotationoffset;

        _rootBone.localPosition = _localPosition;
        _rootBone.localRotation = _localRotation;
    }
}
