using UnityEngine;

public class PlayerToMouseRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _mainBone;

    [SerializeField]
    [Tooltip("Speed of player reaction to cursor. Less value - smootly effect")]
    private float wakeUpAdjustSpeed;
    

    private Quaternion _worldBoneRotation;
    private float _interpolation = 1;


    [SerializeField]
    private Vector3 _forwardBodyDirection;
    [SerializeField]
    private Vector3 _upBodyDirection;

    private void Start()
    {
        var correctedForward = _mainBone.TransformDirection(_forwardBodyDirection).normalized;
        var correctedUpward = _mainBone.TransformDirection(_upBodyDirection).normalized;
        _worldBoneRotation = Quaternion.Inverse(
            Quaternion.LookRotation(correctedForward, correctedUpward)
            ) * _mainBone.rotation;
    } 

    private void LateUpdate()
    {
        _interpolation = Mathf.Min(1, _interpolation + wakeUpAdjustSpeed * Time.deltaTime);
        var lookDirection = _target.position - _mainBone.position;
        var targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up) * _worldBoneRotation;
        _mainBone.rotation = Quaternion.Lerp(_mainBone.rotation, targetRotation, _interpolation);
    }

    private void OnDisable()
    {
        _interpolation = 0;
    }
}
